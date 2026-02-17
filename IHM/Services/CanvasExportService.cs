using LogicLayer;
using Service.Interfaces;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ViewModels;

namespace IHM.Services
{
    /// <summary>
    /// Implémentation de l'interface ICanvasExport. Cette classe utilise les classes de la couche WPF
    /// </summary>
    public class CanvasExportService : ICanvasExportService
    {
        public void SaveAutomatonAsImage(IEnumerable<EtatData> etats, IEnumerable<TransitionData> transitions, string filePath)
        {
            // Calcul du bounding box
            double minX = double.PositiveInfinity;
            double minY = double.PositiveInfinity;
            double maxX = double.NegativeInfinity;
            double maxY = double.NegativeInfinity;

            foreach (EtatData etat in etats)
            {
                double r = etat.EstFinal ? etat.EtatFinalRadius : etat.Radius;

                double left = etat.X - r;
                double right = etat.X + r;
                double top = etat.Y - r;
                double bottom = etat.Y + r;

                minX = Math.Min(minX, left);
                minY = Math.Min(minY, top);
                maxX = Math.Max(maxX, right);
                maxY = Math.Max(maxY, bottom);
            }


            foreach (TransitionData transition in transitions)
            {
                double r = transition.Condition != null ? transition.Condition.Count() * 10 : 10;

                double left = transition.XTexte - r;
                double right = transition.XTexte + r;
                double top = transition.YTexte - r;
                double bottom = transition.YTexte + r;

                minX = Math.Min(minX, left);
                minY = Math.Min(minY, top);
                maxX = Math.Max(maxX, right);
                maxY = Math.Max(maxY, bottom);
            }

            // Dimensions de l'image
            double offsetX = minX < 0 ? -minX : -minX;
            double offsetY = minY < 0 ? -minY : -minY;

            double width = Math.Max(1, maxX - minX);
            double height = Math.Max(1, maxY - minY);

            // Création du conteneur temporaire
            Canvas container = new Canvas
            {
                Width = width,
                Height = height,
                Background = Brushes.White
            };

            foreach (var etat in etats)
            {
                double radius = etat.EstFinal ? etat.EtatFinalRadius : etat.Radius;
                var ellipse = new System.Windows.Shapes.Ellipse
                {
                    Width = radius * 2,
                    Height = radius * 2,
                    Stroke = Brushes.Black,
                    Fill = Brushes.LightGray
                };

                Canvas.SetLeft(ellipse, etat.X + offsetX - radius);
                Canvas.SetTop(ellipse, etat.Y + offsetY - radius);
                container.Children.Add(ellipse);
            }

            // --- Draw transition texts ---
            foreach (var transition in transitions)
            {
                var textBlock = new TextBlock
                {
                    Text = string.Join(",", transition.Condition ?? " "),
                    Foreground = Brushes.Black
                };

                Canvas.SetLeft(textBlock, transition.XTexte + offsetX);
                Canvas.SetTop(textBlock, transition.YTexte + offsetY);
                container.Children.Add(textBlock);
            }

            // Mise en forme
            container.Measure(new Size(width, height));
            container.Arrange(new Rect(0, 0, width, height));
            container.UpdateLayout();

            Application.Current.Dispatcher.Invoke(() => { }, System.Windows.Threading.DispatcherPriority.Render);

            // Export
            RenderAndSave(container, width, height, filePath);


        }

        #region Helpers
        private void RenderAndSave(Visual visual, double width, double height, string filePath)
        {
            string? dir = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            RenderTargetBitmap rtb = new RenderTargetBitmap(
                (int)Math.Ceiling(width),
                (int)Math.Ceiling(height),
                96, 96,
                PixelFormats.Pbgra32);

            rtb.Render(visual);

            BitmapEncoder encoder = CreateEncoder(filePath);
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            using FileStream fs = File.Create(filePath);
            encoder.Save(fs);
        }

        private BitmapEncoder CreateEncoder(string filePath)
        {
            return Path.GetExtension(filePath).ToLower() switch
            {
                ".jpg" or ".jpeg" => new JpegBitmapEncoder { QualityLevel = 95 },
                ".png" => new PngBitmapEncoder(),
                ".bmp" => new BmpBitmapEncoder(),
                _ => throw new NotSupportedException($"Format non supporté : {filePath}")
            };
        }

        private ItemsControl CloneItemsControl(ItemsControl original) =>
            new ItemsControl
            {
                ItemsSource = original.ItemsSource,
                ItemTemplate = original.ItemTemplate,
                ItemsPanel = original.ItemsPanel,
                ItemContainerStyle = original.ItemContainerStyle
            };

        private IEnumerable<T> FindVisualChildren<T>(DependencyObject parent) where T : DependencyObject
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child is T t)
                    yield return t;

                foreach (T result in FindVisualChildren<T>(child))
                    yield return result;
            }
        }

        private void ApplyTranslation(UIElement element, double dx, double dy)
        {
            element.RenderTransform = new TranslateTransform(dx, dy);
        }
        #endregion
    }
}
