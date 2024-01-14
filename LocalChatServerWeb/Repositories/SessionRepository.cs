using LocalChatServerWeb.Models;
using LocalChatServerWeb.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LocalChatServerWeb.Services
{
    public class SessionRepository 
    {
        private readonly IMongoCollection<Session> sessionsCollection;
        public SessionRepository(IOptions<MongoDbConfig> ChatDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                ChatDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ChatDatabaseSettings.Value.Name);

            sessionsCollection = mongoDatabase.GetCollection<Session>(
                "Sessions");
        }

        public async Task<List<Session>> GetSessionsAsync() =>
            await sessionsCollection.Find(_ => true).ToListAsync();

        public async Task<Session?> GetSessionAsync(string id) =>
            await sessionsCollection.Find(x => x.Id == new Guid(id)).FirstOrDefaultAsync();

        public async Task CreateSessionAsync(Session newBook) =>
            await sessionsCollection.InsertOneAsync(newBook);

        public async Task UpdateSessionAsync(string id, Session updatedBook) =>
            await sessionsCollection.ReplaceOneAsync(x => x.Id == new Guid(id), updatedBook);

        public async Task RemoveSessionAsync(string id) =>
            await sessionsCollection.DeleteOneAsync(x => x.Id == new Guid(id));
    }
}
