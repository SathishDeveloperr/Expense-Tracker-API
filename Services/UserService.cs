using Expense_Tracker_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Expense_Tracker_API.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserService(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _userCollection = database.GetCollection<User>(settings.Value.UserCollectionName);
        }

        public async Task<List<User>> GetAsync() => await _userCollection.Find(_ => true).ToListAsync();
        public async Task<User?> GetAsync(string id) => await _userCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(User user) => await _userCollection.InsertOneAsync(user);
        public async Task UpdateAsync(string id, User user) => await _userCollection.ReplaceOneAsync(u => u.Id == id, user);
        public async Task DeleteAsync(string id) => await _userCollection.DeleteOneAsync(u => u.Id == id);

    }
}
