using LocalChatServerWeb.Data;
using LocalChatServerWeb.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbGenericRepository.Models;

namespace LocalChatServerWeb.Repositories
{
    public class Repository<T> where T : Document
    {
        private readonly IMongoCollection<T> collection;
        public Repository(IOptions<MongoDbConfig> ChatDatabaseSettings, string collectionName)
        {
            var mongoClient = new MongoClient(
                ChatDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ChatDatabaseSettings.Value.Name);

            collection = mongoDatabase.GetCollection<T>(
                collectionName);
        }

        public async Task<List<T>> GetAsync() =>
            await collection.Find(_ => true).ToListAsync();

        public async Task<T?> GetAsync(string id) =>
            await collection.Find(x => x.Id == new Guid(id)).FirstOrDefaultAsync();

        public async Task CreateAsync(T newBook) =>
            await collection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, T updatedBook) =>
            await collection.ReplaceOneAsync(x => x.Id == new Guid(id), updatedBook);

        public async Task RemoveAsync(string id) =>
            await collection.DeleteOneAsync(x => x.Id == new Guid(id));
    }
}
