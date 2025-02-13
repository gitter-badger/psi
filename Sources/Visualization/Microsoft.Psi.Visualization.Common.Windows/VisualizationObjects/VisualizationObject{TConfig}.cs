﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace Microsoft.Psi.Visualization.VisualizationObjects
{
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using Microsoft.Psi.Visualization.Config;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Represents a base class for visualization objects.
    /// </summary>
    /// <typeparam name="TConfig">The type of the visualization object configuration.</typeparam>
    [DataContract(Namespace = "http://www.microsoft.com/psi")]
    public abstract class VisualizationObject<TConfig> : VisualizationObject
        where TConfig : VisualizationObjectConfiguration, new()
    {
        private TConfig configuration;

        /// <summary>
        /// Gets or sets the visualization object configuration.
        /// </summary>
        [DataMember]
        public TConfig Configuration
        {
            get => this.configuration;
            set
            {
                if (this.configuration != null)
                {
                    this.configuration.PropertyChanged -= this.OnConfigurationPropertyChanged;
                }

                this.Set(nameof(this.Configuration), ref this.configuration, value);

                this.OnConfigurationChanged();

                if (this.configuration != null)
                {
                    this.configuration.PropertyChanged += this.OnConfigurationPropertyChanged;
                }
            }
        }

        /// <summary>
        /// Gets the contract resolver. Default is null.
        /// </summary>
        [Browsable(false)]
        [IgnoreDataMember]
        protected virtual IContractResolver ContractResolver => null;

        /// <inheritdoc />
        protected override void InitNew()
        {
            base.InitNew();
            this.Configuration = new TConfig();
        }

        [OnDeserializing]
        private void OnDeserializing(StreamingContext c)
        {
            this.InitNew();
        }
    }
}
