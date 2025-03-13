using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Models
{
	public class OrderDetails
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public int OrderId { get; set; }
		[Required]
		public int ProductId { get; set; }
		[Required]
		public decimal Price { get; set; }
		[Required]
		public int Count { get; set; }

		[ForeignKey("OrderId")]
		public Order? Order { get; set; }
		[ForeignKey("ProductId")]
		public Product? Product { get; set; }
	}
}
