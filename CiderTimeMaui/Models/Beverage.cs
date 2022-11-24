namespace CiderTimeMaui.Models
{
    public class Beverage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
