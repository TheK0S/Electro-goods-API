namespace Electro_goods_API.Models.DTO
{
    public class StaticFilterDTO
    {
        public List<CategoryDTO> Categories { get; set; }
        public List<CountryDTO> Countries { get; set; }
        public List<ManufacturerDTO> Manufacturers { get; set; }
    }
}
