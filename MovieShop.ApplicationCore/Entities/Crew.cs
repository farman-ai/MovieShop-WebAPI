namespace MovieShop.ApplicationCore.Entities
{
    public class Crew
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Gender { get; set; }
        public string? TmdbUrl { get; set; }
        public string? ProfilePath { get; set; }

        public ICollection<MovieCrew> MovieCrews { get; set; } = new List<MovieCrew>();
    }
}