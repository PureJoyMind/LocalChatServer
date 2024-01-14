using LocalChatServerWeb.Data;
using LocalChatServerWeb.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LocalChatServerWeb.Repositories
{
    public class MessageRepository
    {
        private readonly IMongoCollection<Message> messagesCollection;
        public MessageRepository(IOptions<MongoDbConfig> ChatDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                ChatDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ChatDatabaseSettings.Value.Name);

            messagesCollection = mongoDatabase.GetCollection<Message>(
                "Messages");
        }

        public async Task<List<Message>> GetAsync() =>
            await messagesCollection.Find(_ => true).ToListAsync();

        public async Task<Message?> GetAsync(string id) =>
            await messagesCollection.Find(x => x.Id == new Guid(id)).FirstOrDefaultAsync();

        public async Task CreateAsync(Message newBook) =>
            await messagesCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, Message updatedBook) =>
            await messagesCollection.ReplaceOneAsync(x => x.Id == new Guid(id), updatedBook);

        public async Task RemoveAsync(string id) =>
            await messagesCollection.DeleteOneAsync(x => x.Id == new Guid(id));
    }
}
