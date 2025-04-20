using System.Text.Json.Serialization;

namespace ProductApp.Domain.Commands
{
    public record RegisterProductResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public RegisterProductResponse()
        {
            Success = false;
            Message = "Product not created!";
        }

    }
}
