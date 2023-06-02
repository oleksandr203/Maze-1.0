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

namespace Maze_1._0
{   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double sizeOfCell = 10;
        GridGameState field;
        Cell[,] steps;
        Cell[,] cells;
        Cell[,] stepsOfSolution;        
        Pen _pen = new Pen(Brushes.Gray, 1);
        
        public MainWindow()
        {
            InitializeComponent();            
        }
       
        private async void DrawCanv(int rows, int columns)
        {           
            field = new GridGameState(columns, rows);
            gameFieldCanvas.Width = columns * sizeOfCell;
            gameFieldCanvas.Height = rows * sizeOfCell;
            await Task.Run(() => cells = field.GetCellsShot());            
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                for (int c = 0; c < field.Columns; c++)
                {
                    for (int r = 0; r < field.Rows; r++)
                    {
                        if (cells[c, r].VerticalWall)
                        {
                            drawingContext.DrawLine(_pen, PointScaleConvertUpRight(cells[c, r].GetPosition()),
                                PointScaleConvertDownRight(cells[c, r].GetPosition()));
                        }
                        if (cells[c, r].HorizontalWall)
                        {
                            drawingContext.DrawLine(_pen, PointScaleConvertDownLeft(cells[c, r].GetPosition()),
                                PointScaleConvertDownRight(cells[c, r].GetPosition()));
                        }
                        if (cells[c, r].IsStartCell)
                        {                            
                            drawingContext.DrawEllipse(null, new Pen(Brushes.Green, 4), PointScaleConvertCenterCell(cells[c, r].GetPosition()), sizeOfCell / 3, sizeOfCell / 3);
                        }
                        if (cells[c, r].IsFinishCell)
                        {   
                            drawingContext.DrawEllipse(null, new Pen(Brushes.DarkRed, 4), PointScaleConvertCenterCell(cells[c, r].GetPosition()), sizeOfCell / 3, sizeOfCell / 3);
                        }                        
                    }                   
                }
            }
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)gameFieldCanvas.Width + 25, (int)gameFieldCanvas.Height + 25, 100, 100, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);
            canvasImage.Source = bmp;            
        }

        public async void DrawPlayerSolving()
        {                   
            DrawingVisual drawingVisual = new DrawingVisual();            
            await Task.Run(() => steps = field.GetStepsPoints());
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            { 
                for (int c = 0; c < field.Columns; c++)
                {
                    for (int r = 0; r < field.Rows; r++)
                    {
                        if (steps[c, r].IsStepped)
                        {
                            drawingContext.DrawEllipse(Brushes.Yellow, null, PointScaleConvertCenterCell(steps[c, r].GetPosition()),
                                sizeOfCell / 7, sizeOfCell / 7);
                        }                       
                    }
                }
                drawingContext.DrawEllipse(Brushes.Tomato, null, PointScaleConvertCenterCell(field.CurrentPosition), sizeOfCell / 4, sizeOfCell / 4);                
            }
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)gameFieldCanvas.Width + 25,
                (int)gameFieldCanvas.Height + 25, 100, 100, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);
            canvasImageSecond.Source = bmp;
        }

        public async void DrawAutoSolving()
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            await Task.Run(() => stepsOfSolution = field.GetAutoSolution());           
           
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {                               
                for (int c = 0; c < field.Columns; c++)
                {
                    for (int r = 0; r < field.Rows; r++)
                    {
                        if (stepsOfSolution[c, r].IsSteppedBySolution)
                        {
                            drawingContext.DrawEllipse(Brushes.Red, null, PointScaleConvertCenterCell(stepsOfSolution[c, r].GetPosition()),
                                sizeOfCell / 6, sizeOfCell / 6);
                        }
                    }
                }                
            }
            RenderTargetBitmap bmpAutoSolve = new RenderTargetBitmap((int)gameFieldCanvas.Width + 25, (int)gameFieldCanvas.Height + 25, 100, 100, PixelFormats.Pbgra32);
            bmpAutoSolve.Render(drawingVisual);
            canvasImageSecond.Source = bmpAutoSolve;
            btnHelp.IsEnabled = true;
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
            btnHelp.IsEnabled = false;
            DrawAutoSolving();                    
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!field.IsFinished)
            {
                switch (e.Key)
                {
                    case Key.Left:
                        field.StepLeft();
                        break;

                    case Key.Right:
                        field.StepRight();
                        break;

                    case Key.Down:
                        field.StepDown();
                        break;

                    case Key.Up:
                        field.StepUp();
                        break;

                    default: break;
                }

                field.MarkLocalPlayerPositon();
                DrawPlayerSolving();
            }            
            if(field.IsFinished)
            {
                GameOverMenu.Opacity = 50;
                GameOverMenu.Visibility= Visibility.Visible;
            }
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            StartMenuGrid.Visibility = Visibility.Collapsed;

            btnStart_Click_1(sender, e);
        }

        private void btnStart_Click_1(object sender, RoutedEventArgs e)
        {
             DrawCanv((int)(gameFieldCanvas.Height / sizeOfCell), (int)(gameFieldCanvas.Width / sizeOfCell));
             DrawPlayerSolving();        
        }

        private void GoToStartMenu_Click(object sender, RoutedEventArgs e)
        {
            StartMenuGrid.Visibility = Visibility.Visible;
        }
                
        private async void cellSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {           
                while ((int)cellSizeSlider.Value % 5 != 0)
                {
                    cellSizeSlider.Value -= (int)cellSizeSlider.Value % 5;
                    cellSizeSlider.IsEnabled = false;                
                    await Task.Delay(100);
                }            
            LabelShowResolution.Content = $"You choose {(int)((Slider)sender).Value} Colunms";
            sizeOfCell = (gameFieldCanvas.Width / ((Slider)sender).Value);
            cellSizeSlider.IsEnabled = true;
        }

        private void acceptAgain_Click(object sender, RoutedEventArgs e)
        {
            GameOverMenu.Visibility = Visibility.Hidden;
            btnStart_Click_1(sender, e);
        }
    }    
}

