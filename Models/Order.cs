using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Models
{
	public class Order
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public int UserId { get; set; }
		[Required]
		public DateTime CreatedDate { get; set; }
		public bool IsFinal { get; set; }

		[ForeignKey("UserId")]
		public User? User { get; set; }
		public List<OrderDetails>? Details { get; set; }
	}
}
