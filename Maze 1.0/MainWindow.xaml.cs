using System;
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
        public MainWindow()
        {
            InitializeComponent();           
            
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Shape shapeToDraw = null;


            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            drawingContext.DrawRoundedRectangle(Brushes.Red, new Pen(Brushes.Black, 10), new Rect(50, 50, 60, 80), 5, 5);
            RenderTargetBitmap bmp = new RenderTargetBitmap(100, 100, 40, 40, PixelFormats.Pbgra32);
            RenderTargetBitmap bmp2 = new RenderTargetBitmap(50, 50, 40, 40, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);

            shapeToDraw = new Rectangle() { Fill = Brushes.Green, Height = 35, Width = 35, RadiusX = 10, RadiusY = 10 };
            Canvas.SetLeft(shapeToDraw, 20);
            Canvas.SetTop(shapeToDraw, 25);
            bmp2.Render(drawingVisual);
            drawingCanvas.Children.Add(shapeToDraw);        

        }

        private void btnEXit_Click(object sender, RoutedEventArgs e)
        {
           MessageBoxResult result = MessageBox.Show("Are you sure?", "Exit", MessageBoxButton.OKCancel);
            if(result  == MessageBoxResult.OK)
                this.Close();
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void drawingCanvas_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}

//Shape shapeToDraw = null;
//switch (_currentShape)
//{
//    case SelectedShape.Circle:
//        shapeToDraw = new Ellipse() { Fill = Brushes.Brown, Height = 35, Width = 35 };
//        RadialGradientBrush brush = new RadialGradientBrush();
//        brush.GradientStops.Add(new GradientStop(
//        (Color)ColorConverter.ConvertFromString("#FF2B1BE9"), 1));
//        brush.GradientStops.Add(new GradientStop(
//        (Color)ColorConverter.ConvertFromString("#FF2B1BE9"), 0));
//        brush.GradientStops.Add(new GradientStop(
//        (Color)ColorConverter.ConvertFromString("#FE5A8E6A"), 0.545));
//        shapeToDraw.Fill = brush;
//        break;

//    case SelectedShape.Rectangle:
//        shapeToDraw = new Rectangle() { Fill = Brushes.Green, Height = 35, Width = 35, RadiusX = 10, RadiusY = 10 };
//        break;

//    case SelectedShape.Line:
//        shapeToDraw = new Line()
//        {
//            Stroke = Brushes.Blue,
//            StrokeThickness = 10,
//            X1 = 0,
//            X2 = 50,
//            Y1 = 0,
//            Y2 = 50,
//            StrokeStartLineCap = PenLineCap.Triangle,
//            StrokeEndLineCap = PenLineCap.Round
//        };
//        break;
//    default: return;
//}

//Canvas.SetLeft(shapeToDraw, e.GetPosition(canvasDrawingArea).X);
//Canvas.SetTop(shapeToDraw, e.GetPosition(canvasDrawingArea).Y);
//canvasDrawingArea.Children.Add(shapeToDraw);
//        }
//        private void canvasDrawingArea_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
//{
//    Point pt = e.GetPosition((Canvas)sender);
//    HitTestResult result = VisualTreeHelper.HitTest(canvasDrawingArea, pt);
//    if (result != null)
//    {
//        canvasDrawingArea.Children.Remove(result.VisualHit as Shape);
//    }
//}       
//    }
