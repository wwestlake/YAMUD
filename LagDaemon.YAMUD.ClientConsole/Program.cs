using IronPython.Hosting;
using Microsoft.Scripting.Hosting;


var engine = Python.CreateEngine();

// Execute Python code
var scope = engine.CreateScope();
engine.Execute("x = 5", scope);
engine.Execute("result = x**2", scope);

// Retrieve the result from Python
dynamic result = scope.GetVariable("result");

// Print the result
Console.WriteLine($"The square of 5 is {result}");
