using ShopTARge24.Core.Dto;

namespace ShopTARge24.Models.ChuckNorris
{
    public class ChuckNorrisViewModel
    {
        public List<Categories> Categories;
        public string? id { get; set; }
        public string? iconUrl { get; set; }
        public string? url { get; set; }
        public string? value { get; set; }
        public string? createdAt { get; set; }
        public string? updatedAt { get; set; }
    }
}
