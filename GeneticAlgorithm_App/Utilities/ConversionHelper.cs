using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm_App.Utilities
{
    internal static class ConversionHelper
    {
        public static int XRealToXInt(int a, int b, double x_real, int l) =>
            (int)Math.Floor(1.0 / (b - a) * (x_real - a) * (Math.Pow(2, l) - 1));

        public static float XIntToXReal(int a, int b, int x_int, int l, float d) =>
            x_int * (b - a) / (float)(Math.Pow(2, l) - 1) + a;

        public static string XIntToXBin(int num, int l) =>
            Convert.ToString(num, 2).PadLeft(l, '0');

        public static int XBinToXInt(string xbin) =>
            Convert.ToInt32(xbin, 2);
    }
}
