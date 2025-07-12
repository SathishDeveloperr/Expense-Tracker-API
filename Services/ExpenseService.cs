using Expense_Tracker_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Expense_Tracker_API.Services
{
    public class ExpenseService
    {
        private readonly IMongoCollection<ExpenseEntry> _collection;

        public ExpenseService(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<ExpenseEntry>(settings.Value.ExpenseCollectionName);
        }

        public async Task<List<ExpenseEntry>> GetAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task<ExpenseEntry?> GetAsync(string id) =>
            await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ExpenseEntry entry) =>
            await _collection.InsertOneAsync(entry);

        public async Task UpdateAsync(string id, ExpenseEntry entry) =>
            await _collection.ReplaceOneAsync(e => e.Id == id, entry);

        public async Task DeleteAsync(string id) =>
            await _collection.DeleteOneAsync(e => e.Id == id);
    }
}
