using System;
using System.Collections;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using GeneticAlgorithm_App.Utilities;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace GeneticAlgorithm_App
{
    public partial class GeneticAlgorithmForm : Form
    {
        public GeneticAlgorithmForm()
        {
            InitializeComponent();
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            this.txtLowerBound.Text = "-4";
            this.txtUpperBound.Text = "12";
            this.cbxPrecision.SelectedIndex = 2;
            this.txtPopulationSize.Text = "10";
            this.txtCrossoverProbability.Text = "0,7";
            this.txtMutationProbability.Text = "0,002";
            this.txtGoal.SelectedIndex = 0;
            this.txtNoGenerations.Text = "100";
            this.chbIsElite.Checked = true;
        }

        public int LowerBound => int.Parse(txtLowerBound.Text);
        public int UpperBound => int.Parse(txtUpperBound.Text);
        public int PopulationSize => int.Parse(txtPopulationSize.Text);
        public float PrecisionIndicator => float.Parse(cbxPrecision.SelectedItem.ToString());
        public float CrossoverProbability => float.Parse(txtCrossoverProbability.Text);
        public float MutationProbability => float.Parse(txtMutationProbability.Text);
        public int Generations => int.Parse(txtNoGenerations.Text);
        public bool IsElite => chbIsElite.Checked;

        private GeneticAlgorithmParams GetCurrentAlgorithmParams()
        {
            return new GeneticAlgorithmParams
            {
                LowerBound = this.LowerBound,
                UpperBound = this.UpperBound,
                PopulationSize = this.PopulationSize,
                PrecisionIndicator = this.PrecisionIndicator,
                CrossoverProbability = this.CrossoverProbability,
                MutationProbability = this.MutationProbability,
                Generations = this.Generations,
                IsElite = this.IsElite
            };
        }

        private (double minFxVal, double maxFxVal, double avgFxVal, double[] lastXreals, int maxIndex, double EliteXreal) ProcessGeneration(
            int a, int b, int l, int n, float d, float p_k, float p_m, int prec, double[] lastXreals, bool isElite)
        {
            Generation gen = new Generation();

            // Early Steps
            double[] xrealArray = lastXreals == null
                ? gen.FillFirstTwoColumnsAndXrealArray(a, b, n, prec)
                : gen.FillFirstTwoColumnsAndXrealArray(a, b, n, prec, lastXreals);

            double[] fxValuesArray = gen.FillFxColumnAndFxValuesArray(xrealArray);

            // Find the index of the maximum value for elite handling
            int eliteIndex = isElite ? fxValuesArray.ToList().IndexOf(fxValuesArray.Max()) : -1;

            double[] gxValuesArray = gen.FillGxColumnAndGxValuesArray(d, fxValuesArray);
            double[] pArray = gen.FillP_iColumnAndPArray(gxValuesArray);
            double[] qArray = gen.FillQ_iColumnAndQArray(pArray);
            double[] rArray = gen.FillRColumnAndRArray(n);

            double[] selected = gen.FillSelectedColumnAndSelectedArray(prec, xrealArray, qArray, rArray);
            string[] xbinArr = gen.FillXbinColumnAndXbinArray(a, b, l, selected);

            // Parent Preparation
            var (parents, parentsFromDataGridView) = gen.FillParentsColumnAndParentsArray(n, p_k, xbinArr);
            int parentCounter = gen.CountParents(parents);
            var (filteredParents, indexesOfFiltered) = gen.FilterParentsAndTheirIndexes(parentCounter, parents);

            // Pairing & P_c
            var pairs = gen.PerformPairingAndAddToListWithRandomNumber(l, filteredParents);
            gen.FillP_cColumn(parents, pairs, parentsFromDataGridView);

            // Crossing
            var children = gen.PerformCrossingAndReturnOnlyChildren(pairs);

            // Post-Crossing
            var allPostCrossing = gen.HandlePostCrossingArray(n, children, indexesOfFiltered, xbinArr);

            // Mutation & Ending
            var mutatedIndexes = gen.HandleMutatedBitsArray(n, l, p_m, allPostCrossing);
            var postMutation = gen.AfterMutationColumnAndFillPostMutationArray(n, l, mutatedIndexes, allPostCrossing);
            var finalXRealArray = gen.HandleFinalXreals(a, b, n, l, postMutation, d);
            var finalFxValuesArray = gen.HandleFinalFxValues(n, finalXRealArray);

            // Elite Handling
            gen.UpdateEliteArrays(n, isElite, xrealArray, fxValuesArray, eliteIndex, finalXRealArray, finalFxValuesArray);

            double min = finalFxValuesArray.Min();
            double max = finalFxValuesArray.Max();
            double avg = finalFxValuesArray.Average();
            int maxIndex = Array.IndexOf(finalFxValuesArray, max);

            return (min, max, avg, finalXRealArray, maxIndex, finalXRealArray[maxIndex]);
        }

        private (double[], double[], double[], double[]) RunGeneticAlgorithm(GeneticAlgorithmParams settings, int l, int prec, double[] lastXreals, double[] minFxByGeneration, double[] maxFxByGeneration, double[] avgFxByGeneration)
        {
            bool isElite = settings.IsElite;
            int T = settings.Generations;
            int n = settings.PopulationSize;
            float d = settings.PrecisionIndicator;
            float p_k = settings.CrossoverProbability;
            float p_m = settings.MutationProbability;
            int a = settings.LowerBound;
            int b = settings.UpperBound;

            for (int counter = 0; counter < T; counter++)
            {
                //this.dgvStatistics.Rows?.Clear();
                (minFxByGeneration[counter], maxFxByGeneration[counter], avgFxByGeneration[counter], lastXreals, _, _) =
                    ProcessGeneration(a, b, l, n, d, p_k, p_m, prec, counter == 0 ? null : lastXreals, isElite);
            }

            maxDoubles = maxFxByGeneration.ToList();
            return (lastXreals, minFxByGeneration, maxFxByGeneration, avgFxByGeneration);
        }

        public List<double> maxDoubles = new List<double>();

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            #region Preparations

            this.dgvStatistics.Rows.Clear();

            var settings = GetCurrentAlgorithmParams();

            int prec = GeneticAlgorithmParams.GetPrecInNumber(settings.PrecisionIndicator);
            int l = Generation.GetLValue(settings.LowerBound, settings.UpperBound, settings.PrecisionIndicator);

            int n = settings.PopulationSize;
            int T = settings.Generations;
            int a = settings.LowerBound;
            int b = settings.UpperBound;
            #endregion

            double[] lastXreals = new double[n];
            double[] minFxByGeneration = new double[T], maxFxByGeneration = new double[T], avgFxByGeneration = new double[T];
            (lastXreals, minFxByGeneration, maxFxByGeneration, avgFxByGeneration) = RunGeneticAlgorithm(settings, l, prec, lastXreals, minFxByGeneration, maxFxByGeneration, avgFxByGeneration);

            chart.Series.Clear();

            ConfigureChartAppearance(chart);
            PopulateChart(T, minFxByGeneration, maxFxByGeneration, avgFxByGeneration);
            CalculateAndDisplayShares(a, b, l, lastXreals);
        }

        private void CalculateAndDisplayShares(int a, int b, int l, double[] lastXreals)
        {
            var totalElements = lastXreals.Length;

            var groupedData = lastXreals
                .GroupBy(value => value)
                .Select(grp => new
                {
                    XReal = grp.Key,
                    Count = grp.Count(),
                    Percentage = (double)grp.Count() / totalElements * 100
                })
                .OrderByDescending(item => item.Percentage)
                .ToList();

            dgvShares.Rows.Clear();

            for (int i = 0; i < groupedData.Count; i++)
            {
                var group = groupedData[i];
                dgvShares.Rows.Add(
                    i + 1,
                    group.XReal,
                    ConversionHelper.XIntToXBin(ConversionHelper.XRealToXInt(a, b, group.XReal, l), l),
                    Generation.CountFxValue(group.XReal),
                    group.Percentage
                );
            }
        }

        private void BtnSimulation_Click(object sender, EventArgs e)
        {
            List<int> nLoop = new List<int> { 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80 };
            List<double> pkLoop = new List<double> { 0.5, 0.55, 0.6, 0.65, 0.7, 0.75, 0.8, 0.85, 0.9 };
            List<double> pmLoop = new List<double> { 0.0001, 0.0005, 0.001, 0.005, 0.01 };
            List<int> tLoop = new List<int> { 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150 };

            ConcurrentBag<(int, double, double, int, double)> testResults = new ConcurrentBag<(int, double, double, int, double)>();

            var _settings = GetCurrentAlgorithmParams();

            int a = _settings.LowerBound;
            int b = _settings.UpperBound;
            float d = _settings.PrecisionIndicator;
            int prec = GeneticAlgorithmParams.GetPrecInNumber(d);
            int l = Generation.GetLValue(a, b, d);
            bool isElite = _settings.IsElite;

            var totalCases = nLoop.Count * pkLoop.Count * pmLoop.Count * tLoop.Count;
            var processedCases = 0;

            var allCases = from n in nLoop
                           from pk in pkLoop
                           from pm in pmLoop
                           from t in tLoop
                           select new { n, pk, pm, t };

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Parallel.ForEach(allCases, caseParams =>
            {
                var (n, pk, pm, t) = (caseParams.n, caseParams.pk, caseParams.pm, caseParams.t);
                List<double> allMaxFx = new List<double>();

                double max = -1.9999;

                for (int rep = 0; rep < 100; ++rep)
                {
                    double[] lastXreals = new double[n];
                    double[] maxFxByGeneration = new double[t];
                    double[] minFxByGeneration = new double[t];
                    double[] avgFxByGeneration = new double[t];

                    for (int counter = 0; counter < t; counter++)
                    {


                        //this.dgvStatistics.Rows?.Clear();
                        (minFxByGeneration[counter], maxFxByGeneration[counter], avgFxByGeneration[counter], lastXreals, _, _) =
                            ProcessGeneration(a, b, l, n, d, (float)pk, (float)pm, prec, counter == 0 ? null : lastXreals, isElite);

                        if(maxFxByGeneration.Max() > max)
                        {
                            max = maxFxByGeneration.Max();
                            allMaxFx.Add(max);
                        }
                    }
                }

                double averageFx = allMaxFx.Average();
                testResults.Add((n, pk, pm, t, averageFx));

                // Increment processed cases and update progress counter
                Interlocked.Increment(ref processedCases);
                System.Diagnostics.Debug.WriteLine($"Progress: {processedCases}/{totalCases}");
            });

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;

            System.Diagnostics.Debug.WriteLine($"Execution time: {ts.Hours} hours, {ts.Minutes} minutes, {ts.Seconds} seconds");

            var sortedTestResults = testResults.OrderByDescending(t => t.Item5).ThenBy(t => t.Item3).ToList();

            for (int i = 0; i < 50 && i < sortedTestResults.Count; ++i)
            {
                dgbSimulationTopResults.Rows.Add(i + 1, sortedTestResults[i].Item1,sortedTestResults[i].Item2, sortedTestResults[i].Item3, sortedTestResults[i].Item4, sortedTestResults[i].Item5);
            }
        }

        #region chart
        private void ConfigureChartAppearance(Chart chart)
        {
            var chartArea = chart.ChartAreas[0];
            chartArea.AxisX.IsStartedFromZero = false;
            chartArea.AxisY.IsStartedFromZero = false;

            chartArea.AxisY.Interval = 1;
            chartArea.AxisX.Interval = 10;

            chartArea.AxisY.Minimum = -2;
            chartArea.AxisY.Maximum = 2;

            chartArea.AxisX.Title = "pokolenie (T)";
            chartArea.AxisY.Title = "wartoœæ fx";
        }

        private void PopulateChart(int T, double[] minFxByGeneration, double[] maxFxByGeneration, double[] avgFxByGeneration)
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
        #endregion
    }
}