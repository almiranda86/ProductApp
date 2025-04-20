using ProductApp.Domain.Core.ResultPattern;
using ProductApp.Domain.Queries;

namespace ProductApp.Domain.Behavior.Service
{
    public interface IGetProduct
    {
        Task<Result<GetProductResult>> GetProductAsync(string productId, CancellationToken cancellationToken);
    }
}
