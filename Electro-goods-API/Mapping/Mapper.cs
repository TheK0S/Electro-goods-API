using Azure.Core;
using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Entities;

namespace Electro_goods_API.Mapping
{
    public static class Mapper
    {
        public static ProductDTO MapProductToProductDTO(Product product, string language)
        {
            if(language is null) language = "ru";

            return new ProductDTO
            {
                Id = product.Id,
                Name = language == "ru" ? product.Name : product.NameUK,
                Description = language == "ru" ? product.Description : product.DescriptionUK,
                Price = product.Price,
                CategoryId = product.CategoryId,
                CountryId = product.CountryId,
                ManufacturerId = product.ManufacturerId,
                ProductAttributes = product.ProductAttributes,
            };
        }

        public static List<ProductDTO> MapProductToProductDTO(List<Product> products, string language)
        {
            if (language is null) language = "ru";
            var productsDTO = new List<ProductDTO>();

            foreach (var product in products)
                productsDTO.Add(MapProductToProductDTO(product, language));

            return productsDTO;
        }

        public static OrderDTO MapOrderToOrderDTO(Order order, string language)
        {
            if (language is null) language = "ru";

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

        public static List<OrderDTO> MapOrderToOrderDTO(List<Order> orders, string language)
        {
            if (language == null) language = "ru";

            var ordersDTO = new List<OrderDTO>();
            foreach (var order in orders)
                ordersDTO.Add(MapOrderToOrderDTO(order, language));

            return ordersDTO;
        }

        public static OrderItemDTO MapOrderItemToOrderItemDTO(OrderItem orderItem, string language)
        {
            if (language is null) language = "ru";

            return new OrderItemDTO
            {
                Id= orderItem.Id,
                ProductName = language == "ru"? orderItem.ProductName: orderItem.ProductNameUK,
                ProductPrice = orderItem.ProductPrice,
                Quantity = orderItem.Quantity,
                Product = MapProductToProductDTO(orderItem.Product, language),
            };
        }

        public static List<OrderItemDTO> MapOrderItemToOrderItemDTO(List<OrderItem> orderItems, string language)
        {
            if (language is null) language = "ru";
        
            var orderItemsDTO = new List<OrderItemDTO>();
            foreach (var orderItem in orderItems)
                orderItemsDTO.Add(MapOrderItemToOrderItemDTO(orderItem, language));

            return orderItemsDTO;
        }

        public static string GetLanguageFromHeaders(IHeaderDictionary headers)
        {
            if (headers == null)
                return "ru";

            headers.TryGetValue("Api-Language", out var lang);

            if(string.IsNullOrWhiteSpace(lang))
                return "ru";

            return lang;
        }
    }
}
