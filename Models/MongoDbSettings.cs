namespace Expense_Tracker_API.Models
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ExpenseCollectionName { get; set; } = "ExpenseData";
        public string UserCollectionName { get; set; } = "Users";
        public string BankCollectionName { get; set; } = "Banks";
        public string IncomeCollectionName { get; set; } = "IncomeData";
    }

}
