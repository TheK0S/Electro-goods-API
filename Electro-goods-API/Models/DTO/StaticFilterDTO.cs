namespace Electro_goods_API.Models.DTO
{
    public class StaticFilterDTO
    {
        public List<CategoryDTO>? Category { get; set; }
        public List<CountryDTO>? Country { get; set; }
        public List<ManufacturerDTO>? Manufacturer { get; set; }
    }
}
