using Microsoft.ML.Data;
using System;
using System.Linq;

namespace LagDaemon.YAMUD.MachineLearning.Console
{
    /// <summary>
    /// Class to contain the output values from the transformation.
    /// </summary>
    public class MovieReviewSentimentPrediction
    {
        [VectorType(2)]
        public float[]? Prediction { get; set; }
    }
}
