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
            const int N = 10;
            double interval = 0.2;
            double intervalCount = 1 / interval;
            double[] Probabilites = new double[(int)intervalCount];
            double[] Y = new double[N];
            double[] YCLT = new double[N];
            double[] X = new double[N];

            double sum = 0, summForRandomValues = 0;

            Random rnd = new Random();
            double MathExpected = 0.5;
            double dispersion = 1;

            for (int i = 0; i < Probabilites.Length; i ++)
            {
                Probabilites[i] = 0;
            }

            for (int i = 1; i <= N; i++)
            {
                double x = rnd.NextDouble();
                X[i - 1] = x;
                int j = 0;
           
                //summForRandomValues += x;
                sum += Math.Abs(2 * x - 1);
            }

            secondMethod(firstChart, secondChart, X, MathExpected, dispersion, N, rnd, interval, Probabilites);
//firstMethod(firstChart, secondChart); 
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

        private static void secondMethod(Chart firstChart, Chart secondChart, double[] X, double MathExpected, double dispersion, int N, Random rnd,
            double interval, double[] Probabilites)
        {

            firstChart.Series[0].Points.Clear();
            secondChart.Series[0].Points.Clear();
            /*double leftBorder = 0, rightBorder = 1;
            double sum = 0, sumProbabilities = 0;
            int N = 50;
            double Y = 0;
            double dispersion = Math.Sqrt(1.0);
            double count = 0.1;*/

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
            /* for (int i = 1; i <= N; i++)
             {

                     Y = Math.Sqrt(3 / (count * dispersion * sumForSecondMethods(N, rightBorder, leftBorder,count))) + 0.5;

                     firstChart.Series[0].Points.AddXY(count, Y);
                     sumProbabilities += Math.Abs(Y);
                     if (sumProbabilities > 1)
                         secondChart.Series[0].Points.AddXY(count, 1);
                     else
                         secondChart.Series[0].Points.AddXY(count, sumProbabilities);
                     count += (rightBorder - leftBorder) / (double)N;
             }*/
            double[] Y = new double[N];
            double sum = 0;
            for (int i = 1; i <= N; i++)
            {  
                sum += Math.Abs(2 * X[i - 1] - 1);
            }

            // выборка генерируемая по 2 методу            
            for (int i = 1; i <= N; i++)
            {
                
                Y[i - 1] = Math.Sqrt(3 / (i * dispersion * sum)) + MathExpected;
               // secondChart.Series[0].Points.AddXY(X[i-1], Y[i-1]);
                double interval2 = interval;
                int j = 0;
                while (interval2 <=  1)
                {
                    if (Y[i-1] < interval2 && Y[i-1] > interval2 - interval)
                    {
                        Probabilites[j]++;
                        break;
                    }
                    j++;
                    interval2 += interval;
                }

            }
            double high = 0;
            double interval3 = 0;
            for (int i = 0; i < Probabilites.Length; i++)
            {
                Probabilites[i] = Probabilites[i] / N;
                high = Probabilites[i] / interval3;
                firstChart.Series[0].Points.AddXY(interval3, high);
                interval3 += interval;
            }
        }


        private static void firstMethod(Chart firstChart, Chart secondChart)
        {
            
            firstChart.Series[1].Points.Clear();
            double N = 50;
            double dRandValue = 0;
            double count = 0;
            double mu = 0.5;
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
            //secondMethod(firstChart, secondChart, X, MathExpected, dispersion, N, rnd);
            //firstMethod(firstChart, secondChart);
            methods();
        }
    }
}
/*using System;
using System.ComponentModel;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            const int N = 1000;
            double[] Y = new double[N];
            double[] YCLT = new double[N];
            double[] X = new double[N];

            double sum = 0, summForRandomValues = 0;

            Random rnd = new Random();
            double MathExpected = 0.5;
            double dispersion = 1;


            for (int i = 1; i <= N; i++)
            {
                double x = rnd.NextDouble();
                X[i - 1] = x;
                //summForRandomValues += x;
                sum += Math.Abs(2 * x - 1);
            }

            // выборка генерируемая по 2 методу            
            for (int i = 1; i <= N; i++)
            {
                Y[i - 1] = Math.Sqrt(3 / (i * dispersion * sum)) + MathExpected;

            }

            // выборка генерируемая для цпт
            for (int i = 1; i <= N; i++)
            {
                for (int j = 1; j < i; j++)
                {
                    summForRandomValues += X[j - 1];
                }

                YCLT[i - 1] = (summForRandomValues - (MathExpected * i)) / (dispersion * Math.Sqrt(i));
                summForRandomValues = 0;
            }

            for (int i = 0; i < N; i++)
            {
                Console.Write($"{YCLT[i]} ");
            }



            // Console.WriteLine(sum);

            *//* for (int i = 0; i < N; i++)
            {
            Console.Write($"{Y[i]} ");
            }*//*

        }
    }
}*/