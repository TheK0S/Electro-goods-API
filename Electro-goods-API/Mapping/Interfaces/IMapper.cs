using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Entities;

namespace Electro_goods_API.Mapping.Interfaces
{
    public interface IMapper
    {
        public ProductDTO MapProductToProductDTO(Product product, string language);
        public List<ProductDTO> MapProductToProductDTO(List<Product> products, string language);
        public OrderDTO MapOrderToOrderDTO(Order order, string language);
        public List<OrderDTO> MapOrderToOrderDTO(List<Order> orders, string language);
        public OrderItemDTO MapOrderItemToOrderItemDTO(OrderItem orderItem, string language);
        public List<OrderItemDTO> MapOrderItemToOrderItemDTO(List<OrderItem> orderItems, string language);
        public CategoryDTO MapCategoryToCategoryDTO(Category category, string language);
        public List<CategoryDTO> MapCategoryToCategoryDTO(List<Category> categories, string language);
        public CountryDTO MapCountryToCountryDTO(Country country, string language);
        public List<CountryDTO> MapCountryToCountryDTO(List<Country> countries, string language);
        public ManufacturerDTO MapManufacturerToManufacturerDTO(Manufacturer manufacturer, string language);
        public List<ManufacturerDTO> MapManufacturerToManufacturerDTO(List<Manufacturer> manufacturers, string language);
        public ProductAttributeDTO MapProductAttributeToProductAttributeDTO(ProductAttribute attribute, string language);
        public List<ProductAttributeDTO> MapProductAttributeToProductAttributeDTO(List<ProductAttribute> attributes, string language);
        public string GetLanguageFromHeaders(IHeaderDictionary headers);
    }
}
