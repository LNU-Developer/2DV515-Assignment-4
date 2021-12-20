using NaiveBayesAssignment.Models;

var iris = new Loader("./Data/iris.csv");
iris.LoadData();
var irisBayes = new NaiveBayesService();
irisBayes.Fit(iris.GetX(), iris.GetY());
irisBayes.Predict(iris.GetX());
var bankNotes = new Loader("./Data/iris.csv");
bankNotes.LoadData();
