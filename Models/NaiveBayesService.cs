namespace NaiveBayesAssignment.Models
{
    public class NaiveBayesService : INaiveBayesService
    {
        private List<Model> _Model;

        public void Fit(float[][] X, int[] y)
        {
            var model = CreateModel(X, y);
            PrintModel(model);
            _Model = model;
        }

        public int[] Predict(float[][] X)
        {
            var preds = new int[X.Count()];
            foreach (var row in X)
            {
                foreach (var column in row)
                {

                }

            }

            return preds;
        }
        public float AccuracyScore(int[] preds, int[] y)
        {
            throw new NotImplementedException();
        }

        public int[][] ConfusionMatrix(int[] preds, int[] y)
        {
            throw new NotImplementedException();
        }

        public int[] CrossvalPredict(float[][] X, int[] y, int folds)
        {
            throw new NotImplementedException();
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
                    Calculation = new List<Calculation>
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
    }
}
