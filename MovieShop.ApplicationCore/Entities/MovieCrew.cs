namespace MovieShop.ApplicationCore.Entities;

public class MovieCrew
{
    public int MovieId { get; set; }
    public int CrewId { get; set; }
    public int RoleId { get; set; }

    public Movie Movie { get; set; }
    public Crew Crew { get; set; }
    public Role Role { get; set; }
}