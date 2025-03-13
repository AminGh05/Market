using System.ComponentModel.DataAnnotations;

namespace Market.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }
		[Required, MaxLength(50)]
		public required string Username { get; set; }
		[Required, MaxLength(50)]
		public required string Email { get; set; }
		[Required, MaxLength(50)]
		public required string Password { get; set; }
		[Required]
		public required DateTime RegirsterDate { get; set; }
		public bool IsAdmin { get; set; }

		public List<Order>? Orders { get; set; }
	}
}
