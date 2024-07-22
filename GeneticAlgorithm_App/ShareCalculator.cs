using GeneticAlgorithm_App.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm_App
{
    internal static class ShareCalculator
    {
        public static List<ShareData> CalculateShares(int a, int b, int l, double[] lastXreals)
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

            return groupedData.Select(group => new ShareData
            {
                XReal = group.XReal,
                XBin = ConversionHelper.XIntToXBin(ConversionHelper.XRealToXInt(a, b, group.XReal, l), l),
                FxValue = GenerationCycle.CountFxValue(group.XReal),
                Percentage = group.Percentage
            }).ToList();
        }
    }
}
