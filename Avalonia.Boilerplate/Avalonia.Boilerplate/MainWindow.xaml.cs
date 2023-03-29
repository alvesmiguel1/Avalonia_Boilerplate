using System;
using System.Collections;
using System.Runtime.InteropServices;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Avalonia.Boilerplate {
    public class MainWindow : Window
    {
        
        private readonly TabControl tabs;
        
        private const int OSXTrafficLightsWidth = 72;
        private const int TitleBarHeight = 34; 
        
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            PseudoClasses.Set(":osx", RuntimeInformation.IsOSPlatform(OSPlatform.OSX));
            
            
            RefreshExtendClientAreaChromeHints();
            InitializeWindowState();
 
            tabs = this.FindControl<TabControl>("tabs");
            AddDummyTab();
        }
        
        private void RefreshExtendClientAreaChromeHints()
        {
            ExtendClientAreaChromeHints = ExtendClientAreaChromeHints.PreferSystemChrome;
        }
        
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e) {
            base.OnApplyTemplate(e);
            ApplyTitleBarMargin(WindowState);
        }
        
        protected override void HandleWindowStateChanged(Avalonia.Controls.WindowState state) {
            base.HandleWindowStateChanged(state);
            PseudoClasses.Set(":maximized", state == WindowState.Maximized);
            ApplyTitleBarMargin(state);

            ExtendClientAreaTitleBarHeightHint = TitleBarHeight;
        }
        
        private void ApplyTitleBarMargin(WindowState state) {
            TitleBarMargin = state switch {
                WindowState.FullScreen => new Thickness(0),
                _ => new Thickness(OSXTrafficLightsWidth, 0, 0, 0),
            };
        }

        private void InitializeWindowState()
        {
            // Setup dummy window state saved from last session
            Position = new PixelPoint(421, 206);
            Width = 1721;
            Height = 1095;
            WindowState = WindowState.Maximized;

            // simulate system decoration change: in the original app, seems to be caused by Position set
            //SystemDecorations = SystemDecorations.BorderOnly;
            
            // simulate a change back to Full
            //SystemDecorations = SystemDecorations.Full;
        }

        private void AddDummyTab()
        {
            var tabHeaderInfo = new TabHeaderInfo {
                Caption = "Tab title",
                AllowClose = true, 
                ImageSource = new Bitmap(AvaloniaLocator.Current.GetService<IAssetLoader>().Open(new Uri("resm:Avalonia.Boilerplate.ServiceStudio.ico")))
            };
            
            var tab = new TabItem() {
                Content = new DummyContent(),
                DataContext = tabHeaderInfo,
                Header = tabHeaderInfo,
                IsVisible = true,
                IsEnabled = false
            };

            ((IList)tabs.Items).Add(tab);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public static readonly StyledProperty<Thickness> TitleBarMarginProperty =
            AvaloniaProperty.Register<MainWindow, Thickness>(nameof(TitleBarMargin), defaultValue: new Thickness(0),
                inherits: true);

        public Thickness TitleBarMargin
        {
            get => GetValue(TitleBarMarginProperty);
            private set => SetValue(TitleBarMarginProperty, value);
        }
    }
}
