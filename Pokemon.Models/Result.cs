namespace Pokemon.Models
{
    public class Result
    {
        public int count { get; set; }
        public string? next { get; set; }
        public string? previous { get; set; }
        public List<Pokemon>? results { get; set; }
    }
}
