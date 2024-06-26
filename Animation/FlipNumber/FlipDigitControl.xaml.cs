﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FlipNumber
{
    /// <summary>
    /// FlipDigitControl.xaml 的交互逻辑
    /// </summary>
    public partial class FlipDigitControl : UserControl, INotifyPropertyChanged
    {


        public static readonly RoutedEvent FlipAnimateTriggerEvent =
            EventManager.RegisterRoutedEvent(
                "FlipAnimateTrigger",
                RoutingStrategy.Bubble,
                typeof(EventHandler<FlipAnimateTriggerRoutedEventArgs>),
                typeof(FlipDigitControl)
            );

        public event RoutedEventHandler FlipAnimateTrigger
        {
            add => AddHandler(FlipAnimateTriggerEvent, value);
            remove => RemoveHandler(FlipAnimateTriggerEvent, value);
        }

        public int CurrentNum 
        {
            get => currentNum;
            set
            {
                if (currentNum != value)
                {
                    currentNum = value;
                    OnPropertyChanged(nameof(CurrentNum));
                }
            }
        }

        public int PrevNum
        {
            get => prevNum;
            set
            {
                if (prevNum != value)
                {
                    prevNum = value;
                    OnPropertyChanged(nameof(PrevNum));
                }
            }
        }

        public bool TriggerAnimate
        {
            get => triggerAnimate;
            set
            {
                if (triggerAnimate != value)
                {
                    triggerAnimate = value;
                    OnPropertyChanged(nameof(TriggerAnimate));
                }
            }
        }

        private int currentNum;
        private int prevNum;

        private bool triggerAnimate;
        private DispatcherTimer timer = new();
        public FlipDigitControl()
        {
            InitializeComponent();
            this.DataContext = this;
            TriggerAnimate = false;
            FlipAnimateTrigger += FlipDigitControl_FlipAnimateTrigger;
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
            AnimateStory.Completed += AnimateStory_Completed;
        }

        private void AnimateStory_Completed(object? sender, EventArgs e)
        {
            TriggerAnimate = true;
            PrevNum = CurrentNum;
        }

        private void FlipDigitControl_FlipAnimateTrigger(object sender, RoutedEventArgs e)
        {
            //AnimateStory.Begin();
            System.Diagnostics.Trace.WriteLine($"Flip Trigger");
            TriggerAnimate = false;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //PrevNum = CurrentNum;
            if (TriggerAnimate)
            {
                if (++CurrentNum > 9)
                {
                    CurrentNum = 0;
                }
                AnimateStory.Begin();
                RaiseEvent(new FlipAnimateTriggerRoutedEventArgs(FlipAnimateTriggerEvent, this));
            }

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class FlipAnimateTriggerRoutedEventArgs : RoutedEventArgs
    {
        public FlipAnimateTriggerRoutedEventArgs(RoutedEvent routedEvent, object source)
            : base(routedEvent, source)
        {
        }

    }
}
