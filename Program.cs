using NaiveBayes.Models;

var loader = new Loader("./Data/iris.csv");
loader.LoadData();

foreach (var item in loader.GetX())
{
    Console.WriteLine(item);
    foreach (var item2 in item)
    {
        Console.WriteLine(item2);
    }
}

foreach (var item in loader.GetY())
{
    Console.WriteLine(item);
}
