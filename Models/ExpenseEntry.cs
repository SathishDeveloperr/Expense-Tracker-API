using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Expense_Tracker_API.Models
{
    public class ExpenseEntry
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = null!;

        public decimal Amount { get; set; }

        public string Type { get; set; } = null!;

        public string Category { get; set; } = null!;

        public DateTime Date { get; set; }

        public string Bank { get; set; } = null!;

        public string? Description { get; set; }
    }
}
