using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace PromocodeService.Models
{
    /// <summary>
    /// Object to represent a promocode
    /// </summary>
    public class Promocode
    {
        /// <summary>
        /// Represents the promocode db id
        /// </summary>
        [BsonId]
        public long Id { get; set; }

        /// <summary>
        /// Represents the promocode owner
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Represents the promocode unique text
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Represents is the promocode active
        /// </summary>
        public bool IsActive { get; set; }
    }
}
