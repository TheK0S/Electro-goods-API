using Electro_goods_API.Models.DTO;

namespace Electro_goods_API.Comparers
{
    public class ProductAttributeFilterItemDTOComparer : IEqualityComparer<ProductAttributeFilterItemDTO>
    {
        public bool Equals(ProductAttributeFilterItemDTO x, ProductAttributeFilterItemDTO y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (x is null || y is null)
                return false;
            return x.Value == y.Value;
        }

        public int GetHashCode(ProductAttributeFilterItemDTO obj)
        {
            return obj.Value.GetHashCode();
        }
    }

}
