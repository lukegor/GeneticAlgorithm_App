using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm_App.Utilities
{
    internal static class RandomNumberHelper
    {
        private static readonly Random random = new Random();

        public static float GetRandomNumberWithPrecision(int min, int max, int prec) =>
            (float)Math.Round(random.NextDouble() * (max - min) + min, prec);

        public static float GetRandomFloatNumber(int min, int max) =>
            (float)(random.NextDouble() * (max - min) + min);

        public static int GetRandomIntNumber(int min, int max) =>
            random.Next(min, max);

        public static int GetRandomIntNumber() => random.Next();

        public static double GetNextDouble() =>
            random.NextDouble();

    }
}
