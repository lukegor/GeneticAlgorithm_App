using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm_App
{
    internal class DgvWriter
    {
        private DataGridView _dataGridView;

        public DgvWriter() { }
        public DgvWriter(DataGridView dataGridView, int n)
        {
            _dataGridView = dataGridView;
        }

        public void Write(Array arr, int col)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                _dataGridView[col, i].Value = arr.GetValue(i);
            }
        }
    }
}
