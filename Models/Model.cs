namespace NaiveBayesAssignment.Models
{
    public class Model
    {
        public int Key { get; set; }
        public Calculation[] Calculation { get; set; }
        public int Count { get; set; }
    }

    public class Calculation
    {
        public int Key { get; set; }
        public float Sum { get; set; }
        public float Mean { get; set; }
        public float StandardDeviation { get; set; }
    }
}