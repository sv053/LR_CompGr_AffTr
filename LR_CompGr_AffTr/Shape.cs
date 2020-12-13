using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;


namespace LR_CompGr_AffTr
{
    class Shape
    {

       double w;
       double h;

        //координаты вершин исходной фигуры
        double[,] vs = { { -60, -125, 1 }, { 70, -125, 1 }, { -100, 120, 1 }, {95, 55, 1}, { -60, -125, 1 } };

        public Polygon OxArrow = null, OyArrow = null, shp=null;
        public Line Ox = null, Oy = null;

        public double[,] Vs
        {
            get => vs;
            private set => vs = value;
        }

        public double W { get => w; set => w = value; }
        public double H { get => h; set => h = value; }

        public Shape()
        { }

            public Shape(double width, double height)
        {
            W = width;
            H = height;
        }
     
        // задаём формулу поворота 
        public double[,] Rotate(int angle)
        {
            double rad = angle * Math.PI / 180;

            return new[,]
            {
                {Math.Cos(rad), Math.Sin(rad), 0},
                {-Math.Sin(rad), Math.Cos(rad), 0},
                {0,0,1}
            };
        }
        // задаём формулу отражения
        public double[,] Reflect()
        {
            var r = new double[Vs.GetLength(0), Vs.GetLength(1)];

            for (int i = 0; i < Vs.GetLength(0); i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    r[i, j] = Convert.ToInt32(Vs[i, j] * (-1));
                }
            }
            return r;
        }
        // получаем вершины для поворота и отражения путём применения формул поворота и отражения к исходному массиву вершин
        public double[,] GetVertices(double[,] b)
        {
            var r = new double[Vs.GetLength(0), b.GetLength(1)];

            for (int i = 0; i < Vs.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        r[i, j] += Convert.ToInt32(Vs[i, k] * b[k, j]);
                    }
                }
            }
            return r;
        }
        // получаем вершины для перемещения 
        public double[,] GetTranslatedVertices(double lambda, double mu)
        {
            var r = new double[Vs.GetLength(0), Vs.GetLength(1)];

            for (int i = 0; i < Vs.GetLength(0); i++)
            {
                 r[i, 0] = Convert.ToInt32(Vs[i, 0] + lambda);
                 r[i, 1] = Convert.ToInt32(Vs[i, 1] + mu);
                 r[i, 2] = Convert.ToInt32(Vs[i, 2] + 0);
            }
            return r;           
        }
        // получаем масштабированные вершины 
        public double[,] GetScaledVertices(double alpha, double delta)
        {
            PointCollection rv = new PointCollection();

            var r = new double[Vs.GetLength(0), Vs.GetLength(1)];

            for (int i = 0; i < Vs.GetLength(0); i++)
            {
                r[i, 0] = Convert.ToInt32(Vs[i, 0] * alpha);
                r[i, 1] = Convert.ToInt32(Vs[i, 1] * delta);
                r[i, 2] = Convert.ToInt32(Vs[i, 2] + 0);
            }
            return r;
        }
        // формируем оси координат со стрелками
        public void BuildAxis()
        {
            // ось абсцисс
            Ox = new Line()
            {
                StrokeThickness = 1,
                Stroke = Brushes.Gray,
                X1 = 5,
                X2 =W-5,
                Y1 = H / 2,
                Y2 = H / 2
            };
            // ось ординат
            Oy = new Line()
            {
                StrokeThickness = 1,
                Stroke = Brushes.Gray,
                X1 = W / 2,
                X2 = W / 2,
                Y1 = 5,
                Y2 = H-5
            };
            // стрелка оси абсцисс
            OxArrow = new Polygon()
            {
                StrokeThickness = 1,
                Stroke = Brushes.Gray,
                Fill = Brushes.Gray,
                Points = { new Point(W-5, H / 2), new Point(W - 15, H / 2 - 5), new Point(W - 15, H / 2 + 5) }
            };
            // стрелка оси ординат
            OyArrow = new Polygon()
            {
                StrokeThickness = 1,
                Stroke = Brushes.Gray,
                Fill = Brushes.Gray,
                Points = { new Point(W / 2, 5), new Point(W / 2 - 4, 15), new Point(W / 2 + 4, 15) }
            };

        }
    }
}
