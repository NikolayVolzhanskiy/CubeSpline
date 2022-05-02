using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CubeSpline
{
	public partial class Form1 : Form
	{
		Bitmap image, tempDraw;
		List<Point> points;
		int indexPoint = 0;
		Point p = new Point();
		bool mouseDown = false;
		bool CrSpline = false;
		bool clear = false;

		public Form1()
		{
			InitializeComponent();
			image = new Bitmap(Frame.ClientRectangle.Width, this.ClientRectangle.Height);
			points = new List<Point>();
		}

		private double[] mGaussian(double[,] a, double[] b)
		{
			int size = indexPoint;
			double[,] _a = new double[size, size];
			double[] c = new double[size];

			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					_a[i, i] = 1;
				}
			}

			for (int i = 0; i < size; i++)
			{
				double ap = a[i, i];
				for (int j = 0; j < size; j++)
				{
					if (ap != 0 && ap != 1)
					{
						_a[i, j] /= ap;
						a[i, j] /= ap;
					}
				}
				double[] ci = new double[size];
				for (int j = 0; j < size; j++)
				{
					ci[j] = a[j, i];
					if (j != i)
					{
						for (int k = 0; k < size; k++)
						{
							_a[j, k] -= _a[i, k] * ci[j];
							a[j, k] -= a[i, k] * ci[j];
						}
					}
				}
			}
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					c[i] += _a[i, j] * b[j];
				}
			}
			return c;
		}

		private void spline(Pen _pen, Graphics _graph)
		{

			List<double> dx = new List<double>();
			List<double> dy = new List<double>();
			for (int i = 0; i < indexPoint - 1; i++)
			{
				double temp_dx = points[i + 1].X - points[i].X;
				dx.Add(temp_dx);
				double temp_dy = points[i + 1].Y - points[i].Y;
				dy.Add(temp_dy);
			}

			double[,] H = new double[indexPoint, indexPoint];
			for (int i = 0; i < indexPoint; i++)
			{
				for (int j = 0; j < indexPoint; j++)
				{
					H[i, j] = 0;
				}
			}
			double[] Y = new double[indexPoint];
			for (int i = 0; i < indexPoint; i++)
			{
				Y[i] = 0;
			}
			double[] C = new double[indexPoint];
			for (int i = 0; i < indexPoint; i++)
			{
				C[i] = 0;
			}

			/// Нахождение коэффициентов Матрицы H и столбца C
			H[0, 0] = 1;
			H[indexPoint - 1, indexPoint - 1] = 1;
			for (int i = 1; i < indexPoint - 1; i++)
			{
				H[i, i - 1] = dx[i - 1];
				H[i, i] = 2 * (dx[i - 1] + dx[i]);
				H[i, i + 1] = dx[i];
				Y[i] = 3 * (dy[i] / dx[i] - dy[i - 1] / dx[i - 1]);
			}

			/// Решение матриц и нахождение коэффициентов С
			C = mGaussian(H, Y);
			

			/// Вычисление коэффициентов
			List<double> ai = new List<double>();
			List<double> bi = new List<double>();
			List<double> di = new List<double>();
			List<double> ci = new List<double>();
			for (int i = 0; i < indexPoint - 1; i++)
			{
				ai.Add(points[i].Y);
				di.Add((C[i + 1] - C[i]) / (3 * dx[i]));
				bi.Add(dy[i] / dx[i] - dx[i] * (2 * C[i] + C[i + 1]) / 3);
				ci.Add(C[i]);
			}
			//textBox1.Text = bi[3].ToString();

			/// Расчет координат для кривой
			int size = points[indexPoint - 1].X - points[0].X;

			List<double> xx = new List<double>();
			List<double> yy = new List<double>();
			double step = 1;
			double value = points[0].X;
			double num = size / step;

			/// Разбиение отрезка Х1 - Х2 на малые интервалы step
			for (double i = 0; i <= num; i++)
			{
					xx.Add(value);
					value += step;
			}

			int size_xx = xx.Count;

			List<int> x_ = new List<int>();
			List<int> xx_ = new List<int>(); 
			for (int i = 0; i < indexPoint; i++)
			{
				int temp = (int)(points[i].X / step);
				x_.Add(temp);
			}
			for (int i = 0; i < size_xx; i++)
			{
				int temp = (int)(xx[i] / step);
				xx_.Add(temp);
			}

			
			for (int i = 0; i < size_xx; i++)
			{
				int k = 0;
				for (int j = 0; j < indexPoint - 1; j++)
				{
					if (xx_[i] >= x_[j] && xx_[i] < x_[j + 1])
					{
						k = j;
						break;
					}
					else if (xx[i] == points[indexPoint - 1].X)
					{
						k = indexPoint - 2;
					}
				}
				/// yy(i) = Y[i] + bi(k) * (xx[i] - X[k]) + 1 / 2.0 * M(i) * pow((xx[i] - X[k]) , 2) + di(k) * pow((xx[i] - X[k]),3);
				double temp = ai[k] + bi[k] * (xx[i] - points[k].X) + ci[k] * Math.Pow((xx[i] - points[k].X), 2) + di[k] * Math.Pow((xx[i] - points[k].X), 3);
				yy.Add(temp);

			}
			for (int i = 0; i < size_xx - 1; i++)
			{
				_graph.DrawLine(_pen, (int)xx[i], (int)yy[i], (int)xx[i + 1], (int)yy[i + 1]);
			}
		}

		private void Frame_Paint(object sender, PaintEventArgs e)
        {
            if (null != tempDraw && !clear)
            {
                tempDraw = (Bitmap)image.Clone();
                Graphics graph = Graphics.FromImage(tempDraw);
                Pen pen = new Pen(Color.Blue, 4);
                graph.DrawEllipse(pen, p.X - 4 / 2, p.Y - 4 / 2, 4, 4);
                if (indexPoint > 0)
                {
                    pen.Color = Color.Green; pen.Width = 1;
                    graph.DrawLine(pen, points[indexPoint-1].X, points[indexPoint-1].Y, p.X, p.Y);
                }
                if (CrSpline)
                {
					pen.Color = Color.Black; pen.Width = 2;
					spline(pen, graph);
					/*Point[] _points = new Point[indexPoint];
					for (int i = 0; i < indexPoint; i++)
						_points[i] = points[i];
					graph.DrawCurve(pen, _points);*/
				}
				pen.Dispose();
                e.Graphics.DrawImageUnscaled(tempDraw, 0, 0);
                graph.Dispose();
            }
            
        }

        private void Frame_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                p = e.Location;
                Frame.Refresh();
            }
        }

        private void Frame_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            image = (Bitmap)tempDraw.Clone();
            points.Add(p);
            indexPoint++;
        }

        private void CreateSpline_Click(object sender, EventArgs e)
        {
			if (indexPoint > 1)
			{
				CrSpline = true;
				Frame.Invalidate();
			}
		}

        private void button1_Click(object sender, EventArgs e)
        {
			clear = true;
        }

        private void Frame_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            p = e.Location;
            Frame.Invalidate();
            tempDraw = (Bitmap)image.Clone();
        }

    }
}