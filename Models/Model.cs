namespace NaiveBayesAssignment.Models
{
    public class Model
    {
        public int Key { get; set; }
        public List<float> Values { get; set; }
        public List<Calculation> Calculation { get; set; } = new List<Calculation>();
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