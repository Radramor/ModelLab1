using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace ModelLab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            methods();
        }

        void methods()
        {
            secondMethod(firstChart, secondChart);
            firstMethod(firstChart, secondChart); 
        }

        private static double sumForSecondMethods(int N, double rightBorder ,double leftBorder, double count)
        {
            double sum = 0;
            Random rnd = new Random();
            double x = 1 / (rightBorder - leftBorder);

            for (int i = 1; i <= N; i++)
            {
                sum += 2 * x - 1;
            }
            return sum;
        }

        private static void secondMethod(Chart firstChart, Chart secondChart)
        {
            firstChart.Series[0].Points.Clear();
            double leftBorder = 0, rightBorder = 1;
            double sum = 0, sumProbabilities = 0;
            int N = 50;
            double Y = 0;
            double dispersion = Math.Sqrt(1.0);
            double count = 0.1;

            /*for (int i = 0; i <= N; i++)
            {
                if (leftBorder < count && count < rightBorder)
                {
                    Y = 1 / (rightBorder - leftBorder);
                    firstChart.Series[0].Points.AddXY(count, Y);
                    Y = (count - leftBorder) / (rightBorder - leftBorder);
                    secondChart.Series[0].Points.AddXY(count, Y);
                    count += (rightBorder - leftBorder) / (double)N;
                }
                else
                {

                    count += (rightBorder - leftBorder) / (double)N;
                }

            }*/
            for (int i = 1; i <= N; i++)
            {

                    Y = Math.Sqrt(3 / (count * dispersion * sumForSecondMethods(N, rightBorder, leftBorder,count))) + 0.5;

                    firstChart.Series[0].Points.AddXY(count, Y);
                    sumProbabilities += Math.Abs(Y);
                    if (sumProbabilities > 1)
                        secondChart.Series[0].Points.AddXY(count, 1);
                    else
                        secondChart.Series[0].Points.AddXY(count, sumProbabilities);
                    count += (rightBorder - leftBorder) / (double)N;
            }

        }

        private static void firstMethod(Chart firstChart, Chart secondChart)
        {
            
            firstChart.Series[1].Points.Clear();
            double N = 50;
            double dRandValue = 0;
            double count = 0;
            double mu = 5;
            double sigma = 1;
            double sum = 0;
            for (int n = 0; n < N; n++)
            {
                dRandValue = Math.Round(1 /(sigma * Math.Sqrt(2 * Math.PI)) * Math.Exp(-0.5 * Math.Pow((count - mu) / sigma, 2)), 2);
                firstChart.Series[1].Points.AddXY((count - mu) / sigma, dRandValue);
                sum += dRandValue;
                secondChart.Series[1].Points.AddXY((count - mu) / sigma, sum / (0.1 * N));
                count += 1 / (0.1 * N);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            secondMethod(firstChart, secondChart);
            firstMethod(firstChart, secondChart);
        }
    }
}
