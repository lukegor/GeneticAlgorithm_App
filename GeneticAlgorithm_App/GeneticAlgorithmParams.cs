using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm_App
{
    internal class GeneticAlgorithmParams
    {
        public int LowerBound { get; set; }
        public int UpperBound { get; set; }
        public int PopulationSize { get; set; }
        public float PrecisionIndicator { get; set; }
        public float CrossoverProbability { get; set; }
        public float MutationProbability { get; set; }
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
