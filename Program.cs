using NaiveBayesAssignment.Models;

Console.WriteLine("---IRIS---");
var iris = new Loader("./Data/iris.csv");
iris.LoadData();

var irisBayes = new NaiveBayesService();
irisBayes.Fit(iris.GetX(), iris.GetY());
var irisPreds = irisBayes.Predict(iris.GetX());

var irisAccuracy = irisBayes.AccuracyScore(irisPreds, iris.GetY());
Console.WriteLine("AccuracyScore: " + irisAccuracy);
var irisConfMatrix = irisBayes.ConfusionMatrix(irisPreds, iris.GetY());
Console.WriteLine("ConfusionMatrix:");
irisBayes.PrintConfusionMatrix(irisConfMatrix);

Console.WriteLine("");
Console.WriteLine("---BANKNOTE---");
var bankNotes = new Loader("./Data/banknote_authentication.csv");
bankNotes.LoadData();

var bankNotesBayes = new NaiveBayesService();
bankNotesBayes.Fit(bankNotes.GetX(), bankNotes.GetY());
var bankNotesPreds = bankNotesBayes.Predict(bankNotes.GetX());

var bankNotesAccuracy = bankNotesBayes.AccuracyScore(bankNotesPreds, bankNotes.GetY());
Console.WriteLine("AccuracyScore: " + bankNotesAccuracy);
Console.WriteLine("ConfusionMatrix:");
var bankConfMatrix = bankNotesBayes.ConfusionMatrix(bankNotesPreds, bankNotes.GetY());
bankNotesBayes.PrintConfusionMatrix(bankConfMatrix);
