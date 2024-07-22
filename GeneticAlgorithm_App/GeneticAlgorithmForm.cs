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
using GeneticAlgorithm_App.Visualisation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace GeneticAlgorithm_App
{
    public partial class GeneticAlgorithmForm : Form
    {
        private GeneticAlgorithmProcessor _processor = new GeneticAlgorithmProcessor();

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

        [Description("a")]
        public int LowerBound => int.Parse(txtLowerBound.Text);
        [Description("b")]
        public int UpperBound => int.Parse(txtUpperBound.Text);
        [Description("n")]
        public int PopulationSize => int.Parse(txtPopulationSize.Text);
        [Description("d")]
        public float PrecisionIndicator => float.Parse(cbxPrecision.SelectedItem.ToString());
        [Description("p_k/pk")]
        public float CrossoverProbability => float.Parse(txtCrossoverProbability.Text);
        [Description("p_m/pm")]
        public float MutationProbability => float.Parse(txtMutationProbability.Text);
        [Description("T/t")]
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
            (lastXreals, minFxByGeneration, maxFxByGeneration, avgFxByGeneration) = _processor.RunGeneticAlgorithm(settings, l, prec);

            chart.Series.Clear();
            ChartVisualizer.ConfigureChartAppearance(chart);
            ChartVisualizer.PopulateChart(chart, settings.Generations, minFxByGeneration, maxFxByGeneration, avgFxByGeneration);

            var shareData = ShareCalculator.CalculateShares(settings.LowerBound, settings.UpperBound, l, lastXreals);
            DisplayShares(shareData);
        }

        private void DisplayShares(List<ShareData> shareData)
        {
            dgvShares.Rows.Clear();

            foreach (var data in shareData.Select((value, index) => new { value, index }))
            {
                dgvShares.Rows.Add(
                    data.index + 1,
                    data.value.XReal,
                    data.value.XBin,
                    data.value.FxValue,
                    data.value.Percentage
                );
            }
        }

        private void BtnSimulation_Click(object sender, EventArgs e)
        {
            var settings = GetCurrentAlgorithmParams();
            var simulationRunner = new SimulationRunner(settings, _processor);
            var simulationResults = simulationRunner.RunSimulations();

            PopulateTopResults(simulationResults);
        }

        private void PopulateTopResults(List<(int, double, double, int, double)> sortedTestResults)
        {
            for (int i = 0; i < 50 && i < sortedTestResults.Count; ++i)
            {
                dgbSimulationTopResults.Rows.Add(i + 1, sortedTestResults[i].Item1, sortedTestResults[i].Item2, sortedTestResults[i].Item3, sortedTestResults[i].Item4, sortedTestResults[i].Item5);
            }
        }
    }
}