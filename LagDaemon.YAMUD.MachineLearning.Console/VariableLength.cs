using Microsoft.ML.Data;
using System;
using System.Linq;
using static Tensorflow.Keras.Engine.InputSpec;

namespace LagDaemon.YAMUD.MachineLearning.Console
{
    /// <summary>
    /// Class to hold the variable length feature vector. Used to define the
    /// column names used as input to the custom mapping action.
    /// </summary>
    public class VariableLength
    {
        /// <summary>
        /// This is a variable length vector designated by VectorType attribute.
        /// Variable length vectors are produced by applying operations such as 'TokenizeWords' on strings
        /// resulting in vectors of tokens of variable lengths.
        /// </summary>
        [VectorType]
        public int[]? VariableLengthFeatures { get; set; }
    }
}
