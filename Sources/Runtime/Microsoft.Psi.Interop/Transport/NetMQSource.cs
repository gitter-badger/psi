﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace Microsoft.Psi.Interop.Transport
{
    using System;
    using Microsoft.Psi.Components;
    using Microsoft.Psi.Interop.Serialization;
    using NetMQ;
    using NetMQ.Sockets;

    /// <summary>
    /// NetMQ (ZeroMQ) subscriber component.
    /// </summary>
    /// <typeparam name="T">Message type</typeparam>
    public class NetMQSource<T> : IProducer<T>, ISourceComponent, IDisposable
    {
        private readonly string topic;
        private readonly string address;
        private readonly IFormatDeserializer deserializer;

        private SubscriberSocket socket;
        private NetMQPoller poller;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetMQSource{T}"/> class.
        /// </summary>
        /// <param name="pipeline">Pipeline to which this component belongs</param>
        /// <param name="topic">Topic name</param>
        /// <param name="address">Connection string</param>
        /// <param name="deserializer">Format deserializer with which messages are deserialized</param>
        public NetMQSource(Pipeline pipeline, string topic, string address, IFormatDeserializer deserializer)
        {
            this.topic = topic;
            this.address = address;
            this.deserializer = deserializer;
            this.Out = pipeline.CreateEmitter<T>(this, topic);
        }

        /// <inheritdoc />
        public Emitter<T> Out { get; }

        /// <inheritdoc />
        public void Dispose()
        {
            this.Stop();
        }

        /// <inheritdoc/>
        public void Start(Action<DateTime> notifyCompletionTime)
        {
            // notify that this is an infinite source component
            notifyCompletionTime(DateTime.MaxValue);

            this.socket = new SubscriberSocket();
            this.socket.Connect(this.address);
            this.socket.Subscribe(this.topic);
            this.socket.ReceiveReady += this.ReceiveReady;
            this.poller = new NetMQPoller();
            this.poller.Add(this.socket);
            this.poller.RunAsync();
        }

        /// <inheritdoc/>
        public void Stop()
        {
            if (this.socket != null)
            {
                this.poller.Dispose();
                this.socket.Dispose();
                this.socket = null;
            }
        }

        private void ReceiveReady(object sender, NetMQSocketEventArgs e)
        {
            while (this.socket.TryReceiveFrameString(out var ignoreTopicName))
            {
                var bytes = this.socket.ReceiveFrameBytes();
                var (message, originatingTime) = this.deserializer.DeserializeMessage(bytes, 0, bytes.Length);
                this.Out.Post(message, originatingTime);
            }
        }
    }
}
