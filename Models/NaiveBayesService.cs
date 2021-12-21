namespace NaiveBayesAssignment.Models
{
    public class NaiveBayesService : INaiveBayesService
    {
        private Model[] _Model;

        public void Fit(float[][] X, int[] y)
        {
            var model = CreateModel(X, y);
            // PrintModel(model);
            _Model = model.ToArray();
        }

        public int[] Predict(float[][] X)
        {
            var preds = new List<int>();
            foreach (var row in X)
            {
                var tmpPred = new List<double[]>();
                for (int i = 0; i < _Model.Count(); i++)
                {
                    var quotient = new double[5];
                    foreach (var column in row)
                    {
                        var indexValue = _Model[i].Calculation[Array.IndexOf(row, column)];

                        var power = Math.Pow((column - indexValue.Mean), 2) / (2 * Math.Pow(indexValue.StandardDeviation, 2));
                        var dividend = Math.Pow(Math.E, -power);
                        var divisor = indexValue.StandardDeviation * Math.Sqrt(2 * Math.PI);

                        quotient[Array.IndexOf(row, column)] = dividend / divisor;
                        var log = Math.Log(quotient[0]) + Math.Log(quotient[1]) + Math.Log(quotient[2]) + Math.Log(quotient[3]);
                        quotient[4] = Math.Exp(log);
                    }
                    tmpPred.Add(quotient);
                }
                var best = tmpPred.Aggregate((max, x) => x[4] > max[4] ? x : max);
                preds.Add(tmpPred.IndexOf(best));
            }
            // PrintPreds(preds);
            return preds.ToArray();
        }

        public float AccuracyScore(int[] preds, int[] y)
        {
            float count = 0;
            for (int i = 0; i < preds.Length; i++)
                if (preds[i] == y[i]) count++;
            return count / preds.Length;
        }

        public int[][] ConfusionMatrix(int[] preds, int[] y)
        {
            var count = y.Distinct().Count();
            var confMatrix = new int[count][];

            for (int i = 0; i < count; i++)
                confMatrix[i] = new int[count];

            for (int i = 0; i < preds.Length; i++)
                confMatrix[y[i]][preds[i]] += 1;

            return confMatrix;
        }

        public int[] CrossvalPredict(float[][] X, int[] y, int folds)
        {
            if (folds <= 0) return new int[0];
            var preds = new List<int[]>();

            var splitX = new List<float[][]>();
            var splitY = new List<int[]>();
            for (int i = 0; i < folds; i++)
            {
                splitX.Add(X.Skip(i * X.Count() / folds).Take(X.Count() / folds).ToArray());
                splitY.Add(y.Skip(i * y.Count() / folds).Take(y.Count() / folds).ToArray());
            }

            for (int i = 0; i < splitX.Count; i++)
            {
                var testX = splitX.ElementAt(i).ToArray();
                var trainingX = splitX.Where((source, index) => index != i).ToArray();
                var trainingY = splitY.Where((source, index) => index != i).ToArray();
                Fit(trainingX.SelectMany(i => i).ToArray(), trainingY.SelectMany(i => i).ToArray());
                preds.Add(Predict(testX));
            }
            return preds.SelectMany(i => i).ToArray();
        }

        private List<Model> CreateModel(float[][] X, int[] y)
        {
            var labels = new List<Model>();
            foreach (var label in y.Distinct())
            {
                int count = y.Count(s => s == label);
                labels.Add(new Model
                {
                    Key = label,
                    Calculation = new Calculation[4]
                    {
                        CreateCalculation(0, label, SumArrayColumn(0, label, X, y), count, X, y),
                        CreateCalculation(1, label, SumArrayColumn(1, label, X, y), count, X, y),
                        CreateCalculation(2, label, SumArrayColumn(2, label, X, y), count, X, y),
                        CreateCalculation(3, label, SumArrayColumn(3, label, X, y), count, X, y),
                    },
                    Count = count
                });
            }
            return labels;
        }
        private Calculation CreateCalculation(int key, int column, float sum, int count, float[][] X, int[] y)
        {
            return new Calculation
            {
                Key = key,
                Sum = sum,
                Mean = sum / count,
                StandardDeviation = CalculateStandardDeviations(sum / count, count, key, column, X, y)
            };
        }

        private float CalculateStandardDeviations(float mean, int count, int column, int key, float[][] X, int[] y)
        {
            float total = 0;
            for (int row = 0; row < X.Count(); row++)
            {
                if (y[row] == key)
                    total += (float)Math.Pow(X[row][column] - mean, 2);
            }
            return (float)Math.Sqrt(total / count);
        }
        private float SumArrayColumn(int column, int key, float[][] X, int[] y)
        {
            float sum = 0;
            for (int row = 0; row < X.Count(); row++)
            {
                if (y[row] == key)
                    sum += X[row][column];
            }
            return sum;
        }

        private void PrintModel(List<Model> model)
        {
            foreach (var item in model)
            {
                Console.WriteLine("-----NEWLINE-----");
                Console.WriteLine(item.Key);
                Console.WriteLine(item.Count);
                foreach (var value in item.Calculation)
                {
                    Console.WriteLine(value.Key);
                    Console.WriteLine(value.Sum);
                    Console.WriteLine(value.Mean);
                    Console.WriteLine(value.StandardDeviation);
                }
            }
        }
        private void PrintPreds(List<int> preds)
        {
            foreach (var item in preds)
            {
                Console.WriteLine(item);
            }
        }

        public void PrintConfusionMatrix(int[][] matrix)
        {
            for (int i = 0; i < matrix.Count(); i++)
            {
                for (int j = 0; j < matrix[i].Count(); j++)
                {
                    Console.Write(string.Format("{0} ", matrix[i][j]));
                }
                Console.Write(Environment.NewLine);
            }
        }
    }
}
