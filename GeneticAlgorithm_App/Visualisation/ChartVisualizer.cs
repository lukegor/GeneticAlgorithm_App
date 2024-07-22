using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace GeneticAlgorithm_App.Visualisation
{
    internal static class ChartVisualizer
    {
        internal static void ConfigureChartAppearance(Chart chart)
        {
            var chartArea = chart.ChartAreas[0];
            chartArea.AxisX.IsStartedFromZero = false;
            chartArea.AxisY.IsStartedFromZero = false;

            chartArea.AxisY.Interval = 1;
            chartArea.AxisX.Interval = 10;

            chartArea.AxisY.Minimum = -2;
            chartArea.AxisY.Maximum = 2;

            chartArea.AxisX.Title = "pokolenie (T)";
            chartArea.AxisY.Title = "wartość fx";
        }

        internal static void PopulateChart(Chart chart, int T, double[] minFxByGeneration, double[] maxFxByGeneration, double[] avgFxByGeneration)
        {
            // Define series configuration
            static void ConfigureSeries(Series series, string name, Color color)
            {
                series.Name = name;
                series.ChartType = SeriesChartType.Line;
                series.Color = color;
                series.BorderWidth = 3;
            }

            // Create and configure series
            var seriesMin = new Series();
            var seriesMax = new Series();
            var seriesAvg = new Series();

            ConfigureSeries(seriesMin, "min fx", Color.Red);
            ConfigureSeries(seriesMax, "max fx", Color.Blue);
            ConfigureSeries(seriesAvg, "avg fx", Color.Green);

            // Add data points to series
            for (int i = 0; i < T; ++i)
            {
                seriesMin.Points.AddXY(i + 1, minFxByGeneration[i]);
                seriesMax.Points.AddXY(i + 1, maxFxByGeneration[i]);
                seriesAvg.Points.AddXY(i + 1, avgFxByGeneration[i]);
            }

            // Add series to chart
            chart.Series.Clear();
            chart.Series.Add(seriesMin);
            chart.Series.Add(seriesMax);
            chart.Series.Add(seriesAvg);
        }
    }
}
