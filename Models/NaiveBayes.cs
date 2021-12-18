namespace NaiveBayes.Models
{
    public class NaiveBayes : INaiveBayes
    {
        public NaiveBayes()
        {

        }
        public void Fit(float[][] X, int[] y)
        {
            throw new NotImplementedException();
        }

        public int[] Predict(float[][] X)
        {
            throw new NotImplementedException();
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
    }
}
