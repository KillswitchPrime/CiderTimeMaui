namespace CiderTimeMaui.Models
{
    public class Label
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Beverage> Beverages { get; set; }
    }
}
