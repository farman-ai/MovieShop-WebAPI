using System.ComponentModel.DataAnnotations;

namespace MovieShop.ApplicationCore.Models;

public class UserRegisterModel
{
	[Required]
	[StringLength(50)]
	public string FirstName { get; set; } = "";

	[Required]
	[StringLength(50)]
	public string LastName { get; set; } = "";

	[Required]
	[EmailAddress]
	public string Email { get; set; } = "";

	[Required]
	[DataType(DataType.Password)]
	[StringLength(100, MinimumLength = 6)]
	public string Password { get; set; } = "";

	
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; }

 
    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }
}

