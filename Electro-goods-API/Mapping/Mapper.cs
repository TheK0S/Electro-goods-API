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
