using Core.Entities;
using Core.Interfaces;


namespace Core.Services
{
    public class ProductService : IProductService
    {
        private readonly     IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Product> CreateProduct(Product product)
        {
            return await _productRepository.AddAsync(product);
        }

        public async Task DeleteProduct(int id)
        {
             await (_productRepository.DeleteAsync(id));
        }

        public async Task<IReadOnlyList<Product>> GetAllProducts()
        {
            return await (_productRepository.GetAllAsync());
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task UpdateProduct(Product product)
        {
            await _productRepository.UpdateAsync(product);
        }
    }
}
