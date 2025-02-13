﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

#pragma warning disable SA1118 // Parameter must not span multiple lines

namespace Microsoft.Psi.Samples.WpfSample
{
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Pipeline pipeline;

        public MainWindow()
        {
            this.InitializeComponent();
            this.DispImage = new DisplayImage();
            this.DataContext = this;
            this.Closing += this.MainWindow_Closing;

            // Setup our Psi pipeline
            this.SetupPsi();
        }

        // Define a property that exposes our DisplayImage so that WPF can access it.
        public DisplayImage DispImage { get; set; }

        /// <summary>
        /// SetupPsi() is called at application startup. It is responsible for
        /// building and starting the Psi pipeline
        /// </summary>
        public void SetupPsi()
        {
            // First create the pipeline object.
            this.pipeline = Pipeline.Create();

            // Next register an event handler to catch pipeline errors
            this.pipeline.PipelineCompleted += this.Pipeline_PipelineCompleted;

            // Create our webcam
            Media.MediaCapture webcam = new Media.MediaCapture(this.pipeline, 1920, 1080, 30, true);

            // Bind the webcam's output to our display image.
            // The "Do" operator is executed on each sample from the stream (webcam.Out), which are the images coming from the webcam
            webcam.Out.Do(
                (img, e) =>
                {
                    // Update our UI image with the Psi image
                    this.DispImage.UpdateImage(img);
                });
            if (webcam.Audio != null)
            {
                var player = new Audio.AudioPlayer(
                    this.pipeline,
                    new Audio.AudioPlayerConfiguration()
                    {
                        InputFormat = Audio.WaveFormat.Create16BitPcm(48000, 2),
                    });
                webcam.Audio.PipeTo(player.In);
            }

            // Finally start the pipeline running
            try
            {
                this.pipeline.RunAsync();
            }
            catch (AggregateException exp)
            {
                MessageBox.Show("Error! " + exp.InnerException.Message);
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Dispose of the pipeline to shut it down and exit clean
            this.pipeline?.Dispose();
            this.pipeline = null;
        }

        /// <summary>
        /// <see cref="Pipeline_PipelineCompleted"/> is called when the pipeline finishes running
        /// </summary>
        /// <param name="sender">Object that sent this event</param>
        /// <param name="e">Pipeline event arguments. Primarily used to get any pipeline errors back.</param>
        private void Pipeline_PipelineCompleted(object sender, PipelineCompletedEventArgs e)
        {
            if (e.Errors.Count > 0)
            {
                MessageBox.Show("Error! " + e.Errors[0].Message);
            }
        }
    }
}

#pragma warning restore SA1118 // Parameter must not span multiple lines
