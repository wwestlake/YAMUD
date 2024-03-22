namespace LagDaemon.YAMUD.API

open MongoDB.Driver
open LagDaemon.YAMUD.DataModel.User

module Services =

    type UserService (client: MongoClient) =
        let databaseName = "yamud"
        let collectionName = "users"
        let db             = client.GetDatabase(databaseName)
        let collection = db.GetCollection<User>(collectionName)    

        // Create
        member _.createUserAsync user =
            let bsonDoc = serializeUnion user
            collection.InsertOneAsync bsonDoc

        member _.createManyUsersAsync users =
            collection.InsertManyAsync users
            
        // Read
        member _.readOnId id =
            collection.Find(fun x -> x.ID = id)

        member _.readOnEmail email =
            collection.Find(fun x -> x.EmailAddress = email)

        // Update
        member _.updateDisplayName id newDisplayName =
            let filter = Builders<User>.Filter.Eq((fun f -> f.ID), id)
            let update = Builders<User>.Update.Set((fun f -> f.DisplayName), newDisplayName)
            collection.UpdateOne(filter, update)

        member _.updateDisplayNameAsync id newDisplayName =
            let filter = Builders<User>.Filter.Eq((fun f -> f.ID), id)
            let update = Builders<User>.Update.Set((fun f -> f.DisplayName), newDisplayName)
            collection.UpdateOneAsync(filter, update) |> Async.AwaitTask

        member _.updateToken id token =
            let filter = Builders<User>.Filter.Eq((fun f -> f.ID), id)
            let update = Builders<User>.Update.Set((fun f -> f.ValidationToken), token)
            collection.UpdateOne(filter, update)

         member _.updateTokenAsync id token =
            let filter = Builders<User>.Filter.Eq((fun f -> f.ID), id)
            let update = Builders<User>.Update.Set((fun f -> f.ValidationToken), token)
            collection.UpdateOneAsync(filter, update) |> Async.AwaitTask
