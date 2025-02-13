﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

namespace Microsoft.Psi.Visualization.VisualizationObjects
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Windows.Media;
    using Microsoft.Psi.Visualization.Collections;
    using Microsoft.Psi.Visualization.Config;
    using Microsoft.Psi.Visualization.Data;
    using Microsoft.Psi.Visualization.Navigation;

    /// <summary>
    /// Represents a timeline visualization object.
    /// </summary>
    /// <typeparam name="TData">The type of the timeline visualization.</typeparam>
    /// <typeparam name="TConfig">The type of the timeline visualization object configuration.</typeparam>
    [DataContract(Namespace = "http://www.microsoft.com/psi")]
    public abstract class TimelineVisualizationObject<TData, TConfig> : StreamVisualizationObject<TData, TConfig>
        where TConfig : TimelineVisualizationObjectConfiguration, new()
    {
        private TimeSpan viewDuration;

        /// <summary>
        /// The interval data summarized from the stream data.
        /// </summary>
        private ObservableKeyedCache<DateTime, IntervalData<TData>>.ObservableKeyedView summaryData;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineVisualizationObject{TData, TConfig}"/> class.
        /// </summary>
        public TimelineVisualizationObject()
        {
            this.PropertyChanging += this.TimelineVisualizationObject_PropertyChanging;
            this.PropertyChanged += this.TimelineVisualizationObject_PropertyChanged;
        }

        /// <summary>
        /// Gets or sets the summary data view.
        /// </summary>
        [Browsable(false)]
        [IgnoreDataMember]
        public ObservableKeyedCache<DateTime, IntervalData<TData>>.ObservableKeyedView SummaryData
        {
            get => this.summaryData;
            protected set
            {
                if (this.summaryData != value)
                {
                    var oldValue = this.summaryData;
                    this.Set(nameof(this.SummaryData), ref this.summaryData, value);
                    this.OnSummaryDataChanged(oldValue, this.summaryData);
                }
            }
        }

        /// <summary>
        /// Gets the value of the color to use when displaying in the legend. By default, white.
        /// </summary>
        public virtual Color LegendColor => Colors.White;

        /// <summary>
        /// Gets the value to display in the legend. By default the current value is returned
        /// </summary>
        [IgnoreDataMember]
        public virtual string LegendValue => this.CurrentValue.HasValue ? this.CurrentValue.Value.Data.ToString() : string.Empty;

        /// <summary>
        /// Gets a value indicating whether the visualization object is using summarization.
        /// </summary>
        protected bool IsUsingSummarization => this.Configuration.StreamBinding.SummarizerType != null;

        /// <inheritdoc/>
        public override DateTime? GetSnappedTime(DateTime time)
        {
            if (this.IsUsingSummarization)
            {
                return this.GetTimeOfNearestMessage(time, this.SummaryData?.Count ?? 0, (idx) => this.SummaryData[idx].OriginatingTime);
            }
            else
            {
                return base.GetSnappedTime(time);
            }
        }

        /// <inheritdoc/>
        protected override void OnCursorModeChanged(object sender, CursorModeChangedEventArgs cursorModeChangedEventArgs)
        {
            // If we changed from or to live mode, and we're currently bound to a datasource, then refresh the data or summaries
            if (this.IsBound && cursorModeChangedEventArgs.OriginalValue != cursorModeChangedEventArgs.NewValue)
            {
                if ((cursorModeChangedEventArgs.OriginalValue == CursorMode.Live) || (cursorModeChangedEventArgs.NewValue == CursorMode.Live))
                {
                    this.RefreshData();
                }
            }

            base.OnCursorModeChanged(sender, cursorModeChangedEventArgs);
        }

        /// <inheritdoc />
        protected override void OnConnect()
        {
            base.OnConnect();
            this.Panel.PropertyChanged += this.OnPanelPropertyChanged;
        }

        /// <inheritdoc />
        protected override void OnDisconnect()
        {
            base.OnDisconnect();
            this.Panel.PropertyChanged -= this.OnPanelPropertyChanged;
        }

        /// <inheritdoc />
        protected override void OnStreamBound()
        {
            base.OnStreamBound();
            this.Navigator.ViewRange.RangeChanged += this.OnViewRangeChanged;
            this.OnViewRangeChanged(
                this.Navigator.ViewRange,
                new NavigatorTimeRangeChangedEventArgs(this.Navigator.ViewRange.StartTime, this.Navigator.ViewRange.StartTime, this.Navigator.ViewRange.EndTime, this.Navigator.ViewRange.EndTime));
        }

        /// <inheritdoc />
        protected override void OnStreamUnbound()
        {
            base.OnStreamUnbound();
            this.Navigator.ViewRange.RangeChanged -= this.OnViewRangeChanged;
            this.SummaryData = null;
        }

        /// <summary>
        /// Invoked when a <see cref="TimelineVisualizationObject{TData, TConfig}"/> property is changing.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The PropertyChangingEventArgs</param>
        protected void TimelineVisualizationObject_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == nameof(this.CurrentValue))
            {
                this.RaisePropertyChanging(nameof(this.LegendValue));
            }
        }

        /// <summary>
        /// Invoked when a <see cref="TimelineVisualizationObject{TData, TConfig}"/> property changes.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The PropertyChangingEventArgs</param>
        protected void TimelineVisualizationObject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.CurrentValue))
            {
                this.RaisePropertyChanged(nameof(this.LegendValue));
            }
        }

        /// <summary>
        /// Invoked when the <see cref="TimelineVisualizationObject{TData, TConfig}.SummaryData"/> property changes.
        /// </summary>
        /// <param name="oldValue">The old summary data value.</param>
        /// <param name="newValue">The new summary data value.</param>
        protected virtual void OnSummaryDataChanged(
            ObservableKeyedCache<DateTime, IntervalData<TData>>.ObservableKeyedView oldValue,
            ObservableKeyedCache<DateTime, IntervalData<TData>>.ObservableKeyedView newValue)
        {
            if (oldValue != null)
            {
                oldValue.DetailedCollectionChanged -= this.SummaryData_CollectionChanged;
            }

            if (newValue != null)
            {
                newValue.DetailedCollectionChanged += this.SummaryData_CollectionChanged;
            }

            this.OnSummaryDataCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        /// Invoked when the <see cref="TimelineVisualizationObject{TData, TConfig}.SummaryData"/> collection changes.
        /// </summary>
        /// <param name="e">Collection changed event arguments.</param>
        protected virtual void OnSummaryDataCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            // see if we are still active
            if (this.Container == null)
            {
                return;
            }

            if (this.Navigator.IsCursorModePlayback)
            {
                IntervalData<TData> last = this.SummaryData.LastOrDefault();
                if (last != default(IntervalData<TData>))
                {
                    this.CurrentValue = Message.Create(last.Value, last.OriginatingTime, last.EndTime, 0, 0);
                }
            }
        }

        /// <inheritdoc />
        protected override void SetCurrentValue(DateTime currentTime)
        {
            if (this.SummaryData != null)
            {
                int index = this.GetIndexForTime(currentTime, this.SummaryData?.Count ?? 0, (idx) => this.SummaryData[idx].OriginatingTime);
                if (index != -1)
                {
                    var interval = this.SummaryData[index];
                    this.CurrentValue = Message.Create(interval.Value, interval.OriginatingTime, interval.EndTime, 0, 0);
                }
                else
                {
                    base.SetCurrentValue(currentTime);
                }
            }
            else
            {
                int index = this.GetIndexForTime(currentTime, this.Data?.Count ?? 0, (idx) => this.Data[idx].OriginatingTime);
                if (index != -1)
                {
                    this.CurrentValue = this.Data[index];
                }
                else
                {
                    base.SetCurrentValue(currentTime);
                }
            }
        }

        private void OnViewRangeChanged(object sender, NavigatorTimeRangeChangedEventArgs e)
        {
            this.RefreshData();
        }

        private void RefreshData()
        {
            // Check that we're actually bound to a store
            if (this.Configuration.StreamBinding.IsBound)
            {
                if (this.Navigator.CursorMode == CursorMode.Live)
                {
                    if (this.viewDuration != this.Navigator.ViewRange.Duration)
                    {
                        this.viewDuration = this.Navigator.ViewRange.Duration;

                        if (this.IsUsingSummarization)
                        {
                            // If performing summarization, recompute the sampling tick interval (i.e. summarization interval)
                            // whenever the view range duration has changed.
                            this.Configuration.SamplingTicks = (long)(this.viewDuration.Ticks / this.Panel.Width);
                            this.RefreshSummaryData();
                        }
                        else
                        {
                            // Not summarizing, so read data directly from the stream
                            this.Data = DataManager.Instance.ReadStream<TData>(this.Configuration.StreamBinding, last => last - this.viewDuration);
                        }
                    }
                }
                else
                {
                    this.viewDuration = this.Navigator.ViewRange.Duration;

                    if (this.IsUsingSummarization)
                    {
                        // If performing summarization, recompute the sampling tick interval (i.e. summarization interval)
                        // whenever the view range duration has changed.
                        this.Configuration.SamplingTicks = (long)(this.viewDuration.Ticks / this.Panel.Width);
                        this.RefreshSummaryData();
                    }
                    else
                    {
                        // Not summarizing, so read data directly from the stream - note that end time is exclusive, so adding one tick to ensure any message at EndTime is included
                        this.Data = DataManager.Instance.ReadStream<TData>(this.Configuration.StreamBinding, this.Navigator.DataRange.StartTime, this.Navigator.DataRange.EndTime.AddTicks(1));
                    }
                }
            }
            else
            {
                this.Data = null;
                this.SummaryData = null;
                this.RefreshSummaryData();
            }
        }

        private void OnPanelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.Panel.Width))
            {
                if (this.Configuration.StreamBinding?.SummarizerType != null)
                {
                    this.Configuration.SamplingTicks = (long)(this.viewDuration.Ticks / this.Panel.Width);
                    this.RefreshSummaryData();
                }
            }
        }

        private void RefreshSummaryData()
        {
            // Check that we're actually bound to a store
            if (this.Configuration.StreamBinding.IsBound)
            {
                if (this.Navigator.CursorMode == CursorMode.Live)
                {
                    this.SummaryData = DataManager.Instance.ReadSummary<TData>(
                        this.Configuration.StreamBinding,
                        TimeSpan.FromTicks(this.Configuration.SamplingTicks),
                        last => last - this.viewDuration);
                }
                else
                {
                    var startTime = this.Navigator.ViewRange.StartTime;
                    var endTime = this.Navigator.ViewRange.EndTime;

                    // Attempt to read a little extra data outside the view range so that the end
                    // points appear to connect to the next/previous values. This is flawed as we
                    // are just guessing how much to extend the time interval by. What we really
                    // need is for the DataManager to give us everything in the requested time
                    // interval plus the next/previous data point just outside the interval.
                    var extra = TimeSpan.FromMilliseconds(100);

                    this.SummaryData = DataManager.Instance.ReadSummary<TData>(
                        this.Configuration.StreamBinding,
                        startTime > DateTime.MinValue + extra ? startTime - extra : startTime,
                        endTime < DateTime.MaxValue - extra ? endTime + extra : endTime,
                        TimeSpan.FromTicks(this.Configuration.SamplingTicks));
                }
            }
            else
            {
                this.SummaryData = null;
            }
        }

        private void SummaryData_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnSummaryDataCollectionChanged(e);
        }
    }
}
