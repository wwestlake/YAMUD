using LagDaemon.YAMUD.Model.Map;
using MongoDB.Driver;

namespace LagDaemon.YAMUD.API.Services
{
    public class MongoRoomService : IMongoRoomService
    {
        private readonly IMongoCollection<Room> _roomCollection;

        public MongoRoomService(IMongoClient mongoClient, string databaseName, string collectionName)
        {
            var database = mongoClient.GetDatabase(databaseName);
            _roomCollection = database.GetCollection<Room>(collectionName);
        }

        public async Task<IEnumerable<Room>> GetRoomsByLevel(int level)
        {
            var filter = Builders<Room>.Filter.Eq(r => r.Z, level);
            return await _roomCollection.Find(filter).ToListAsync();
        }

        public async Task<Room> GetRoomById(Guid id)
        {
            var filter = Builders<Room>.Filter.Eq(r => r.Id, id);
            return await _roomCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Room> CreateRoom(Room room)
        {
            room.Id = Guid.NewGuid();
            await _roomCollection.InsertOneAsync(room);
            return room;
        }

        public async Task UpdateRoom(Room room)
        {
            var filter = Builders<Room>.Filter.Eq(r => r.Id, room.Id);
            await _roomCollection.ReplaceOneAsync(filter, room);
        }

        public async Task DeleteRoom(Guid id)
        {
            var filter = Builders<Room>.Filter.Eq(r => r.Id, id);
            await _roomCollection.DeleteOneAsync(filter);
        }
    }
}
