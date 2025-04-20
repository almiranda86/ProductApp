using System.Text.Json.Serialization;

namespace ProductApp.Domain.Commands
{
    public record RegisterProductRequest
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("categories")]
        public List<string> Categories { get; set; }

        [JsonPropertyName("price")]
        public float Price { get; set; }
    }
}
