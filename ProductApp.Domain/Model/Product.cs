using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ProductApp.Domain.Model
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("stock")]
        [DataMember]
        [BsonElement("stock")]
        public string Stock { get; set; }

        [JsonPropertyName("description")]
        [DataMember]
        [BsonElement("description")]
        public string Description { get; set; }

        [JsonPropertyName("categories")]
        [DataMember]
        [BsonElement("categories")]
        public List<string> Categories { get; set; }

        [JsonPropertyName("price")]
        [DataMember]
        [BsonElement("price")]
        public string Price { get; set; }
    }
}
