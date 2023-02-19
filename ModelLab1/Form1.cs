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

        private static double sumForSecondMethods(int N)
        {
            double sum = 0;
            Random rnd = new Random();

            for (int i = 1; i <= N; i++)
            {
                double x = 1 / (double)N;
                //double x = rnd.NextDouble();
                sum += 2 * x - 1;
            }
            return sum;
        }

        private static void secondMethod(Chart firstChart, Chart secondChart)
        {
            firstChart.Series[0].Points.Clear();
            
            double sum = 0, sumProbabilities = 0;
            int N = 100;

            double step = 1 / (double)N;
            double[] Y = new double[N];
            sum = sumForSecondMethods(N);
            double dispersion = Math.Sqrt(1.0);
            double count = 0;

            for (int i = 1; i <= N; i++)
            {
                Y[i - 1] = Math.Sqrt(3 / (i * dispersion * Math.Abs(sum))) + 0.5;
                firstChart.Series[0].Points.AddXY(count, Y[i - 1]);
                sumProbabilities += Math.Abs( Y[i - 1]);
                secondChart.Series[0].Points.AddXY(count, sumProbabilities);
                count += 0.1;
            }

        }

        private static void firstMethod(Chart firstChart, Chart secondChart)
        {
            
            firstChart.Series[1].Points.Clear();
            int N = 100;
            double[] massive = new double[N];
            double dRandValue = 0;
            double count = 0;
            double mu = 5;
            double sigma = 1;
            double sum = 0;

            for (int n = 0 ; n < N; n++)
            {
                dRandValue = Math.Round(1 / Math.Sqrt(2 * Math.PI) * Math.Exp(-0.5 * Math.Pow((count - mu)/sigma, 2)), 5);
                massive[n] = dRandValue;
                count += 0.1;
            }
            count = 0;
            for (int n = 0; n < N; n++)
            {
                firstChart.Series[1].Points.AddXY(count - 5, massive[n]);
                sum += massive[n];
                secondChart.Series[1].Points.AddXY(count - 5, sum / 10);
                count += 0.1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            secondMethod(firstChart, secondChart);
            firstMethod(firstChart, secondChart);
        }
    }
}
