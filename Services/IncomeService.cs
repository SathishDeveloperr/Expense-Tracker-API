using Expense_Tracker_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Expense_Tracker_API.Services
{
    public class IncomeService
    {
        private readonly IMongoCollection<IncomeData> _incomeCollection;

        public IncomeService(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _incomeCollection = database.GetCollection<IncomeData>(settings.Value.IncomeCollectionName);
        }

        public async Task<List<IncomeData>> GetAsync() =>
            await _incomeCollection.Find(_ => true).ToListAsync();

        public async Task<IncomeData?> GetAsync(string id) =>
            await _incomeCollection.Find(i => i.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(IncomeData income) =>
            await _incomeCollection.InsertOneAsync(income);

        public async Task UpdateAsync(string id, IncomeData income) =>
            await _incomeCollection.ReplaceOneAsync(i => i.Id == id, income);

        public async Task DeleteAsync(string id) =>
            await _incomeCollection.DeleteOneAsync(i => i.Id == id);
    }
}
