
namespace Electro_goods_API.Models.Filters
{
    public class OrderFilter
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StatusId { get; set; }
        public string? ShippingCity { get; set; }
        public string? UserNamePart { get; set; }
        public string? EmailPart { get; set; }
    }
}
