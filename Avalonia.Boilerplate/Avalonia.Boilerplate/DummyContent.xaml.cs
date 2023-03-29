using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using Avalonia.VisualTree;

namespace Avalonia.Boilerplate
{
    public class DummyContent : UserControl
    {

        public DummyContent() {
            AvaloniaXamlLoader.Load(this);
            var btn = this.FindControl<Button>("btn");
            btn.Click += BtnOnClick;

            var btnModal = this.FindControl<Button>("btn-modal");
            btnModal.Click += BtnModalOnClick;
            
            var btnWithoutParent = this.FindControl<Button>("btn-without-parent");
            btnWithoutParent.Click += BtnWithoutParentOnClick;
        }

        private void BtnWithoutParentOnClick(object? sender, RoutedEventArgs e)
        {
            var w = new Window
            {
                Width = 200, 
                Height = 200, 
                ExtendClientAreaToDecorationsHint = true, 
                ExtendClientAreaChromeHints=ExtendClientAreaChromeHints.PreferSystemChrome, 
                CanResize = false
            };
            w.Show();
        }

        private void BtnModalOnClick(object sender, RoutedEventArgs e)
        {
            var w = new Window { Width = 200, Height = 200 };
            w.ShowDialog(this.GetVisualRoot() as Window);
        }

        private void BtnOnClick(object sender, RoutedEventArgs e)
        {
            var w = new Window { Width = 200, Height = 200 };
            w.Show(this.GetVisualRoot() as Window);
        }
    }
}