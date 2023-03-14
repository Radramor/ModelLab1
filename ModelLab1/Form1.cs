using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ModelLab1
{
    public partial class Form1 : Form
    {
        private const int NumberOfSampleElements = 20; // количество элементов одной выборки
        private double[] X = new double[NumberOfSampleElements]; // элементы выборки
        private Random rnd = new Random();
        
        private const int NumberOfSamples = 10000; // количество выборок
        private double[] CLT_Y = new double[NumberOfSamples]; // результаты преобразования выборок по формулам
        private double[] Y = new double[NumberOfSamples]; // результаты преобразования выборок по формулам
        
        private const double m = 0.5; // матожидание
        private const double dispersion = 1;
  
        public Form1()
        {
            InitializeComponent();
            methods();
        }

        void GenerateY()
        {
            for (int i = 0; i < NumberOfSamples; i++)
            {
                generateXSample();
            double sumOfXElements = 0;
            for (int j = 0; j < NumberOfSampleElements; j++)
            {
                sumOfXElements += Math.Abs(2 * X[j] - 1);
            }
                
                Y[i] = Math.Sqrt(3 / (NumberOfSampleElements * dispersion * sumOfXElements)) + m;
            }
        }

        void GenerateCLT_Y()
        {
            for (int i = 0; i < NumberOfSamples; i++)
            {
                generateXSample();
                double sumOfXElements = 0;
                for (int j = 0; j < NumberOfSampleElements; j++)
                {   
                    sumOfXElements += X[j];
                }

                CLT_Y[i] = (sumOfXElements - m * NumberOfSampleElements) / (dispersion * Math.Sqrt(NumberOfSampleElements));
            }
        }

        void methods()
        {
            GenerateY();
           //Array.Sort(Y, (x, y) => y.CompareTo(x));
            Array.Sort(Y);
            GenerateCLT_Y();
           //Array.Sort(CLT_Y, (x, y) => y.CompareTo(x));
            Array.Sort(CLT_Y);

            CreateGistogramms(Y, firstChart);
            CreateGistogramms(CLT_Y, secondChart);
            }


        void CreateGistogramms(double[] YNow, Chart chart)
        {
            double minValue, maxValue, intervalLength;
            int countOfIntervals;
            countOfIntervals = (int)Math.Floor(1 + 3.322 * Math.Log10(NumberOfSamples));

            // нахождение длины интервала
            minValue = YNow[0];
            maxValue = YNow[NumberOfSamples - 1];
            intervalLength = (maxValue - minValue) / countOfIntervals;

            double[] ProbabilitiesOfHittingTheInerval = new double[countOfIntervals];
            int[] CounterOfNumbersOnInterval = new int[countOfIntervals];
            double[] MiddlesOfTheIntervals = new double[countOfIntervals];
            
            for (int i =0; i < countOfIntervals; i++)
            {
                CounterOfNumbersOnInterval[i] = 0;
            }

            // нахождение середин интервалов
            for (int i = 0; i < countOfIntervals; i++)
            {
                double leftBorderOfInterval = minValue + (i * intervalLength);
                double rightBorderOfInterval = minValue + ((i+1) * intervalLength);
                MiddlesOfTheIntervals[i] = (leftBorderOfInterval + rightBorderOfInterval) / 2;
            }


            for (int i = 0; i < NumberOfSamples; i++)
            {
                double leftBorderOfInterval = minValue;
                int j = 0;
                while(true)
                {
                    if (YNow[i] >= leftBorderOfInterval && YNow[i] < leftBorderOfInterval + intervalLength)
                    {
                        CounterOfNumbersOnInterval[j]++;
                        break;
                    }

                    // случай попадания варианта на крайнюю правую границу
                    if (j == countOfIntervals - 1 && YNow[i] >= leftBorderOfInterval + intervalLength) 
                    {
                        CounterOfNumbersOnInterval[j]++;
                        break;
                    }
                    
                    j++;
                    if (j == countOfIntervals)
                        break;
                    leftBorderOfInterval += intervalLength;
                }
            }
            int summ = 0; // для проверки (должна быть равна количеству выборок)
            for (int j = 0; j < countOfIntervals; j++)
            {
                summ += CounterOfNumbersOnInterval[j];
            }
            double summs = 0; // для проверки (должна быть равна 1)
            for (int i = 0; i < countOfIntervals; i++)
            {
                ProbabilitiesOfHittingTheInerval[i] = (double)CounterOfNumbersOnInterval[i] / NumberOfSamples;
                summs += ProbabilitiesOfHittingTheInerval[i];
                chart.Series[0].Points.AddXY(MiddlesOfTheIntervals[i], ProbabilitiesOfHittingTheInerval[i]);
            }
        }
        void generateXSample()
        {
            // Создаём набор чисел с равномерным законом распределения на интервале (0,1)
            for (int i = 0; i < NumberOfSampleElements; i++)
            {
                X[i] = rnd.NextDouble();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            firstChart.Series[0].Points.Clear();
            secondChart.Series[0].Points.Clear();
            methods();
        }
    }
}
