module LagDaemon.YAMUD.Parsers.Tests

open NUnit.Framework
open LagDaemon.YAMUD.Parsers.UserCommandParser
open LagDaemon.YAMUD.Model.Map
open MovementHandler


// Define your test module
[<TestFixture>]
module MovementCommandTests =

    // Define your test cases
    [<TestCase("go west", "Success: Movement West")>]
    [<TestCase("go north", "Success: Movement North")>]
    [<TestCase("go east", "Success: Movement East")>]
    [<TestCase("go up", "Success: Movement Up")>]
    [<TestCase("go down", "Success: Movement Down")>]
    [<TestCase("go south", "Success: Movement South")>]
    [<TestCase("pig head", "Failure: Parse error")>]
    let ``ParseCommand should return expected results`` (cmd: string) (expectedResult: string) =
        // Define your success and failure functions (replace with actual logic)
        let onSuccess (cmd: Command) = sprintf "Success: %A" cmd
        let onFail (errMsg: string) = sprintf "Failure: %s" errMsg
        
        // Call the function under test
        let actualResult = ParseCommand cmd onSuccess onFail

        // Assert the result
        Assert.AreEqual(expectedResult, actualResult)


    [<TestCase("go west", -1, 0, 0)>]
    [<TestCase("go north", 0, 1, 0)>]
    [<TestCase("go east", 1, 0, 0)>]
    [<TestCase("go up", 0, 0, 1)>]
    [<TestCase("go down", 0,0,-1)>]
    [<TestCase("go south", 0, -1, 0)>]
    let ``ExecuteCommand should modify address by movement commands`` (cmd: string) x y z =
        let address = new RoomAddress();

        // Call the function under test
        let actualResult = ExecuteCommand address cmd

        // Assert the result
        Assert.AreEqual(x, address.X)
        Assert.AreEqual(y, address.Y)
        Assert.AreEqual(z, address.Level)

    [<Test>]
    let ``ExecuteCommand should not modify address by non-movement commands`` () =
        let address = new RoomAddress();

        // Call the function under test
        let actualResult = ExecuteCommand address "pig head"

        // Assert the result
        Assert.AreEqual(0, address.X)
        Assert.AreEqual(0, address.Y)
        Assert.AreEqual(0, address.Level)