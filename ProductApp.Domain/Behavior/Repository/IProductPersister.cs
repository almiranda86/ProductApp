using ProductApp.Domain.Model;

namespace ProductApp.Domain.Behavior.Repository
{
    public interface IProductPersister
    {
        Task<bool> CreateProduct(Product product);
    }
}
