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
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void DrawCanv()
        {
            Pen _pen = new Pen(Brushes.Brown, 2); //to move to xaml form
            Grid_Set field = new Grid_Set(10, 10, (int)gameFieldCanvas.Width/10);
            Cell[,] cells = field.GetCells();


            for (int c = 0; c < 3; c++)
            {
                cells[c, 4].CanMoveDown();
            }
            cells[0, 4].CanMoveRight();


            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {             
                for (int r = 0; r < field.Rows; r++)
                {
                    for (int c = 0; c < field.Columns; c++)
                    {
                        if (cells[c, r].VetricalWall)
                        {
                            drawingContext.DrawLine(_pen, cells[c, r].GetPositionRU(), cells[c, r].GetPositionRD());
                        }
                        if (cells[c, r].HorizontalWall)
                        {
                            drawingContext.DrawLine(_pen, cells[c, r].GetPositionLD(), cells[c, r].GetPositionRD());
                        }                                               
                    }
                }
            }            
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)gameFieldCanvas.Width + 25, (int)gameFieldCanvas.Height + 25, 100, 100, PixelFormats.Pbgra32);

            bmp.Render(drawingVisual);            
            canvasImage.Source = bmp;
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
       
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
           
        }
    }    
}

