﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace Microsoft.Psi.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Implements a time-based windowing component.
    /// </summary>
    /// <typeparam name="TInput">The type of input messages</typeparam>
    /// <typeparam name="TOutput">The type of output messages</typeparam>
    public class RelativeTimeWindow<TInput, TOutput> : ConsumerProducer<TInput, TOutput>
    {
        private readonly IRecyclingPool<Message<TInput>> recycler = RecyclingPool.Create<Message<TInput>>();
        private readonly RelativeTimeInterval relativeTimeInterval;
        private readonly Func<IEnumerable<Message<TInput>>, TOutput> selector;

        private int anchorMessageSequenceId = -1;
        private DateTime anchorMessageOriginatingTime = DateTime.MinValue;
        private bool initialBuffer = true; // constructing first initial buffer
        private Queue<Message<TInput>> buffer = new Queue<Message<TInput>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="RelativeTimeWindow{TInput, TOutput}"/> class.
        /// </summary>
        /// <param name="pipeline">Pipeline to which this component belongs.</param>
        /// <param name="relativeTimeInterval">The relative time interval over which to gather messages.</param>
        /// <param name="selector">Select output message from collected window of input messages.</param>
        public RelativeTimeWindow(Pipeline pipeline, RelativeTimeInterval relativeTimeInterval, Func<IEnumerable<Message<TInput>>, TOutput> selector)
            : base(pipeline)
        {
            this.relativeTimeInterval = relativeTimeInterval;
            this.selector = selector;
            this.In.Unsubscribed += _ => this.OnUnsubscribed();
        }

        /// <inheritdoc />
        protected override void Receive(TInput value, Envelope envelope)
        {
            // clone and add the new message
            var message = new Message<TInput>(value, envelope).DeepClone(this.recycler);

            // time-based
            if (this.initialBuffer)
            {
                var clone = this.buffer.DeepClone();
                this.buffer.Enqueue(message);
                if (this.CheckRemoval(message.OriginatingTime))
                {
                    this.initialBuffer = false;
                    this.ProcessWindow(clone, false, this.Out);
                    this.ProcessRemoval(message.OriginatingTime);
                    this.ProcessWindow(this.buffer, false, this.Out);
                }
            }
            else
            {
                this.buffer.Enqueue(message);
                this.ProcessRemoval(message.OriginatingTime);
                this.ProcessWindow(this.buffer, false, this.Out);
            }
        }

        private bool RemoveCondition(Message<TInput> message, DateTime currentTime)
        {
            if (this.initialBuffer)
            {
                return message.OriginatingTime < (this.buffer.Last().OriginatingTime + this.relativeTimeInterval).Left;
            }
            else
            {
                return this.anchorMessageOriginatingTime > DateTime.MinValue && message.OriginatingTime < (this.anchorMessageOriginatingTime + this.relativeTimeInterval).Left;
            }
        }

        private void ProcessWindow(IEnumerable<Message<TInput>> messageList, bool final, Emitter<TOutput> emitter)
        {
            this.initialBuffer = false;
            var messages = messageList.ToArray();
            var anchorMessageIndex = 0;
            if (this.anchorMessageSequenceId >= 0)
            {
                for (int i = 0; i < messages.Length; i++)
                {
                    if (messages[i].Envelope.SequenceId == this.anchorMessageSequenceId)
                    {
                        anchorMessageIndex = i + 1;
                        break;
                    }
                }
            }

            if (anchorMessageIndex < messages.Length)
            {
                // compute the time interval from the next point we should output
                TimeInterval window = messages[anchorMessageIndex].OriginatingTime + this.relativeTimeInterval;

                // decide whether we should output - only output when we have seen enough (or know that nothing further is to be seen - `final`).
                // evidence that nothing else will appear in the window
                bool shouldOutputNextMessage = final || messages.Last().OriginatingTime > window.Right;

                if (shouldOutputNextMessage)
                {
                    // compute the buffer to return
                    var ret = this.selector(messages.Where(m => window.PointIsWithin(m.OriginatingTime)));

                    // post it with the originating time of the anchor message
                    emitter.Post(ret, messages[anchorMessageIndex].OriginatingTime);

                    // set the sequence id for the last originating message that was posted
                    this.anchorMessageSequenceId = messages[anchorMessageIndex].SequenceId;
                    this.anchorMessageOriginatingTime = messages[anchorMessageIndex].OriginatingTime;
                }
            }
        }

        private void DequeueBuffer()
        {
            var free = this.buffer.Dequeue();
            this.recycler?.Recycle(free);
        }

        private bool CheckRemoval(DateTime originatingTime)
        {
            return this.buffer.Count > 0 && this.RemoveCondition(this.buffer.Peek(), originatingTime);
        }

        private void ProcessRemoval(DateTime originatingTime)
        {
            while (this.CheckRemoval(originatingTime))
            {
                this.DequeueBuffer();
            }
        }

        private void OnUnsubscribed()
        {
            // give the processor an opportunity now with `final` flag set
            while (this.buffer.Count > 0)
            {
                this.ProcessWindow(this.buffer, true, this.Out);
                if (this.CheckRemoval(DateTime.MaxValue))
                {
                    this.ProcessRemoval(DateTime.MaxValue);
                    this.ProcessWindow(this.buffer, true, this.Out);
                }

                if (this.buffer.Count > 0)
                {
                    // continue processing with trailing buffer
                    this.DequeueBuffer();
                }
            }
        }
    }
}