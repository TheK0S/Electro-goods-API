using Electro_goods_API.Models.Entities;

namespace Electro_goods_API.Repositories.Interfaces
{
    public interface IProductAttributeRepository
    {
        Task<List<ProductAttribute>> GetProductAttributesByProductId(int productId);
        Task<ProductAttribute> GetProductAttributeById(int id);
        Task<ProductAttribute> CreateProductAttribute(ProductAttribute productAttribute);
        Task UpdateProductAttribute(int id, ProductAttribute productAttribute);
        Task DeleteProductAttribute(int id);
    }
}
