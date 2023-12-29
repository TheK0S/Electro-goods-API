using Electro_goods_API.Models.DTO;

namespace Electro_goods_API.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<CategoryDto>> GetAllCategories();
        Task<CategoryDto> GetCategoryById(int id);
        Task<CategoryDto> CreateCategory(CategoryDto category);
        Task UpdateCategory(int id, CategoryDto category);
        Task DeleteCategory(int id);
    }
}
