using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using TouchScreenKeyboard.LogicalKeys;

namespace TouchScreenKeyboard.Controls
{
    public class OnScreenKeyEventArgs : RoutedEventArgs
    {
        public OnScreenKey OnScreenKey { get; private set; }

        public OnScreenKeyEventArgs(RoutedEvent routedEvent, OnScreenKey onScreenKey)
            : base(routedEvent)
        {
            OnScreenKey = onScreenKey;
        }
    }

    public delegate void OnScreenKeyEventHandler(DependencyObject sender, OnScreenKeyEventArgs e);

    public class OnScreenKey : Border
    {
        public static readonly DependencyProperty KeyProperty = DependencyProperty.Register("Key", typeof(ILogicalKey), typeof(OnScreenKey), new UIPropertyMetadata(null, OnKeyChanged));
        public static readonly DependencyProperty AreAnimationsEnabledProperty = DependencyProperty.Register("AreAnimationsEnabled", typeof(bool), typeof(OnScreenKey), new UIPropertyMetadata(true));
        public static readonly DependencyProperty IsMouseOverAnimationEnabledProperty = DependencyProperty.Register("IsMouseOverAnimationEnabled", typeof(bool), typeof(OnScreenKey), new UIPropertyMetadata(false));
        public static readonly DependencyProperty IsOnScreenKeyDownProperty = DependencyProperty.Register("IsOnScreenKeyDown", typeof(bool), typeof(OnScreenKey), new UIPropertyMetadata(false));
        public static readonly DependencyProperty GridWidthProperty = DependencyProperty.Register("GridWidth", typeof(GridLength), typeof(OnScreenKey), new UIPropertyMetadata(new GridLength(1, GridUnitType.Star)));


        public static readonly RoutedEvent PreviewOnScreenKeyDownEvent = EventManager.RegisterRoutedEvent("PreviewOnScreenKeyDown", RoutingStrategy.Direct, typeof(OnScreenKeyEventHandler), typeof(OnScreenKey));
        public static readonly RoutedEvent PreviewOnScreenKeyUpEvent = EventManager.RegisterRoutedEvent("PreviewOnScreenKeyUp", RoutingStrategy.Direct, typeof(OnScreenKeyEventHandler), typeof(OnScreenKey));
        public static readonly RoutedEvent OnScreenKeyDownEvent = EventManager.RegisterRoutedEvent("OnScreenKeyDown", RoutingStrategy.Direct, typeof(OnScreenKeyEventHandler), typeof(OnScreenKey));
        public static readonly RoutedEvent OnScreenKeyUpEvent = EventManager.RegisterRoutedEvent("OnScreenKeyUp", RoutingStrategy.Direct, typeof(OnScreenKeyEventHandler), typeof(OnScreenKey));
        public static readonly RoutedEvent OnScreenKeyPressEvent = EventManager.RegisterRoutedEvent("OnScreenKeyPress", RoutingStrategy.Direct, typeof(OnScreenKeyEventHandler), typeof(OnScreenKey));


        private Border _keySurface;
        private Border _mouseDownSurface;
        private TextBlock _keyText;

        private GradientBrush _keySurfaceBrush
        {
            get
            {
                LinearGradientBrush brushForbg = new LinearGradientBrush();
                brushForbg.EndPoint = new Point(0, 1);
                brushForbg.StartPoint = new Point(0, 0);
                GradientStop gsBg1 = new GradientStop();
                gsBg1.Color = Color.FromArgb(100, 243, 243, 243);
                gsBg1.Offset = 0;
                GradientStop gsBg2 = new GradientStop();
                gsBg2.Color = Color.FromArgb(100, 235, 235, 235);
                gsBg2.Offset = 0.5;

                GradientStop gsBg3 = new GradientStop();
                gsBg3.Color = Color.FromArgb(100, 221, 221, 221);
                gsBg3.Offset = 0.5;
                GradientStop gsBg4 = new GradientStop();
                gsBg4.Color = Color.FromArgb(100, 76, 85, 98);
                gsBg4.Offset = 1;

                GradientStopCollection gsc2 = new GradientStopCollection();
                gsc2.Add(gsBg1);
                gsc2.Add(gsBg2);
                brushForbg.GradientStops = gsc2;

                return brushForbg;
            }
        }

        private GradientBrush _keySurfaceBorderBrush
        {
            get
            {
                LinearGradientBrush brushForbg = new LinearGradientBrush();
                brushForbg.EndPoint = new Point(0.5, 1);
                brushForbg.StartPoint = new Point(0.5, 0);
                GradientStop gsBg1 = new GradientStop();
                gsBg1.Color = Color.FromArgb(100, 103, 112, 125);
                gsBg1.Offset = 1;
                GradientStop gsBg2 = new GradientStop();
                gsBg2.Color = Color.FromArgb(100, 145, 153, 164);


                GradientStopCollection gsc2 = new GradientStopCollection();
                gsc2.Add(gsBg1);
                gsc2.Add(gsBg2);
                brushForbg.GradientStops = gsc2;

                return brushForbg;
            }
        }

        private GradientBrush _keySurfaceMouseOverBorderBrush
        {
            get
            {
                LinearGradientBrush brushForbg = new LinearGradientBrush();
                brushForbg.EndPoint = new Point(0.5, 1);
                brushForbg.StartPoint = new Point(0.5, 0);
                GradientStop gsBg1 = new GradientStop();
                gsBg1.Color = Color.FromArgb(100, 0, 0, 0);
                gsBg1.Offset = 0;
                GradientStop gsBg2 = new GradientStop();
                gsBg2.Color = Color.FromArgb(100, 255, 255, 255);
                gsBg2.Offset = 1;

                GradientStopCollection gsc2 = new GradientStopCollection();
                gsc2.Add(gsBg1);
                gsc2.Add(gsBg2);
                brushForbg.GradientStops = gsc2;

                return brushForbg;
            }
        }

        private GradientBrush _keySurfaceMouseOverBrush
        {
            get
            {
                LinearGradientBrush brushForbg = new LinearGradientBrush();
                brushForbg.EndPoint = new Point(0.5, 1);
                brushForbg.StartPoint = new Point(0.5, 0);
                GradientStop gsBg1 = new GradientStop();
                gsBg1.Color = Color.FromArgb(100, 103, 112, 125);
                gsBg1.Offset = 1;
                GradientStop gsBg2 = new GradientStop();
                gsBg2.Color = Color.FromArgb(100, 145, 153, 164);


                GradientStopCollection gsc2 = new GradientStopCollection();
                gsc2.Add(gsBg1);
                gsc2.Add(gsBg2);
                brushForbg.GradientStops = gsc2;

                return brushForbg;
            }
        }

        //    private readonly SolidColorBrush _keyOutsideBorderBrush = new SolidColorBrush(Color.FromArgb(26, 26, 26, 26));
        private GradientBrush _keyOutsideBorderBrush
        {
            get
            {
                LinearGradientBrush brushForbg = new LinearGradientBrush();
                brushForbg.EndPoint = new Point(0.5, 1);
                brushForbg.StartPoint = new Point(0.5, 0);
                GradientStop gsBg1 = new GradientStop();
                gsBg1.Color = Color.FromArgb(100, 164, 173, 185);
                gsBg1.Offset = 0; GradientStop
                gsBg2 = new GradientStop();
                gsBg2.Color = Color.FromArgb(100, 76, 85, 98);
                gsBg2.Offset = 0.287;

                GradientStopCollection gsc2 = new GradientStopCollection();
                gsc2.Add(gsBg1);
                gsc2.Add(gsBg2);

                brushForbg.GradientStops = gsc2;

                return brushForbg;
            }
        }



        public ILogicalKey Key
        {
            get { return (ILogicalKey)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }

        public bool AreAnimationsEnabled
        {
            get { return (bool)GetValue(AreAnimationsEnabledProperty); }
            set { SetValue(AreAnimationsEnabledProperty, value); }
        }

        public bool IsMouseOverAnimationEnabled
        {
            get { return (bool)GetValue(IsMouseOverAnimationEnabledProperty); }
            set { SetValue(IsMouseOverAnimationEnabledProperty, value); }
        }

        public bool IsOnScreenKeyDown
        {
            get { return (bool)GetValue(IsOnScreenKeyDownProperty); }
            private set { SetValue(IsOnScreenKeyDownProperty, value); }
        }

        public int GridRow
        {
            get { return (int)GetValue(Grid.RowProperty); }
            set { SetValue(Grid.RowProperty, value); }
        }

        public int GridColumn
        {
            get { return (int)GetValue(Grid.ColumnProperty); }
            set { SetValue(Grid.ColumnProperty, value); }
        }

        public GridLength GridWidth
        {
            get { return (GridLength)GetValue(GridWidthProperty); }
            set { SetValue(GridWidthProperty, value); }
        }
        

        
        protected static void OnKeyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((OnScreenKey)sender).SetupControl((ILogicalKey)e.NewValue);
        }

        public event OnScreenKeyEventHandler PreviewOnScreenKeyDown
        {
            add { AddHandler(PreviewOnScreenKeyDownEvent, value); }
            remove { RemoveHandler(PreviewOnScreenKeyDownEvent, value); }
        }

        protected OnScreenKeyEventArgs RaisePreviewOnScreenKeyDownEvent()
        {
            var args = new OnScreenKeyEventArgs(PreviewOnScreenKeyDownEvent, this);
            RaiseEvent(args);
            return args;
        }

        public event OnScreenKeyEventHandler PreviewOnScreenKeyUp
        {
            add { AddHandler(PreviewOnScreenKeyUpEvent, value); }
            remove { RemoveHandler(PreviewOnScreenKeyUpEvent, value); }
        }

        protected OnScreenKeyEventArgs RaisePreviewOnScreenKeyUpEvent()
        {
            var args = new OnScreenKeyEventArgs(PreviewOnScreenKeyUpEvent, this);
            RaiseEvent(args);
            return args;
        }

        public event OnScreenKeyEventHandler OnScreenKeyDown
        {
            add { AddHandler(OnScreenKeyDownEvent, value); }
            remove { RemoveHandler(OnScreenKeyDownEvent, value); }
        }

        protected OnScreenKeyEventArgs RaiseOnScreenKeyDownEvent()
        {
            var args = new OnScreenKeyEventArgs(OnScreenKeyDownEvent, this);
            RaiseEvent(args);
            return args;
        }

        public event OnScreenKeyEventHandler OnScreenKeyUp
        {
            add { AddHandler(OnScreenKeyUpEvent, value); }
            remove { RemoveHandler(OnScreenKeyUpEvent, value); }
        }

        protected OnScreenKeyEventArgs RaiseOnScreenKeyUpEvent()
        {
            var args = new OnScreenKeyEventArgs(OnScreenKeyUpEvent, this);
            RaiseEvent(args);
            return args;
        }

        public event OnScreenKeyEventHandler OnScreenKeyPress
        {
            add { AddHandler(OnScreenKeyPressEvent, value); }
            remove { RemoveHandler(OnScreenKeyPressEvent, value); }
        }

        protected OnScreenKeyEventArgs RaiseOnScreenKeyPressEvent()
        {
            var args = new OnScreenKeyEventArgs(OnScreenKeyPressEvent, this);
            RaiseEvent(args);
            return args;
        }

        private void SetupControl(ILogicalKey key)
        {
            CornerRadius = new CornerRadius(3);
            BorderBrush = _keyOutsideBorderBrush;
            BorderThickness = new Thickness(1);
            SnapsToDevicePixels = true;

            var g = new Grid();
            Child = g;

            _keySurface = new Border
                              {
                                  CornerRadius = new CornerRadius(3),
                                  BorderBrush = _keySurfaceBorderBrush,
                                  BorderThickness = new Thickness(1),
                                  Background = _keySurfaceBrush,
                                  SnapsToDevicePixels = true
                              };
            g.Children.Add(_keySurface);

            _mouseDownSurface = new Border
                                    {
                                        CornerRadius = new CornerRadius(3),
                                        BorderBrush = _keySurfaceMouseOverBorderBrush,
                                        Opacity = 0,
                                        Background = _keySurfaceMouseOverBrush,
                                        SnapsToDevicePixels = true
                                    };
            g.Children.Add(_mouseDownSurface);

            _keyText = new TextBlock
                           {
                               Margin = new Thickness(3, 0, 0, 0),
                               FontSize = 20,
                               HorizontalAlignment = HorizontalAlignment.Center,
                               VerticalAlignment = VerticalAlignment.Center,
                               Foreground = Brushes.Black,
                               SnapsToDevicePixels = true
                           };
            _keyText.SetBinding(TextBlock.TextProperty, new Binding("DisplayName") { Source = key });
            g.Children.Add(_keyText);

            key.PropertyChanged += Key_PropertyChanged;
            key.LogicalKeyPressed += Key_VirtualKeyPressed;
        }

        void Key_VirtualKeyPressed(object sender, LogicalKeyEventArgs e)
        {
            RaiseOnScreenKeyPressEvent();
        }

        void Key_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Key is ModifierKeyBase && e.PropertyName == "IsInEffect")
            {
                var key = ((ModifierKeyBase)Key);
                if (key.IsInEffect)
                {
                    AnimateMouseDown();
                }
                else
                {
                    AnimateMouseUp();
                }
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            HandleKeyDown();
            base.OnMouseDown(e);
        }

        protected void HandleKeyDown()
        {
            var args = RaisePreviewOnScreenKeyDownEvent();
            if (args.Handled == false)
            {
                IsOnScreenKeyDown = true;
                AnimateMouseDown();
                Key.Press();
            }
            RaiseOnScreenKeyDownEvent();
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            HandleKeyUp();
            base.OnMouseUp(e);
        }

        private void HandleKeyUp()
        {
            var args = RaisePreviewOnScreenKeyUpEvent();
            if (args.Handled == false)
            {
                IsOnScreenKeyDown = false;
                AnimateMouseUp();
            }
            RaiseOnScreenKeyUpEvent();
        }

        private void AnimateMouseDown()
        {
            _mouseDownSurface.BeginAnimation(OpacityProperty, new DoubleAnimation(1, new Duration(TimeSpan.Zero)));
            _keyText.Foreground = _keyOutsideBorderBrush;
        }

        private void AnimateMouseUp()
        {
            if ((Key is TogglingModifierKey || Key is InstantaneousModifierKey) && ((ModifierKeyBase)Key).IsInEffect) return;
            _keySurface.BorderBrush = _keySurfaceBorderBrush;
            _keyText.Foreground = Brushes.Black;
            if (!AreAnimationsEnabled || Key is TogglingModifierKey || Key is InstantaneousModifierKey)
            {
                _mouseDownSurface.BeginAnimation(OpacityProperty, new DoubleAnimation(0, new Duration(TimeSpan.Zero)));
            }
            else
            {
                _mouseDownSurface.BeginAnimation(OpacityProperty, new DoubleAnimation(0, Duration.Automatic));
            }
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            if (IsMouseOverAnimationEnabled)
            {
                _keySurface.Background = _keySurfaceMouseOverBrush;
                _keySurface.BorderBrush = _keySurfaceMouseOverBorderBrush;
            }
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            if (IsMouseOverAnimationEnabled)
            {
                if (Key is TogglingModifierKey && ((ModifierKeyBase)Key).IsInEffect) return;
                _keySurface.Background = _keySurfaceBrush;
                _keySurface.BorderBrush = _keySurfaceBorderBrush;
            }
            if (IsOnScreenKeyDown)
            {
                HandleKeyUp();
            }
            base.OnMouseLeave(e);
        }
    }
}




















