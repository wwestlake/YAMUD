
open MongoDB.Driver
open LagDaemon.YAMUD.API.Services
open LagDaemon.YAMUD.DataModel.User
open LagDaemon.YAMUD.API.User

let connectionString = "mongodb://localhost:27017"
let client = new MongoClient(connectionString)

let userService = new UserService(client)
let newUser = createUser "Bill Westlake" "wwestlake@lagdaemon.com" "Something about myself"
match newUser with
| Some user -> 
        userService.createUserAsync user |> Async.AwaitTask |> ignore
        printfn "User created: %s %s" (user.ID.ToString()) (user.DisplayName.ToString())
| None -> printfn "Error creating new user" |> ignore





