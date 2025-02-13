﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace Microsoft.Psi.Visualization.Views.Visuals2D
{
    using Microsoft.Psi.Visualization.Config;
    using Microsoft.Psi.Visualization.VisualizationObjects;

    /// <summary>
    /// Interaction logic for AudioVisualizationObjectView.xaml
    /// </summary>
    public partial class AudioVisualizationObjectView : PlotVisualizationObjectView<AudioVisualizationObject, AudioVisualizationObjectConfiguration>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioVisualizationObjectView"/> class.
        /// </summary>
        public AudioVisualizationObjectView()
        {
            this.InitializeComponent();
            this.Canvas = this._DynamicCanvas;
        }
    }
}
