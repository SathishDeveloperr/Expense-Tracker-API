using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Expense_Tracker_API.Models
{
    public class IncomeData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = null!;

        public decimal Amount { get; set; }

        public string Bank { get; set; } = null!;

        public string? Description { get; set; }

        public int Month { get; set; } 

        public int Year { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
