using Azure.Core;
using Electro_goods_API.Mapping.Interfaces;
using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Entities;

namespace Electro_goods_API.Mapping
{
    public class Mapper : IMapper
    {
        private readonly string language;

        public Mapper(IHttpContextAccessor httpContextAccessor)
        {
            language = httpContextAccessor.HttpContext?.Items["Language"]?.ToString() ?? "ru";
        }
        public ProductDTO MapProductToProductDTO(Product? product)
        {
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
                Category = MapCategoryToCategoryDTO(product.Category),
                Country = MapCountryToCountryDTO(product.Country),
                Manufacturer = MapManufacturerToManufacturerDTO(product.Manufacturer),
                ProductAttributes = MapProductAttributeToProductAttributeDTO(product.ProductAttributes),
            };
        }

        public List<ProductDTO> MapProductToProductDTO(List<Product>? products)
        {
            if (products is null || products.Count == 0)
                return new List<ProductDTO>();

            var productsDTO = new List<ProductDTO>();
            foreach (var product in products)
                productsDTO.Add(MapProductToProductDTO(product));

            return productsDTO;
        }

        public OrderResponseDTO MapOrderToOrderResponseDTO(Order? order)
        {
            if (order is null)
                return new OrderResponseDTO();

            return new OrderResponseDTO
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Price = order.Price,
                ShippingAddress = order.ShippingAddress,
                User = MapUserToUserDTO(order.User),
                OrderStatus = language == "ru" ? order?.OrderStatus?.StatusName : order?.OrderStatus?.StatusNameUK,
                OrderItems = MapOrderItemToOrderItemResponseDTO(order?.OrderItems),
            };
        }

        public List<OrderResponseDTO> MapOrderToOrderResponseDTO(List<Order>? orders)
        {
            if (orders is null || orders.Count == 0)
                return new List<OrderResponseDTO>();

            var ordersDTO = new List<OrderResponseDTO>();
            foreach (var order in orders)
                ordersDTO.Add(MapOrderToOrderResponseDTO(order));

            return ordersDTO;
        }

        public OrderItemResponseDTO MapOrderItemToOrderItemResponseDTO(OrderItem? orderItem)
        {

            if (orderItem is null)
                return new OrderItemResponseDTO();

            return new OrderItemResponseDTO
            {
                Id = orderItem.Id,
                ProductName = language == "ru" ? orderItem.ProductName : orderItem.ProductNameUK,
                ProductPrice = orderItem.ProductPrice,
                Quantity = orderItem.Quantity,
                Product = MapProductToProductDTO(orderItem.Product),
            };
        }

        public List<OrderItemResponseDTO> MapOrderItemToOrderItemResponseDTO(List<OrderItem>? orderItems)
        {
            if (orderItems is null || orderItems.Count == 0)
                return new List<OrderItemResponseDTO>();

            var orderItemsDTO = new List<OrderItemResponseDTO>();
            foreach (var orderItem in orderItems)
                orderItemsDTO.Add(MapOrderItemToOrderItemResponseDTO(orderItem));

            return orderItemsDTO;
        }

        public CategoryDTO MapCategoryToCategoryDTO(Category? category)
        {
            if (category is null)
                return new CategoryDTO();

            return new CategoryDTO { 
                Id = category.Id, 
                Name = language == "ru" ? category.Name : category.NameUK, 
            };
        }

        public List<CategoryDTO> MapCategoryToCategoryDTO(List<Category>? categories)
        {
            if (categories is null || categories.Count == 0)
                return new List<CategoryDTO>();

            var categoriesDTO = new List<CategoryDTO>();
            foreach (var category in categories)
                categoriesDTO.Add(MapCategoryToCategoryDTO(category));

            return categoriesDTO;
        }

        public CountryDTO MapCountryToCountryDTO(Country? country)
        {
            if (country is null)
                return new CountryDTO();

            return new CountryDTO
            {
                Id = country.Id,
                Name = language == "ru" ? country.Name : country.NameUK,
            };
        }

        public List<CountryDTO> MapCountryToCountryDTO(List<Country>? countries)
        {
            if (countries is null || countries.Count == 0)
                return new List<CountryDTO>();

            var countriesDTO = new List<CountryDTO>();
            foreach (var country in countries)
                countriesDTO.Add(MapCountryToCountryDTO(country));

            return countriesDTO;
        }

        public ManufacturerDTO MapManufacturerToManufacturerDTO(Manufacturer? manufacturer)
        {
            if (manufacturer is null)
                return new ManufacturerDTO();

            return new ManufacturerDTO
            {
                Id = manufacturer.Id,
                Name = language == "ru" ? manufacturer.Name : manufacturer.NameUK,
            };
        }

        public List<ManufacturerDTO> MapManufacturerToManufacturerDTO(List<Manufacturer>? manufacturers)
        {
            if (manufacturers is null || manufacturers.Count == 0)
                return new List<ManufacturerDTO>();

            var manufacturersDTO = new List<ManufacturerDTO>();
            foreach (var manufacturer in manufacturers)
                manufacturersDTO.Add(MapManufacturerToManufacturerDTO(manufacturer));

            return manufacturersDTO;
        }

        public ProductAttributeDTO MapProductAttributeToProductAttributeDTO(ProductAttribute? attribute)
        {
            if (attribute is null)
                return new ProductAttributeDTO();

            return new ProductAttributeDTO
            {
                Id = attribute.AttributeId,
                Name = language == "ru" ? attribute.AttributeName : attribute.AttributeNameUK,
                Value = language == "ru" ? attribute.AttributeValue : attribute.AttributeValueUK,
            };
        }

        public List<ProductAttributeDTO> MapProductAttributeToProductAttributeDTO(List<ProductAttribute>? attributes)
        {
            if (attributes is null || attributes.Count == 0)
                return new List<ProductAttributeDTO>();

            var attributesDTO = new List<ProductAttributeDTO>();
            foreach (var attribute in attributes)
                attributesDTO.Add(MapProductAttributeToProductAttributeDTO(attribute));

            return attributesDTO;
        }

        public UserDTO MapUserToUserDTO(User? user)
        {
            if (user is null)
                return new UserDTO();

            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Patronomic = user.Patronomic,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Birthdate = user.Birthdate,
                CreationDate = user.CreationDate,
                IsActive = user.IsActive,
                Orders = MapOrderToOrderResponseDTO(user.Orders),
            };
        }
    }
}
