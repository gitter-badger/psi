﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace Microsoft.Psi
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading;
    using Microsoft.Psi.Components;

    /// <summary>
    /// Extension methods that simplify operator usage
    /// </summary>
    public static partial class Operators
    {
        /// <summary>
        /// Convert a stream to an <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <remarks>
        /// This may be traversed while the pipeline runs async, or may collect values to be consumed after pipeline disposal.
        /// </remarks>
        /// <typeparam name="T">Type of messages for the source stream.</typeparam>
        /// <param name="source">The source stream.</param>
        /// <param name="condition">Predicate condition while which values will be enumerated (otherwise infinite).</param>
        /// <param name="deliveryPolicy">An optional delivery policy.</param>
        /// <returns>Enumerable with elements from the source stream.</returns>
        public static IEnumerable<T> ToEnumerable<T>(this IProducer<T> source, Func<T, bool> condition = null, DeliveryPolicy deliveryPolicy = null)
        {
            return new StreamEnumerable<T>(source, condition, deliveryPolicy);
        }

        /// <summary>
        /// Enumerable stream class.
        /// </summary>
        /// <typeparam name="T">Type of stream messages.</typeparam>
        public class StreamEnumerable<T> : IEnumerable, IEnumerable<T>
        {
            private readonly StreamEnumerator enumerator;

            /// <summary>
            /// Initializes a new instance of the <see cref="StreamEnumerable{T}"/> class.
            /// </summary>
            /// <param name="source">The source stream to enumerate.</param>
            /// <param name="predicate">Predicate (filter) function.</param>
            /// <param name="deliveryPolicy">An optional delivery policy.</param>
            public StreamEnumerable(IProducer<T> source, Func<T, bool> predicate = null, DeliveryPolicy deliveryPolicy = null)
            {
                this.enumerator = new StreamEnumerator(predicate ?? (_ => true));

                var processor = new Processor<T, T>(
                    source.Out.Pipeline,
                    (d, e, s) =>
                    {
                        this.enumerator.Queue.Enqueue(d);
                        this.enumerator.Update.Set();
                    });

                source.PipeTo(processor, deliveryPolicy);
                processor.In.Unsubscribed += _ => this.enumerator.Update.Set();
            }

            /// <inheritdoc />
            public IEnumerator GetEnumerator()
            {
                return this.enumerator;
            }

            /// <inheritdoc />
            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                return this.enumerator;
            }

            private class StreamEnumerator : IEnumerator, IEnumerator<T>
            {
                private readonly Func<T, bool> predicate;

                private ConcurrentQueue<T> queue = new ConcurrentQueue<T>();

                private ManualResetEvent enqueued = new ManualResetEvent(false);

                private T current;

                public StreamEnumerator(Func<T, bool> predicate)
                {
                    this.predicate = predicate;
                }

                public ConcurrentQueue<T> Queue => this.queue;

                public ManualResetEvent Update => this.enqueued;

                public object Current => this.current;

                T IEnumerator<T>.Current => this.current;

                public void Dispose()
                {
                }

                public bool MoveNext()
                {
                    while (true)
                    {
                        if (this.Queue.TryDequeue(out this.current))
                        {
                            return this.predicate(this.current);
                        }

                        this.Update.WaitOne();
                        if (this.Queue.IsEmpty)
                        {
                            return false;
                        }
                    }
                }

                public void Reset()
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
