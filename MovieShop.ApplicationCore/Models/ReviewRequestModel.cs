using System.ComponentModel.DataAnnotations;

namespace MovieShop.ApplicationCore.Models;

public class ReviewRequestModel
{
    public int MovieId { get; set; }

    [Range(1, 10)]
    public decimal Rating { get; set; }

    [Required]
    public string ReviewText { get; set; } = "";
}