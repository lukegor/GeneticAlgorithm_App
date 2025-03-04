﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm_App
{
    /// <summary>
    /// Processes and runs the genetic algorithm.
    /// </summary>
    internal class GeneticAlgorithmProcessor
    {
        /// <summary>
        /// Processes a single generation of the genetic algorithm.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="l"></param>
        /// <param name="n"></param>
        /// <param name="d"></param>
        /// <param name="p_k"></param>
        /// <param name="p_m"></param>
        /// <param name="prec"></param>
        /// <param name="lastXreals"></param>
        /// <param name="isElite"></param>
        /// <returns>A tuple containing the minimum, maximum, and average fitness values, the last generation of x real values, the index of the maximum fitness value, and the elite x real value.</returns>
        internal (double minFxVal, double maxFxVal, double avgFxVal, double[] lastXreals, int maxIndex, double EliteXreal) ProcessGeneration(
            int a, int b, int l, int n, float d, float p_k, float p_m, int prec, double[] lastXreals, bool isElite)
        {
            GenerationCycle gen = new GenerationCycle();

            // Early Steps
            double[] xrealArray = lastXreals == null
                ? gen.FillFirstTwoColumnsAndXrealArray(a, b, n, prec)
                : gen.RoundXrealArray(prec, lastXreals);

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

        /// <summary>
        /// Runs the genetic algorithm for a specified number of generations.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="l"></param>
        /// <param name="prec"></param>
        /// <returns>A tuple containing the last generation of x real values, and arrays of minimum, maximum, and average fitness values by generation.</returns>
        public (double[], double[], double[], double[]) RunGeneticAlgorithm(GeneticAlgorithmParams settings, int l, int prec)
        {
            int n = settings.PopulationSize, T = settings.Generations;

            double[] lastXreals = new double[n];
            double[] minFxByGeneration = new double[T], maxFxByGeneration = new double[T], avgFxByGeneration = new double[T];

            for (int counter = 0; counter < T; counter++)
            {
                (minFxByGeneration[counter], maxFxByGeneration[counter], avgFxByGeneration[counter], lastXreals, _, _) =
                    ProcessGeneration(settings.LowerBound, settings.UpperBound, l, n, settings.PrecisionIndicator,
                                        settings.CrossoverProbability, settings.MutationProbability, prec, counter == 0 ? null : lastXreals, settings.IsElite);
            }

            return (lastXreals, minFxByGeneration, maxFxByGeneration, avgFxByGeneration);
        }
    }
}
