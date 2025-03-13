using System.ComponentModel.DataAnnotations;

namespace Market.Models.ViewModels
{
	public class RegisterViewModel
	{
		[Required, MaxLength(50)]
		public required string Username { get; set; }
		[Required, MaxLength(50), EmailAddress]
		public required string Email { get; set; }
		[Required, MaxLength(50), DataType(DataType.Password)]
		public required string Password { get; set; }
		[Required, MaxLength(50), DataType(DataType.Password)]
		[Compare("Password"), Display(Name = "Re-enter Password")]
		public required string RePassword { get; set; }
	}

	public class LoginViewModel
	{
		[Required, MaxLength(50)]
		public required string Username { get; set; }
		[Required, MaxLength(50), DataType(DataType.Password)]
		public required string Password { get; set; }
		[Display(Name = "Remember me")]
		public bool RememberMe { get; set; }
	}
}
