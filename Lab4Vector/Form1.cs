using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using LinearAlgebra;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab4Vector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void ShowChart(string name, Chart chart, int type, MathVector[] vectors)
        {
            Series series = new Series(name);
            series.ChartType = SeriesChartType.Bar;
            for (int i = 0; i < vectors.Length; i++)
            {
                series.Points.AddY(vectors[i][type]);
            }

            chart.Series.Add(series);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Loader loader = new Loader();
            MathVector[] vectors = loader.LoadFile();

            ShowChart("sepal_length", chart1, 0, vectors);
            ShowChart("sepal_width", chart2, 1, vectors);
            ShowChart("petal_length", chart3, 2, vectors);
            ShowChart("petal_width", chart4, 3, vectors);

            double evk1 = vectors[0].CalcDistance(vectors[1]);
            double evk2 = vectors[0].CalcDistance(vectors[2]);
            double evk3 = vectors[1].CalcDistance(vectors[2]);

            chart5.Series[0].Points.AddXY("setosa - versicolor", evk1);
            chart5.Series[0].Points.AddXY("setosa - virginica", evk2);
            chart5.Series[0].Points.AddXY("versicolor - virginica", evk3);

        }
    }
}
