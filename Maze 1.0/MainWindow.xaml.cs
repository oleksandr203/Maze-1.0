using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Maze_1._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int HeightRect = 35;
        int WigthRect = 35;
        int blockSize = 90;
        public MainWindow()
        {
            InitializeComponent();
            Grid_Set field = new Grid_Set(10, 10);
            field.CreateGrid();
        }
        
        private void DrawCanv()  //temporary test
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {                
                drawingContext.DrawRoundedRectangle(Brushes.Yellow, new Pen(Brushes.Black, 3), new Rect(5, 5, 450, 100), 20, 20);                
            }            
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)gameFieldCanvas.Height, (int)gameFieldCanvas.Width, 100, 100, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);            
            canvasImage.Source = bmp;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            GameOverMenu.Visibility = Visibility.Collapsed;

            Shape shapeToDraw = null;
            shapeToDraw = new Rectangle() { Fill = Brushes.Green, Height = HeightRect, Width = WigthRect, RadiusX = 10, RadiusY = 10 };

            Canvas.SetLeft(shapeToDraw, 20);
            Canvas.SetTop(shapeToDraw, 25);

            gameFieldCanvas.Children.Add(shapeToDraw);                   
        }

        private void btnEXit_Click(object sender, RoutedEventArgs e)
        {
           MessageBoxResult result = MessageBox.Show("Are you sure?", "Exit", MessageBoxButton.OKCancel);
            if(result  == MessageBoxResult.OK)
                this.Close();
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            GameOverMenu.Visibility = Visibility.Visible; 
        }

        private void drawingCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            DrawCanv();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Enter:
                    MessageBox.Show("Enter");
                    break;
                case Key.Left:
                    HeightRect += 10;
                    WigthRect += 10;
                    break;
                case Key.Right:
                    HeightRect += 20;
                    WigthRect += 10;
                    break;
                case Key.Up:
                    GenImg();
                    break;
                case Key.Down:
                    blockSize += 5;
                    break;
                default: break;
            }
        }
        private void GenImg()
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawRoundedRectangle(Brushes.Yellow, new Pen(Brushes.Black, 5), new Rect(5, 5, 275, blockSize), 20, 20);
            }
            RenderTargetBitmap bmp = new RenderTargetBitmap(400, 100, 100, 90, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);
            canvasImage.Source = bmp;            
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            GenImg();
        }
    }
    public class CustomVisualFrameworkElement : FrameworkElement
    {
        VisualCollection theVisuals;
        public CustomVisualFrameworkElement()
        {
            theVisuals = new VisualCollection(this) { AddRect(), AddCircle() };
        }
        private Visual AddCircle()
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                Rect rect = new Rect(new Point(160, 100), new Size(320, 80));
                drawingContext.DrawEllipse(Brushes.DarkBlue, null,
            new Point(70, 90), 40, 50);
            }
            return drawingVisual;
        }
        private Visual AddRect()
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                Rect rect = new Rect(new Point(160, 100), new Size(320, 80));
                drawingContext.DrawRectangle(Brushes.Tomato, null, rect);
            }
            return drawingVisual;
        }
    }
}

