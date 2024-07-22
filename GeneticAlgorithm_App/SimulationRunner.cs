using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm_App
{
    /// <summary>
    /// Runs simulations of the genetic algorithm with various parameters.
    /// </summary>
    internal class SimulationRunner
    {
        private readonly GeneticAlgorithmParams _settings;
        private readonly GeneticAlgorithmProcessor _processor;

        public SimulationRunner(GeneticAlgorithmParams settings, GeneticAlgorithmProcessor processor)
        {
            _settings = settings;
            _processor = processor;
        }

        /// <summary>
        /// Runs simulations of the genetic algorithm over a range of parameter values.
        /// </summary>
        /// <returns>A list of tuples containing the population size, crossover probability, mutation probability, number of generations, and average maximum fitness value for each of the best individuals in each run of the algorithm for different parameters.</returns>
        internal List<(int, double, double, int, double)> RunSimulations()
        {
            List<int> nLoop = new List<int> { 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80 };
            List<double> pkLoop = new List<double> { 0.5, 0.55, 0.6, 0.65, 0.7, 0.75, 0.8, 0.85, 0.9 };
            List<double> pmLoop = new List<double> { 0.0001, 0.0005, 0.001, 0.005, 0.01 };
            List<int> tLoop = new List<int> { 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150 };

            ConcurrentBag<(int, double, double, int, double)> testResults = new ConcurrentBag<(int, double, double, int, double)>();

            int a = _settings.LowerBound;
            int b = _settings.UpperBound;
            float d = _settings.PrecisionIndicator;
            int prec = GeneticAlgorithmParams.GetPrecInNumber(d);
            int l = GenerationCycle.GetLValue(a, b, d);
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

                    var settings = new GeneticAlgorithmParams(a, b, n, d, (float)pk, (float)pm, t, isElite);

                    (minFxByGeneration, maxFxByGeneration, avgFxByGeneration, lastXreals) =
                        _processor.RunGeneticAlgorithm(settings, l, prec);

                    if (maxFxByGeneration.Max() > max)
                    {
                        max = maxFxByGeneration.Max();
                        allMaxFx.Add(max);
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
            return sortedTestResults;
        }
    }
}
