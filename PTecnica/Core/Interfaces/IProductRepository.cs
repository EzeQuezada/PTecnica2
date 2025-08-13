

using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {   
        Task<Product>GetByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetAllAsync();
        Task<Product> AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);

    }
}
