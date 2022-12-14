using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Chrome;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using Avalonia.VisualTree;
using System;
using System.Reactive.Linq;
using System.Runtime.InteropServices;

namespace Avalonia.Boilerplate {
    public class MainWindow : Window {
        private readonly IDisposable windowDecorationMarginSubscription;

        public MainWindow() {
            /*windowDecorationMarginSubscription = this.GetObservable(WindowStateProperty).Subscribe(property => {
                if (property == WindowState.Maximized)
                {
                    ExtendClientAreaTitleBarHeightHint = 41;
                } else
                {
                    ExtendClientAreaTitleBarHeightHint = 34;
                }
                
            });*/
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                windowDecorationMarginSubscription = this.GetObservable(WindowStateProperty).Subscribe(state => {
                    if (state == WindowState.Maximized)
                    {
                        ExtendClientAreaTitleBarHeightHint = 41; // 34 + Margin (7px)
                    }
                    else
                    {
                        ExtendClientAreaTitleBarHeightHint = 34;
                    }

                });
            } else
            {
                ExtendClientAreaTitleBarHeightHint = 34;
            }
            

            //ExtendClientAreaTitleBarHeightHint = WindowState == WindowState.Maximized ? 48 : 34;
            //ExtendClientAreaTitleBarHeightHint = 34;
            InitializeComponent();
#if DEBUG
            this.AttachDevTools(new Input.KeyGesture(Input.Key.F8));
#endif
            
            this.FindControl<Button>("Dialog").Click += delegate
            {
                var window = CreateSampleWindow();
                window.Height = 200;
                window.Show(GetWindow());
            };
        }


        private Window CreateSampleWindow() {
            var window = new Window
            {
                Height = 200,
                Width = 200,
                Content = new StackPanel
                {
                    Spacing = 4,
                    Children =
                    {
                        new TextBlock { Text = "Hello world!" }
                    }
                },
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            return window;
        }
        
        Window GetWindow() => (Window)this.VisualRoot;

        private void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
            RefreshExtendClientAreaChromeHints();
            PseudoClasses.Set(":win", RuntimeInformation.IsOSPlatform(OSPlatform.Windows));
        }

        protected void RefreshExtendClientAreaChromeHints(bool isTitleBarVisible = true)
        {
            ExtendClientAreaChromeHints = isTitleBarVisible
                ? ExtendClientAreaChromeHints.PreferSystemChrome
                : ExtendClientAreaChromeHints.NoChrome;
        }

        public static readonly StyledProperty<bool> EnableCloseProperty =
            AvaloniaProperty.Register<MainWindow, bool>(nameof(EnableClose), defaultValue: true, inherits: true);

        public bool EnableClose
        {
            get => GetValue(EnableCloseProperty);
            set => SetValue(EnableCloseProperty, value);
        }

        public static readonly StyledProperty<bool> AllowMinimizeProperty =
           AvaloniaProperty.Register<MainWindow, bool>(nameof(AllowMinimize), defaultValue: true, inherits: true);

        public bool AllowMinimize
        {
            get => GetValue(AllowMinimizeProperty);
            set => SetValue(AllowMinimizeProperty, value);
        }

        private static bool CanRestoreOrMaximize(IAvaloniaObject window, bool allowRestoreOrMaximize)
        {
            return allowRestoreOrMaximize && ((MainWindow)window).CanResize;
        }

        public static readonly StyledProperty<bool> AllowRestoreOrMaximizeProperty =
           AvaloniaProperty.Register<MainWindow, bool>(nameof(AllowRestoreOrMaximize), defaultValue: true, inherits: true, coerce: CanRestoreOrMaximize);

        public bool AllowRestoreOrMaximize
        {
            get => GetValue(AllowRestoreOrMaximizeProperty);
            set => SetValue(AllowRestoreOrMaximizeProperty, value);
        }

        public static readonly StyledProperty<bool> ShowCloseProperty =
           AvaloniaProperty.Register<MainWindow, bool>(nameof(ShowClose), defaultValue: true, inherits: true);

        public bool ShowClose
        {
            get => GetValue(ShowCloseProperty);
            set => SetValue(ShowCloseProperty, value);
        }

        public static readonly StyledProperty<bool> ShowPopInProperty =
           AvaloniaProperty.Register<MainWindow, bool>(nameof(ShowPopIn), defaultValue: false, inherits: true);

        public bool ShowPopIn
        {
            get => GetValue(ShowPopInProperty);
            set => SetValue(ShowPopInProperty, value);
        }

        /*protected override void HandleWindowStateChanged(WindowState state)
        {
            base.HandleWindowStateChanged(state);
            if (state == WindowState.Maximized)
            {
                ExtendClientAreaTitleBarHeightHint = 48; // 34 + 7
                var titleBar = this.FindDescendantOfType<TitleBar>();
                titleBar.Margin = new Thickness(titleBar.Margin.Left, titleBar.Margin.Top, titleBar.Margin.Right, 0);
            }
            else
            {
                ExtendClientAreaTitleBarHeightHint = 34;
            }
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            var titleBar = this.FindDescendantOfType<TitleBar>();
            titleBar.Margin = new Thickness(titleBar.Margin.Left, titleBar.Margin.Top, titleBar.Margin.Right, 0);
        }*/
    }
}
