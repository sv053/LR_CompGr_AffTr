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
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Drawing;
using System.Reflection;

namespace LR_CompGr_AffTr
{
    
    public partial class MainWindow : Window
    {
        Shape shape;
        double w;
        double h;

        public MainWindow()
        {
            InitializeComponent();
           
            Loaded += DrawAxis;
        }
       
        public void DrawAxis(object sender, EventArgs e)
        {
            w = DrawingArea.RenderSize.Width; //получаем координату х 
            h = DrawingArea.RenderSize.Height; //и координату у центра холста

            shape = new Shape(w, h);
            shape.BuildAxis();
            DrawingArea.Children.Add(shape.Ox);
            DrawingArea.Children.Add(shape.Oy);
            DrawingArea.Children.Add(shape.OxArrow);
            DrawingArea.Children.Add(shape.OyArrow);
        }
      
        private void Draw(double[,] v)
        {
            for (int i = 0; i < v.GetLength(0)-1 ; i++)
            {
                Line line = null;


                line = new Line()
                {
                    X1 = v[i, 0],
                    Y1 = v[i, 1],
                    X2 = v[i + 1, 0],
                    Y2 = v[i + 1, 1]

                };
               Random rnd = new Random();
         
                SolidColorBrush scb = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255))); 

                line.Stroke = scb;
                Canvas.SetLeft(line, shape.W / 2);
                Canvas.SetTop(line, shape.H / 2);

                bool isShown = false;

                if (!isShown)
                {
                    isShown = true;
                    var dbAnim = new DoubleAnimation();
                    dbAnim.From = 1.0;
                    dbAnim.To = 0.0;
                    dbAnim.Duration = new Duration(TimeSpan.FromSeconds(5));

                    DrawingArea.Children.Add(line);

                    line.BeginAnimation(Line.OpacityProperty, dbAnim);
                }
            }
        }

        private void BasicFButton_Click(object sender, RoutedEventArgs e)
        {
          Draw(shape.Vs);
        }

        private void RotateFButton_Click(object sender, RoutedEventArgs e)
        {
          Draw(shape.GetVertices(shape.Rotate(46)));
          Draw(shape.GetVertices(shape.Rotate(112)));
        }

        private void ReflectFButton_Click(object sender, RoutedEventArgs e)
        {
          Draw(shape.Reflect());
        }

        private void ScaleFButton_Click(object sender, RoutedEventArgs e)
        {
           Draw(shape.GetScaledVertices(2.45, 1.21));
        }
        private void MoveFButton_Click(object sender, RoutedEventArgs e)
        {
            Draw(shape.GetTranslatedVertices(23, 89));
            Draw(shape.GetTranslatedVertices(-123, 45));
            Draw(shape.GetTranslatedVertices(123, -89));
        }

    }
}
