using GeneticAlgorithm_App.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm_App
{
    internal class GenerationCycle
    {
        public static int GetLValue(int a, int b, float d) =>
            (int)Math.Ceiling(Math.Log2(((b - a) / d) + 1));

        public double[] FillArray(int n, Func<int, double> valueGenerator)
        {
            double[] result = new double[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = valueGenerator(i);
            }
            return result;
        }

        public double[] FillFirstTwoColumnsAndXrealArray(int a, int b, int n, int prec) =>
            FillArray(n, _ => RandomNumberHelper.GetRandomNumberWithPrecision(a, b, prec));

        public double[] FillFirstTwoColumnsAndXrealArray(int a, int b, int n, int prec, double[] lastXreals) =>
            lastXreals.Select(x => Math.Round(x, prec)).ToArray();

        public double[] FillFxColumnAndFxValuesArray(double[] xrealArray) =>
            FillArray(xrealArray.Length, i => CountFxValue(xrealArray[i]));

        public static double CountFxValue(double x) =>
            x % 1 * (Math.Cos(20 * Math.PI * x) - Math.Sin(x));

        public double[] FillGxColumnAndGxValuesArray(float d, double[] fxValuesArray)
        {
            double min = fxValuesArray.Min();
            double max = fxValuesArray.Max();
            return FillArray(fxValuesArray.Length, i => (double)CountGxValue(fxValuesArray[i], min, max, d));
        }

        public decimal CountGxValue(double fxValue, double fmin, double fmax, float d) =>
            (decimal)fxValue - (decimal)fmin + (decimal)d;

        public double[] FillP_iColumnAndPArray(double[] gxValuesArray)
        {
            double gxSum = gxValuesArray.Sum();
            return FillArray(gxValuesArray.Length, i => gxValuesArray[i] / gxSum);
        }

        public double[] FillQ_iColumnAndQArray(double[] pArray) =>
        FillArray(pArray.Length, i => pArray.Take(i + 1).Sum());

        public double[] FillRColumnAndRArray(int n) =>
            FillArray(n, _ => RandomNumberHelper.GetNextDouble());

        public double[] FillSelectedColumnAndSelectedArray(int prec, ReadOnlySpan<double> xrealArray, ReadOnlySpan<double> qArray, ReadOnlySpan<double> rArray)
        {
            int n = xrealArray.Length;
            double[] selected = new double[n];

            for (int rIterator = 0; rIterator < n; ++rIterator)
            {
                for (int qIterator = 0; qIterator < n; ++qIterator)
                {
                    double qValue = qArray[qIterator];
                    double rValue = rArray[rIterator];

                    if (qIterator == 0)
                    {
                        if (qValue >= rValue)
                        {
                            selected[rIterator] = Math.Round(xrealArray[qIterator], prec);
                            //dataGridView1[7, rIterator].Value = selected[rIterator];
                        }
                    }

                    else
                    {
                        double lastQValue = qArray[qIterator - 1];

                        if (qValue >= rValue && lastQValue < rValue)
                        {
                            selected[rIterator] = Math.Round(xrealArray[qIterator], prec);
                            //dataGridView1[7, rIterator].Value = selected[rIterator];
                        }
                    }
                }
            }
            return selected;
        }

        public string[] FillXbinColumnAndXbinArray(int a, int b, int l, ReadOnlySpan<double> selected) =>
            selected.ToArray().Select(value => ConversionHelper.XIntToXBin(ConversionHelper.XRealToXInt(a, b, value, l), l)).ToArray();

        public (string[], object[]) FillParentsColumnAndParentsArray(int n, double p_k, ReadOnlySpan<string> xbinArray)
        {
            string[] parents = new string[n];
            object[] parentsFromDataGridView = new object[n];

            do
            {
                for (int i = 0; i < n; ++i)
                {
                    if (string.IsNullOrEmpty(parents[i]))
                    {
                        double tmpR = RandomNumberHelper.GetNextDouble();
                        if (tmpR <= p_k)
                        {
                            parents[i] = xbinArray[i];
                            parentsFromDataGridView[i] = parents[i];
                            //dataGridView1[9, i].Value = parents[i];
                        }
                    }
                }
            } while (parents.Count(parent => parent != string.Empty) == 1);

            return (parents, parentsFromDataGridView);
        }

        public int CountParents(string[] parents) =>
            parents.Count(p => !string.IsNullOrEmpty(p));

        public (string[], int[]) FilterParentsAndTheirIndexes(int parentCounter, ReadOnlySpan<string> parents)
        {
            string[] filteredParents = new string[parentCounter];
            int[] indexesOfFiltered = new int[parentCounter];
            for (int i = 0, j = 0; j < filteredParents.Length; ++i)
            {
                if (!string.IsNullOrEmpty(parents[i]))
                {
                    filteredParents[j] = parents[i];
                    indexesOfFiltered[j] = i;
                    ++j;
                }
            }

            return (filteredParents, indexesOfFiltered);
        }

        public List<Tuple<string, string, int>> PerformPairingAndAddToListWithRandomNumber(int l, ReadOnlySpan<string> filteredParents)
        {
            List<Tuple<string, string, int>> pairs = new List<Tuple<string, string, int>>();
            for (int i = 0; i < filteredParents.Length - 1; i += 2)
            {
                int randomNum = RandomNumberHelper.GetRandomIntNumber(0, l - 2);
                Tuple<string, string, int> pair = new Tuple<string, string, int>(filteredParents[i], filteredParents[i + 1], randomNum);
                pairs.Add(pair);
            }
            if (filteredParents.Length % 2 == 1)
            {
                pairs.Add(new Tuple<string, string, int>(filteredParents[filteredParents.Length - 1], filteredParents[0], RandomNumberHelper.GetRandomIntNumber(0, l - 2)));
            }

            return pairs;
        }

        public void FillP_cColumn(ReadOnlySpan<string> parents, List<Tuple<string, string, int>> pairs, object[] parentsFromDataGridView)
        {
            int pom = 0;
            int pom2 = 0;
            int[] Pcs = new int[parents.Length];

            //for(int j = 0; j < parents.Length; ++j)
            //{
            //	bool check = dataGridView1[9, j].Value == parents[j];
            //	System.Diagnostics.Debug.WriteLine(dataGridView1[9, j].Value + " " + parents[j] + " " + check);
            //}
            //System.Diagnostics.Debug.WriteLine("new");
            for (int z = pom; z < parents.Length; ++z)
            {
                if (!string.IsNullOrEmpty(parents[z]))
                {
                    for (int pairIterator = pom2; pairIterator < pairs.Count; ++pairIterator)
                    {
                        //bool check = parents[z] == dataGridView1[9, z].Value;
                        //System.Diagnostics.Debug.WriteLine(check + "\n");
                        if (parentsFromDataGridView[z].ToString() == pairs[pairIterator].Item1 || parentsFromDataGridView[z].ToString() == pairs[pairIterator].Item2)
                        {
                            //bool check1 = parents[z] == pairs[pairIterator].Item1;
                            //bool check2 = parents[z] == pairs[pairIterator].Item2;
                            //System.Diagnostics.Debug.WriteLine("1. gridview: " + dataGridView1[9, z].Value + "\nrodzic: " + parents[z] + check1 + check2);
                            Pcs[z] = pairs[pairIterator].Item3;
                            //dataGridView1[10, z].Value = Pcs[z];
                            break;
                        }
                        //if (dataGridView1[9, z].Value == pairs[pairIterator].Item1 || dataGridView1[9, z].Value == pairs[pairIterator].Item2)
                        //{
                        //bool check1 = dataGridView1[9, z].Value == pairs[pairIterator].Item1;
                        //bool check2 = dataGridView1[9, z].Value == pairs[pairIterator].Item2;
                        //System.Diagnostics.Debug.WriteLine("\n2. gridview: " + dataGridView1[9, z].Value + "\nrodzic: " + parents[z] + check1 + check2 + "\n");
                        //}
                        //if (dataGridView1[9, z].Value == pairs[pairIterator].Item1 || dataGridView1[9, z].Value == pairs[pairIterator].Item2)
                        //{

                        //string parent = parents[z];
                        //string item1 = pairs[pairIterator].Item1;
                        //string item2 = pairs[pairIterator].Item2;

                        //bool check1 = dataGridView1[9, z].Value == item1;
                        //	bool check2 = dataGridView1[9, z].Value == item2;
                        //	bool check3 = parent == item1;
                        //	bool check4 = parent == item2;
                        //bool check5 = dataGridView1[9, z].Value == parents[z];
                        //	System.Diagnostics.Debug.WriteLine(check1 + " " + check2 + " vs " + check3 + " " + check4 + " " + check5);

                        //}
                        //if (parents[z] == pairs[pairIterator].Item1 || parents[z] == pairs[pairIterator].Item2)
                        //{
                        //	break;
                        //}

                        ++pom2;
                    }
                }
                ++pom;
            }
        }

        public List<string> PerformCrossingAndReturnOnlyChildren(List<Tuple<string, string, int>> pairs)
        {
            List<string> children = new List<string>();
            foreach (var pair in pairs)
            {
                string start1, start2, ending1, ending2;
                string child1, child2;

                int length1 = pair.Item1.Length;
                int length2 = pair.Item2.Length;
                int cutPoint = Convert.ToInt32(pair.Item3);

                int endDistance1 = length1 - cutPoint;
                int endDistance2 = length2 - cutPoint;

                start1 = pair.Item1.Substring(0, length1 - endDistance1);
                start2 = pair.Item2.Substring(0, length2 - endDistance2);
                ending1 = pair.Item1.Substring(length1 - endDistance1);
                ending2 = pair.Item2.Substring(length2 - endDistance2);

                child1 = start1 + ending2;
                child2 = start2 + ending1;

                children.Add(child1);
                children.Add(child2);
            }

            return children;
        }

        public string[] SaveChildrenToPostCrossingArray(int n, List<string> children, ReadOnlySpan<int> indexesOfFiltered)
        {
            string[] earlyCrossing = new string[n];

            int i = 0;
            foreach (int index in indexesOfFiltered)
            {
                earlyCrossing[index] = children[i];
                ++i;
            }

            return earlyCrossing;
        }

        public void FillEmptyFieldsWithRelevantXbin(ReadOnlySpan<string> xbinArr, string[] allPostCrossing)
        {
            int i = 0;
            foreach (string xBin in xbinArr)
            {
                if (string.IsNullOrEmpty(allPostCrossing[i]))
                {
                    allPostCrossing[i] = xBin;
                }
                ++i;
            }
        }

        public string[] HandlePostCrossingArray(int n, List<string> children, ReadOnlySpan<int> indexesOfFiltered, ReadOnlySpan<string> xbinArr)
        {
            string[] allPostCrossing = new string[n];

            //save every child to the array
            allPostCrossing = SaveChildrenToPostCrossingArray(n, children, indexesOfFiltered);

            //save every xbin in case a field in array "post crossing" is empty
            //fill every empty field in the "crossing" column with xbin (according to table)
            //MODIFIES array without returning
            FillEmptyFieldsWithRelevantXbin(xbinArr, allPostCrossing);

            for (int i = 0; i < n; ++i)
            {
                //dataGridView1[11, i].Value = allPostCrossing[i];
            }

            return allPostCrossing;
        }

        public int[,] FillMutatedIndexesArrayWithMinus1s(int n, int l)
        {
            int[,] mutatedIndexes = new int[n, l];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < l; j++)
                {
                    mutatedIndexes[i, j] = -1;
                }
            }

            return mutatedIndexes;
        }

        public (double[], double[]) HandleTwoLastColumns(int a, int b, int n, int l, string[] postCrossingCopy, float d)
        {
            double[] finalXRealArray = new double[n];
            double[] finalFxValuesArray = new double[n];

            for (int i = 0; i < n; ++i)
            {
                finalXRealArray[i] = Math.Round(ConversionHelper.XIntToXReal(a, b, ConversionHelper.XBinToXInt(postCrossingCopy[i]), l, d), GeneticAlgorithmParams.GetPrecInNumber(d));
                //dataGridView1[14, i].Value = finalXRealArray[i];

                finalFxValuesArray[i] = CountFxValue(finalXRealArray[i]);
                //dataGridView1[15, i].Value = finalFxValuesArray[i];
            }

            return (finalXRealArray, finalFxValuesArray);
        }

        public double[] HandleFinalXreals(int a, int b, int n, int l, string[] postCrossingCopy, float d) =>
            postCrossingCopy.Select(xbin => Math.Round(ConversionHelper.XIntToXReal(a, b, ConversionHelper.XBinToXInt(xbin), l, d), GeneticAlgorithmParams.GetPrecInNumber(d))).ToArray();

        public double[] HandleFinalFxValues(int n, ReadOnlySpan<double> finalXreals)
        {
            double[] finalFxValuesArray = new double[n];

            for (int i = 0; i < n; ++i)
            {
                finalFxValuesArray[i] = CountFxValue(finalXreals[i]);
                //dataGridView1[15, i].Value = finalFxValuesArray[i];
            }

            return finalFxValuesArray;
        }

        public void Fill2DArrayWithMutatedBitsIndexes(int n, int l, float p_m, ReadOnlySpan<string> allPostCrossing, int[,] mutatedIndexes)
        {
            StringBuilder[] sb = new StringBuilder[n];

            for (int i = 0; i < n; i++)
            {
                sb[i] = new StringBuilder();
            }


            for (int i = 0; i < n; ++i)
            {
                if (allPostCrossing[i] != null)
                {
                    string postCrossingString = allPostCrossing[i];

                    int j = 0;
                    foreach (char sign in postCrossingString)
                    {
                        double randomVal = RandomNumberHelper.GetRandomFloatNumber(0, 1);

                        if (randomVal <= p_m)
                        {
                            if (sb[i].Length != 0)
                            {
                                sb[i].Append(", ");
                            }
                            sb[i].Append($"{j}");

                            mutatedIndexes[i, j] = j;
                        }
                        ++j;
                    }
                    //dataGridView1[12, i].Value = sb[i];
                }
            }
        }

        public int[,] HandleMutatedBitsArray(int n, int l, float p_m, ReadOnlySpan<string> allPostCrossing)
        {
            int[,] mutatedIndexes = new int[n, l];

            //if an index is not mutated, fill it with -1, to differentiate from index 0 (since c# basically assigns 0 to empty field in int array)
            mutatedIndexes = FillMutatedIndexesArrayWithMinus1s(n, l);

            //loop every row, then loop every char (0/1) in each string
            //in order to check condition and find mutated bits
            //WARNING: this function operates on true mutatedIndexes array, without returning
            Fill2DArrayWithMutatedBitsIndexes(n, l, p_m, allPostCrossing, mutatedIndexes);

            return mutatedIndexes;
        }

        public string[] AfterMutationColumnAndFillPostMutationArray(int n, int l, int[,] mutatedIndexes, string[] allPostCrossing)
        {
            //make a copy of post-crossing generation just in case
            string[] postMutation = new string[n];
            Array.Copy(allPostCrossing, postMutation, n);

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < l; ++j)
                {
                    int value = mutatedIndexes[i, j];
                    if (value != -1)
                    {
                        if (postMutation[i][j] == '0')
                        {
                            postMutation[i] = postMutation[i].Remove(j, 1).Insert(j, "1");
                        }
                        else if (postMutation[i][j] == '1')
                        {
                            postMutation[i] = postMutation[i].Remove(j, 1).Insert(j, "0");
                        }
                    }
                }
                //dataGridView1[13, i].Value = postMutation[i];
            }

            return postMutation;
        }

        public void UpdateEliteArrays(int n, bool isElite, double[] xrealArray, double[] fxValuesArray, int eliteIndex, double[] finalXRealArray, double[] finalFxValuesArray)
        {
            if (isElite && eliteIndex >= 0)
            {
                if (finalXRealArray[eliteIndex] != xrealArray[eliteIndex])
                {
                    if (finalFxValuesArray[eliteIndex] < fxValuesArray[eliteIndex])
                    {
                        finalXRealArray[eliteIndex] = xrealArray[eliteIndex];
                        finalFxValuesArray[eliteIndex] = fxValuesArray[eliteIndex];
                    }
                    else
                    {
                        var shuffledList = Enumerable.Range(0, n).OrderBy(_ => RandomNumberHelper.GetRandomIntNumber()).ToList();
                        foreach (var idx in shuffledList)
                        {
                            if (finalFxValuesArray[idx] < fxValuesArray[eliteIndex])
                            {
                                finalXRealArray[idx] = xrealArray[eliteIndex];
                                finalFxValuesArray[idx] = fxValuesArray[eliteIndex];
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
