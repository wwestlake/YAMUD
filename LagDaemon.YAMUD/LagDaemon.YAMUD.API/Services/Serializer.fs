namespace LagDaemon.YAMUD.API
open Newtonsoft.Json
open MongoDB.Bson

module Serializer =

    let serializeUnion (union: 'a) =
        let json = JsonConvert.SerializeObject(union)
        BsonDocument(JsonConvert.DeserializeObject<BsonDocument>(json))

    let deserializeUnion (doc : BsonDocument) : 'a =
        let json = doc.ToJson()
        JsonConvert.DeserializeObject<'a>(json)


