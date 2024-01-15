using LocalChatServerWeb.Models;
using LocalChatServerWeb.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LocalChatServerWeb.Repositories
{
    public class SessionRepository 
    {
        private readonly IMongoCollection<Session> sessionsCollection;
        public SessionRepository(IOptions<MongoDbConfig> mongoDbConfig)
        {
            var mongoClient = new MongoClient(
                mongoDbConfig.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mongoDbConfig.Value.Name);

            sessionsCollection = mongoDatabase.GetCollection<Session>(
                "Sessions");
        }

        public async Task<List<Session>> GetAsync() =>
            await sessionsCollection.Find(_ => true).ToListAsync();

        public async Task<Session?> GetAsync(string id) =>
            await sessionsCollection.Find(x => x.Id == new Guid(id)).FirstOrDefaultAsync();

        public async Task<Session?> GetByNameAsync(string name) =>
            await sessionsCollection.Find(x => x.Name == name).FirstOrDefaultAsync();

        public async Task CreateAsync(Session newBook) =>
            await sessionsCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, Session updatedBook) =>
            await sessionsCollection.ReplaceOneAsync(x => x.Id == new Guid(id), updatedBook);

        public async Task RemoveAsync(string id) =>
            await sessionsCollection.DeleteOneAsync(x => x.Id == new Guid(id));
    }
}
