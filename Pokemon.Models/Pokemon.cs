namespace Pokemon.Models
{
    public class Pokemon
    {
        public string? name { get; set; }
        public string? url { get; set; }
        public string? imageUrl { get; set; }
        public List<Stat>? stats { get; set; }
    }
}
