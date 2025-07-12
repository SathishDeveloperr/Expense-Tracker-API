using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Expense_Tracker_API.Models
{
    public class Bank
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string BankName { get; set; }

        public string? AccountNumber { get; set; } // optional

        public string? UserId { get; set; } // for multi-user support
    }
}
