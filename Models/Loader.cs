using System.Globalization;
namespace NaiveBayes.Models
{
    public class Loader
    {
        private readonly string _path;
        private float[][] X { get; set; }
        private int[] y { get; set; }
        public Loader(string path) => _path = path;
        public void LoadData()
        {
            using (var reader = new StreamReader(_path))
            {
                reader.ReadLine();
                var rows = new List<List<float>>();
                var uniqueCategories = new HashSet<string>();
                var categories = new List<int>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(",");
                    var rowValues = new List<float>();
                    rowValues.Add(float.Parse(values[0], CultureInfo.InvariantCulture));
                    rowValues.Add(float.Parse(values[1], CultureInfo.InvariantCulture));
                    rowValues.Add(float.Parse(values[2], CultureInfo.InvariantCulture));
                    rowValues.Add(float.Parse(values[3], CultureInfo.InvariantCulture));
                    rows.Add(rowValues);
                    uniqueCategories.Add(values[4]);
                    categories.Add(GetIndexOfFirstFoundValue(values[4], uniqueCategories));

                }
                X = rows.Select(Enumerable.ToArray).ToArray();
                y = categories.ToArray();
            }
        }

        private int GetIndexOfFirstFoundValue(string searchedValue, HashSet<string> set)
        {
            var i = 0;
            foreach (var value in set)
            {
                if (value == searchedValue) return i;
                i++;
            }
            return 1000;
        }

        public float[][] GetX()
        {
            return X;
        }

        public int[] GetY()
        {
            return y;
        }
    }
}
