namespace NaiveBayes.Models
{
    interface INaiveBayes
    {
        void Fit(float[][] X, int[] y);
        int[] Predict(float[][] X);
        float AccuracyScore(int[] preds, int[] y);
        int[][] ConfusionMatrix(int[] preds, int[] y);
        int[] CrossvalPredict(float[][] X, int[] y, int folds);
    }
}
