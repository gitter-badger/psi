﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace Microsoft.Psi.Visualization.Config
{
    using System.Runtime.Serialization;
    using System.Windows.Media;

    /// <summary>
    /// Represents a Kinect bodies 3D visualization object configuration.
    /// </summary>
    [DataContract(Namespace = "http://www.microsoft.com/psi")]
    public class KinectBodies3DVisualizationObjectConfiguration : Instant3DVisualizationObjectConfiguration
    {
        private Color color = Colors.White;
        private double inferredJointsOpacity = 0;
        private double size = 0.03;
        private bool showTrackingBillboards = false;

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        [DataMember]
        public Color Color
        {
            get { return this.color; }
            set { this.Set(nameof(this.Color), ref this.color, value); }
        }

        /// <summary>
        /// Gets or sets the inferred joints opacity.
        /// </summary>
        [DataMember]
        public double InferredJointsOpacity
        {
            get { return this.inferredJointsOpacity; }
            set { this.Set(nameof(this.InferredJointsOpacity), ref this.inferredJointsOpacity, value); }
        }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        [DataMember]
        public double Size
        {
            get { return this.size; }
            set { this.Set(nameof(this.Size), ref this.size, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show tracking billboards
        /// </summary>
        [DataMember]
        public bool ShowTrackingBillboards
        {
            get { return this.showTrackingBillboards; }
            set { this.Set(nameof(this.ShowTrackingBillboards), ref this.showTrackingBillboards, value); }
        }
    }
}
