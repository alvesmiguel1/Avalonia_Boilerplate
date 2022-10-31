using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;

namespace Avalonia.Boilerplate {
    public class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
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
        }
    }
}
