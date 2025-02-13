﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace Microsoft.Psi.Interop.Transport
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.Psi.Components;
    using Microsoft.Psi.Interop.Serialization;

    /// <summary>
    /// Persisted file source component.
    /// </summary>
    /// <typeparam name="T">Message type</typeparam>
    public class FileSource<T> : Generator<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileSource{T}"/> class.
        /// </summary>
        /// <param name="pipeline">Pipeline to which this component belongs</param>
        /// <param name="filename">File name to which to persist</param>
        /// <param name="deserializer">Format serializer with which messages are deserialized</param>
        public FileSource(Pipeline pipeline, string filename, IPersistentFormatDeserializer deserializer)
            : base(pipeline, EnumerateFile(pipeline, filename, deserializer))
        {
        }

        private static IEnumerator<(T, DateTime)> EnumerateFile(Pipeline pipeline, string filename, IPersistentFormatDeserializer deserializer)
        {
            using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                foreach (var record in deserializer.DeserializeRecords(stream))
                {
                    yield return ((T)record.Item1, record.Item2);
                }
            }
        }
    }
}
