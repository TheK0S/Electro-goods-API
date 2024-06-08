using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Entities;

namespace Electro_goods_API.Mapping.Interfaces
{
    public interface IMapper
    {
        public ProductDTO MapProductToProductDTO(Product product);
        public List<ProductDTO> MapProductToProductDTO(List<Product> products);
        public OrderResponseDTO MapOrderToOrderResponseDTO(Order order);
        public List<OrderResponseDTO> MapOrderToOrderResponseDTO(List<Order> orders);
        public OrderItemResponseDTO MapOrderItemToOrderItemResponseDTO(OrderItem orderItem);
        public List<OrderItemResponseDTO> MapOrderItemToOrderItemResponseDTO(List<OrderItem> orderItems);
        public CategoryDTO MapCategoryToCategoryDTO(Category category);
        public List<CategoryDTO> MapCategoryToCategoryDTO(List<Category> categories);
        public CountryDTO MapCountryToCountryDTO(Country country);
        public List<CountryDTO> MapCountryToCountryDTO(List<Country> countries);
        public ManufacturerDTO MapManufacturerToManufacturerDTO(Manufacturer manufacturer);
        public List<ManufacturerDTO> MapManufacturerToManufacturerDTO(List<Manufacturer> manufacturers);
        public ProductAttributeDTO MapProductAttributeToProductAttributeDTO(ProductAttribute attribute);
        public List<ProductAttributeDTO> MapProductAttributeToProductAttributeDTO(List<ProductAttribute> attributes);
        public UserDTO MapUserToUserDTO(User? user);
    }
}
