using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OxyPlot;
using System.Threading.Tasks;

namespace FlightSimulator2.model
{
    class Line
    {
        private double a, b;

        public Line()
        {
            this.a = 0;
            this.b = 0;
        }

        public Line(double a, double b)
        {
            this.a = a;
            this.b = b;
        }
        
        public double f(double x)
        {
            return a * x + b;
        }

        public double Avg(List<DataPoint> x, int size)
        {
            double fi = 0, sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += x[i].Y;
            }
            if (size * sum == 0)
            {
                return 0;
            }
            fi = (double)1 / size * sum;
            return fi;
        }

        public double Var(List<DataPoint> x, int size)
        {
            double var1, sumPow = 0, fi = 0;
            for (int i = 0; i < size; i++)
            {
                sumPow += Math.Pow(x.ElementAt(i).Y, 2);
            }
            fi = Avg(x, size);
            if (size * sumPow == 0)
            {
                return 0;
            }
            var1 = (double)1 / size * sumPow - Math.Pow(fi, 2);
            return var1;
        }

        public double Cov(List<DataPoint> x, List<DataPoint> y, int size)
        {
            double covarince = 0, sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += (x[i].Y - Avg(x, size)) * (y[i].Y - Avg(y, size));
            }

            covarince = sum / size;
            return covarince;
        }

        public double Pearson(List<DataPoint> x, List<DataPoint> y, int size)
        {
            double pear = Cov(x, y, size) / (Math.Sqrt(Var(x, size)) * Math.Sqrt(Var(y, size)));
            return pear;
        }

        public Line linear_reg(List<DataPoint> x, List<DataPoint> y, int size)
        {
            double a, b;
            a = Cov(x, y, size) / Var(x, size);
            b = Avg(y, size) - a * Avg(x, size);
            return new Line(a, b);
        }

        public void linear_regg(List<DataPoint> x, List<DataPoint> y, int size)
        {
            this.a = Cov(x, y, size) / Var(x, size);
            this.b = Avg(y, size) - a * Avg(x, size);
        }

        /*Line linear_reg(List<DataPoint> points, int size)
        {
            float a, b;
            double[] x = new double[size];
            double[] y = new double[size];
            for (int i = 0; i < size; i++)
            {
                x[i] = points[i].X;
                y[i] = points[i].Y;
            }
            a = cov(x, y, size) / var(x, size);
            b = avg(y, size) - a * avg(x, size);
            return Line(a, b);
        }*/


        public double dev(Point p, Line l)
        {
            double fx, dis;
            fx = l.f(p.X);
            dis = Math.Abs(fx - p.Y);
            //  cout << "div1" << endl ;
            return dis;
            //return 0;
        }
    }
}
