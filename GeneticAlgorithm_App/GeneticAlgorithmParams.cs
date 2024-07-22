using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm_App
{
    internal class GeneticAlgorithmParams
    {
        [Description("a")]
        public int LowerBound { get; set; }
        [Description("b")]
        public int UpperBound { get; set; }
        [Description("n")]
        public int PopulationSize { get; set; }
        [Description("d")]
        public float PrecisionIndicator { get; set; }
        [Description("p_k/pk")]
        public float CrossoverProbability { get; set; }
        [Description("p_m/pm")]
        public float MutationProbability { get; set; }
        [Description("T/t")]
        public int Generations { get; set; }
        public bool IsElite { get; set; }

        public GeneticAlgorithmParams() { }
        public GeneticAlgorithmParams(int lowerBound, int upperBound, int populationSize, float precisionIndicator, float crossoverProbability, float mutationProbability, int generations, bool isElite)
        {
            LowerBound = lowerBound;
            UpperBound = upperBound;
            PopulationSize = populationSize;
            PrecisionIndicator = precisionIndicator;
            CrossoverProbability = crossoverProbability;
            MutationProbability = mutationProbability;
            Generations = generations;
            IsElite = isElite;
        }

        /// <summary>
        /// Gets the precision as a number of decimal places based on the precision indicator.
        /// </summary>
        /// <param name="d">The precision indicator.</param>
        /// <returns>The number of decimal places corresponding to the precision indicator.</returns>
        /// <exception cref="InvalidDataException"></exception>
        public static int GetPrecInNumber(float d)
        {
            return d switch
            {
                0.1f => 1,
                0.01f => 2,
                0.001f => 3,
                0.0001f => 4,
                _ => throw new InvalidDataException()
            };
        }
    }
}
