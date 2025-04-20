using ProductApp.Domain.Commands;
using ProductApp.Domain.Core.ResultPattern;

namespace ProductApp.Domain.Behavior.Service
{
    public interface IRegisterProduct
    {
        Task<Result<RegisterProductResponse>> RegisterProductAsync(RegisterProductRequest registerProductRequest, CancellationToken cancellationToken = default);
    }
}
