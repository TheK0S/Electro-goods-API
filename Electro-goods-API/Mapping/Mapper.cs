using Azure.Core;
using Electro_goods_API.Mapping.Interfaces;
using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Entities;

namespace Electro_goods_API.Mapping
{
    public class Mapper : IMapper
    {
        public ProductDTO MapProductToProductDTO(Product product, string language)
        {
            if (string.IsNullOrWhiteSpace(language))
                language = "ru";
            if (product is null)
                return new ProductDTO();

            return new ProductDTO
            {
                Id = product.Id,
                Name = language == "ru" ? product.Name : product.NameUK,
                Description = language == "ru" ? product.Description : product.DescriptionUK,
                Price = product.Price,
                Discount = product.Discount,
                ImgPath = product.ImgPath,
                Category = MapCategoryToCategoryDTO(product.Category, language),
                Country = MapCountryToCountryDTO(product.Country, language),
                Manufacturer = MapManufacturerToManufacturerDTO(product.Manufacturer, language),
                ProductAttributes = MapProductAttributeToProductAttributeDTO(product.ProductAttributes, language),
            };
        }

        public List<ProductDTO> MapProductToProductDTO(List<Product> products, string language)
        {
            if (string.IsNullOrWhiteSpace(language))
                language = "ru";
            if (products is null || products.Count == 0)
                return new List<ProductDTO>();

            var productsDTO = new List<ProductDTO>();
            foreach (var product in products)
                productsDTO.Add(MapProductToProductDTO(product, language));

            return productsDTO;
        }

        public OrderDTO MapOrderToOrderDTO(Order order, string language)
        {
            if (string.IsNullOrWhiteSpace(language))
                language = "ru";
            if (order is null)
                return new OrderDTO();

            return new OrderDTO
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Price = order.Price,
                ShippingAddress = order.ShippingAddress,
                User = order.User,
                OrderStatus = language == "ru" ? order?.OrderStatus?.StatusName : order?.OrderStatus?.StatusNameUK,
                OrderItems = MapOrderItemToOrderItemDTO(order.OrderItems, language),
            };
        }

        public List<OrderDTO> MapOrderToOrderDTO(List<Order> orders, string language)
        {
            if (string.IsNullOrWhiteSpace(language))
                language = "ru";
            if (orders is null || orders.Count == 0)
                return new List<OrderDTO>();

            var ordersDTO = new List<OrderDTO>();
            foreach (var order in orders)
                ordersDTO.Add(MapOrderToOrderDTO(order, language));

            return ordersDTO;
        }

        public OrderItemDTO MapOrderItemToOrderItemDTO(OrderItem orderItem, string language)
        {
            if (string.IsNullOrWhiteSpace(language))
                language = "ru";
            if (orderItem is null)
                return new OrderItemDTO();

            return new OrderItemDTO
            {
                Id = orderItem.Id,
                ProductName = language == "ru" ? orderItem.ProductName : orderItem.ProductNameUK,
                ProductPrice = orderItem.ProductPrice,
                Quantity = orderItem.Quantity,
                Product = MapProductToProductDTO(orderItem.Product, language),
            };
        }

        public List<OrderItemDTO> MapOrderItemToOrderItemDTO(List<OrderItem> orderItems, string language)
        {
            if (string.IsNullOrWhiteSpace(language))
                language = "ru";
            if (orderItems is null || orderItems.Count == 0)
                return new List<OrderItemDTO>();

            var orderItemsDTO = new List<OrderItemDTO>();
            foreach (var orderItem in orderItems)
                orderItemsDTO.Add(MapOrderItemToOrderItemDTO(orderItem, language));

            return orderItemsDTO;
        }

        public CategoryDTO MapCategoryToCategoryDTO(Category category, string language)
        {
            if (string.IsNullOrWhiteSpace(language))
                language = "ru";
            if (category is null)
                return new CategoryDTO();

            return new CategoryDTO { 
                Id = category.Id, 
                Name = language == "ru" ? category.Name : category.NameUK, 
            };
        }

        public List<CategoryDTO> MapCategoryToCategoryDTO(List<Category> categories, string language)
        {
            if (string.IsNullOrWhiteSpace(language)) 
                language = "ru";
            if (categories is null || categories.Count == 0)
                return new List<CategoryDTO>();

            var categoriesDTO = new List<CategoryDTO>();
            foreach (var category in categories)
                categoriesDTO.Add(MapCategoryToCategoryDTO(category, language));

            return categoriesDTO;
        }

        public CountryDTO MapCountryToCountryDTO(Country country, string language)
        {
            if (string.IsNullOrWhiteSpace(language))
                language = "ru";
            if (country is null)
                return new CountryDTO();

            return new CountryDTO
            {
                Id = country.Id,
                Name = language == "ru" ? country.Name : country.NameUK,
            };
        }

        public List<CountryDTO> MapCountryToCountryDTO(List<Country> countries, string language)
        {
            if (string.IsNullOrWhiteSpace(language))
                language = "ru";
            if (countries is null || countries.Count == 0)
                return new List<CountryDTO>();

            var countriesDTO = new List<CountryDTO>();
            foreach (var country in countries)
                countriesDTO.Add(MapCountryToCountryDTO(country, language));

            return countriesDTO;
        }

        public ManufacturerDTO MapManufacturerToManufacturerDTO(Manufacturer manufacturer, string language)
        {
            if (string.IsNullOrWhiteSpace(language))
                language = "ru";
            if (manufacturer is null)
                return new ManufacturerDTO();

            return new ManufacturerDTO
            {
                Id = manufacturer.Id,
                Name = language == "ru" ? manufacturer.Name : manufacturer.NameUK,
            };
        }

        public List<ManufacturerDTO> MapManufacturerToManufacturerDTO(List<Manufacturer> manufacturers, string language)
        {
            if (string.IsNullOrWhiteSpace(language))
                language = "ru";
            if (manufacturers is null || manufacturers.Count == 0)
                return new List<ManufacturerDTO>();

            var manufacturersDTO = new List<ManufacturerDTO>();
            foreach (var manufacturer in manufacturers)
                manufacturersDTO.Add(MapManufacturerToManufacturerDTO(manufacturer, language));

            return manufacturersDTO;
        }

        public ProductAttributeDTO MapProductAttributeToProductAttributeDTO(ProductAttribute attribute, string language)
        {
            if (string.IsNullOrWhiteSpace(language))
                language = "ru";
            if (attribute is null)
                return new ProductAttributeDTO();

            return new ProductAttributeDTO
            {
                Id = attribute.AttributeId,
                Name = language == "ru" ? attribute.AttributeName : attribute.AttributeNameUK,
                Value = language == "ru" ? attribute.AttributeValue : attribute.AttributeValueUK,
            };
        }

        public List<ProductAttributeDTO> MapProductAttributeToProductAttributeDTO(List<ProductAttribute> attributes, string language)
        {
            if (string.IsNullOrWhiteSpace(language))
                language = "ru";
            if (attributes is null || attributes.Count == 0)
                return new List<ProductAttributeDTO>();

            var attributesDTO = new List<ProductAttributeDTO>();
            foreach (var attribute in attributes)
                attributesDTO.Add(MapProductAttributeToProductAttributeDTO(attribute, language));

            return attributesDTO;
        }

        public string GetLanguageFromHeaders(IHeaderDictionary headers)
        {
            if (headers is null)
                return "ru";

            headers.TryGetValue("Api-Language", out var lang);

            if(string.IsNullOrWhiteSpace(lang))
                return "ru";

            return lang;
        }
    }
}
