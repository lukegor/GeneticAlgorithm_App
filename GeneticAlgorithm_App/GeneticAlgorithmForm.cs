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
    /// <summary>
    /// Main form for the Genetic Algorithm application.
    /// </summary>
    public partial class GeneticAlgorithmForm : Form
    {
        private readonly GeneticAlgorithmProcessor _processor = new GeneticAlgorithmProcessor();

        public GeneticAlgorithmForm()
        {
            InitializeComponent();
            SetDefaultValues();
        }

        /// <summary>
        /// Sets the default values for the form controls.
        /// </summary>
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

        /// <summary>
        /// Retrieves the current algorithm parameters from the form.
        /// </summary>
        /// <returns>The current <see cref="GeneticAlgorithmParams"/>.</returns>
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

        /// <summary>
        /// Handles the click event of the Calculate button, running the genetic algorithm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            #region Preparations

            this.dgvStatistics.Rows.Clear();

            var settings = GetCurrentAlgorithmParams();

            int prec = GeneticAlgorithmParams.GetPrecInNumber(settings.PrecisionIndicator);
            int l = GenerationCycle.GetLValue(settings.LowerBound, settings.UpperBound, settings.PrecisionIndicator);

            #endregion

            var (lastXreals, minFxByGeneration, maxFxByGeneration, avgFxByGeneration) = _processor.RunGeneticAlgorithm(settings, l, prec);

            chart.Series.Clear();
            ChartVisualizer.ConfigureChartAppearance(chart);
            ChartVisualizer.PopulateChart(chart, settings.Generations, minFxByGeneration, maxFxByGeneration, avgFxByGeneration);

            var shareData = ShareCalculator.CalculateShares(settings.LowerBound, settings.UpperBound, l, lastXreals);
            DgvWriter.DisplaySharesDgv(dgvShares, shareData);
        }

        /// <summary>
        /// Handles the click event of the Simulation button, running simulations.
        /// </summary>
        private void BtnSimulation_Click(object sender, EventArgs e)
        {
            var settings = GetCurrentAlgorithmParams();
            var simulationRunner = new SimulationRunner(settings, _processor);
            var simulationResults = simulationRunner.RunSimulations();

            int limit = 50;
            DgvWriter.PopulateTopResultsDgv(dgvSimulationTopResults, simulationResults, limit);
        }
    }
}