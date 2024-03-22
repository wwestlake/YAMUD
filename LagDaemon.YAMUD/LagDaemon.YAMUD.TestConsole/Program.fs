
open MongoDB.Driver

let connectionString = "mongodb://localhost:27017"
let client = new MongoClient(connectionString)
let databaseName = "yamud"
let database = client.GetDatabase(databaseName)

printfn "Connected to MongoDB server: %s %s" connectionString databaseName

let databaseNames = client.ListDatabaseNames().ToList()

printfn "Available databases: %A" databaseNames


// Iterate through each database and list its collections
for dbName in databaseNames do
    let database = client.GetDatabase(dbName)
    let collectionNames =  database.ListCollectionNames().ToList()
    printfn "Collections in database '%s':" dbName
    collectionNames |> Seq.iter (printfn "- %s")

