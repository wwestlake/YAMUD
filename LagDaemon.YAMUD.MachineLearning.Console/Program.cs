using LagDaemon.YAMUD.MachineLearning.Console;
using LagDaemon_YAMUD_MachineLearning_Console;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;

//Load sample data
var sampleData = new MLModel1.ModelInput()
{
    Question = @"What is the difference between a built-in function and an imported function?",
};

//Load model and predict output
var result = MLModel1.Predict(sampleData);

Console.WriteLine(result.  .Prediction[0] == 1 ? "Positive sentiment" : "Negative sentiment")

