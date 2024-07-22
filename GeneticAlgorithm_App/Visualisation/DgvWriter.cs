using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm_App.Visualisation
{
    internal static class DgvWriter
    {
        public static void DisplaySharesDgv(DataGridView dgvShares, List<ShareData> shareData)
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

        public static void PopulateTopResultsDgv(DataGridView dgbSimulationTopResults, List<(int, double, double, int, double)> sortedTestResults, int limit)
        {
            dgbSimulationTopResults.Rows.Clear();

            for (int i = 0; i < limit && i < sortedTestResults.Count; ++i)
            {
                dgbSimulationTopResults.Rows.Add(
                    i + 1,
                    sortedTestResults[i].Item1,
                    sortedTestResults[i].Item2,
                    sortedTestResults[i].Item3,
                    sortedTestResults[i].Item4,
                    sortedTestResults[i].Item5
                );
            }
        }

        public static void FillColumnWithArray<T>(DataGridView dgv, int columnIndex, T[] data)
        {
            if (columnIndex < 0 || columnIndex >= dgv.ColumnCount)
                throw new ArgumentOutOfRangeException(nameof(columnIndex), "Column index is out of range.");

            if (dgv.RowCount != data.Length)
                throw new ArgumentException("The length of the data array must match the number of rows in the DataGridView.");

            for (int i = 0; i < data.Length; i++)
            {
                dgv.Rows[i].Cells[columnIndex].Value = data[i];
            }
        }
    }
}
