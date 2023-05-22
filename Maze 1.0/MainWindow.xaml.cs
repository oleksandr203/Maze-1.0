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
        int rows = 12;
        int columns = 10;
        int sizeOfCell = 50;
        public MainWindow()
        {
            InitializeComponent();            
        }
       
        private void DrawCanv()
        {           
            Pen _pen = new Pen(Brushes.Brown, 1); //to move to xaml form?
            GridGameState field = new GridGameState(columns, rows);
            Cell[,] cells = field.GetCellsShot();

            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                for (int c = 0; c < field.Columns; c++)
                {
                    for (int r = 0; r < field.Rows; r++)
                    {
                        if (cells[c, r].VetricalWall)
                        {
                            drawingContext.DrawLine(_pen, PointScaleConvertUpRight(cells[c, r].GetPosition()),
                                PointScaleConvertDownRight(cells[c, r].GetPosition()));
                        }
                        if (cells[c, r].HorizontalWall)
                        {
                            drawingContext.DrawLine(_pen, PointScaleConvertDownLeft(cells[c, r].GetPosition()),
                                PointScaleConvertDownRight(cells[c, r].GetPosition()));
                        }
                        if (cells[c, r].Id == 1)
                        {
                            drawingContext.DrawEllipse(Brushes.Aquamarine, _pen, PointScaleConvertCenterCell(cells[c, r].GetPosition()), sizeOfCell/2, 20);
                        }
                        if (cells[c, r].IsFinishCell)
                        {
                            drawingContext.DrawEllipse(Brushes.BlueViolet, _pen, PointScaleConvertCenterCell(cells[c, r].GetPosition()), sizeOfCell / 2, 20);
                        }
                    }
                }
            }
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)gameFieldCanvas.Width + 25, (int)gameFieldCanvas.Height + 25, 100, 100, PixelFormats.Pbgra32);

            bmp.Render(drawingVisual);
            canvasImage.Source = bmp;
        }

        public Point PointScaleConvertUpLeft(Point p)
        {
            Point point = new Point(p.X, p.Y);
            point.X = p.X * sizeOfCell;
            point.Y = p.Y * sizeOfCell;
            return point;
        }

        public Point PointScaleConvertUpRight(Point p)
        {
            Point point = new Point(p.X, p.Y);
            point.X = p.X * sizeOfCell + sizeOfCell;
            point.Y = p.Y * sizeOfCell;
            return point;
        }
        public Point PointScaleConvertDownLeft(Point p)
        {
            Point point = new Point(p.X, p.Y);
            point.X = p.X * sizeOfCell;
            point.Y = p.Y * sizeOfCell + sizeOfCell;
            return point;
        }

        public Point PointScaleConvertDownRight(Point p)
        {
            Point point = new Point(p.X, p.Y);
            point.X = p.X * sizeOfCell + sizeOfCell;
            point.Y = p.Y * sizeOfCell + sizeOfCell;
            return point;
        }
        public Point PointScaleConvertCenterCell(Point p)
        {
            Point point = new Point(p.X, p.Y);
            point.X = p.X * sizeOfCell + sizeOfCell/2;
            point.Y = p.Y * sizeOfCell + sizeOfCell/2;
            return point;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            GameOverMenu.Visibility = Visibility.Collapsed;                          
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
                   
                    break;

                case Key.Right:
                    
                    break; 
                    
                case Key.Down:
                   
                    break;

                default: break;
            }
        }       
        
    }    
}

