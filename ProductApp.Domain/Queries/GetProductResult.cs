using System.Text.Json.Serialization;

namespace ProductApp.Domain.Queries
{
    public record GetProductResult
    {
        [JsonPropertyName("productId")]
        public Guid Id { get; set; }

        [JsonPropertyName("stock")]
        public int Stock { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("categories")]
        public List<string> Categories { get; set; }

        [JsonPropertyName("price")]
        public float Price { get; set; }
    }
}
