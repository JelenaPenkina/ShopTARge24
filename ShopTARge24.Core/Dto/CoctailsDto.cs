namespace ShopTARge24.Core.Dto
{
    public class CoctailsDto
    {
        public List<Ingredients> Ingredients {  get; set; }
    }

    public class Ingredients
    {
        public int idIngredient {  get; set; }
        public string ingredient { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Alcohol { get; set; }
        public int ABV { get; set; }

    }
}
