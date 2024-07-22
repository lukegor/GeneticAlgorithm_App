using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm_App.Utilities
{
    /// <summary>
    /// Provides methods for converting between real values, integer values, and binary representations.
    /// </summary>
    internal static class ConversionHelper
    {
        /// <summary>
        /// Converts a real value to an integer representation based on given parameters.
        /// </summary>
        /// <param name="a">The lower bound of the range for the real values.</param>
        /// <param name="b">The upper bound of the range for the real values.</param>
        /// <param name="x_real">The real value to be converted.</param>
        /// <param name="l">The length of the binary representation.</param>
        /// <returns>The integer representation of the real value.</returns>
        public static int XRealToXInt(int a, int b, double x_real, int l) =>
            (int)Math.Floor(1.0 / (b - a) * (x_real - a) * (Math.Pow(2, l) - 1));

        /// <summary>
        /// Converts an integer representation to a real value based on given parameters.
        /// </summary>
        /// <param name="a">The lower bound of the range for the real values.</param>
        /// <param name="b">The upper bound of the range for the real values.</param>
        /// <param name="x_int">The integer representation to be converted.</param>
        /// <param name="l">The length of the binary representation.</param>
        /// <param name="d">The precision used in the conversion.</param>
        /// <returns>The real value corresponding to the integer representation.</returns>
        public static float XIntToXReal(int a, int b, int x_int, int l, float d) =>
            x_int * (b - a) / (float)(Math.Pow(2, l) - 1) + a;

        /// <summary>
        /// Converts an integer to its binary representation as a string.
        /// </summary>
        /// <param name="num">The integer to be converted to binary.</param>
        /// <param name="l">The length of the resulting binary string. If necessary, the string will be padded with leading zeros.</param>
        /// <returns>The binary representation of the integer as a string.</returns>
        public static string XIntToXBin(int num, int l) =>
            Convert.ToString(num, 2).PadLeft(l, '0');

        /// <summary>
        /// Converts a binary string representation to an integer.
        /// </summary>
        /// <param name="xbin">The binary string to be converted to an integer.</param>
        /// <returns>The integer representation of the binary string.</returns>
        public static int XBinToXInt(string xbin) =>
            Convert.ToInt32(xbin, 2);
    }
}
