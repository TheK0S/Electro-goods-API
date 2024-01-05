﻿using Electro_goods_API.Models.Entities;
using Electro_goods_API.Models.Filters;

namespace Electro_goods_API.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<List<Product>> GetProducts(ProductFilter filter);
        Task<Product> GetProductById(int id);
        Task<Product> CreateProduct(Product product);
        Task UpdateProduct(int id, Product product);
        Task DeleteProduct(int id);
    }
}
