using Expense_Tracker_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Expense_Tracker_API.Services
{
    public class BankService
    {
        private readonly IMongoCollection<Bank> _bankCollection;

        public BankService(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _bankCollection = database.GetCollection<Bank>(settings.Value.BankCollectionName);
        }

        public async Task<List<Bank>> GetAsync() => await _bankCollection.Find(_ => true).ToListAsync();
        public async Task<Bank?> GetAsync(string id) => await _bankCollection.Find(b => b.Id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(Bank bank) => await _bankCollection.InsertOneAsync(bank);
        public async Task UpdateAsync(string id, Bank bank) => await _bankCollection.ReplaceOneAsync(b => b.Id == id, bank);
        public async Task DeleteAsync(string id) => await _bankCollection.DeleteOneAsync(b => b.Id == id);

    }
}
