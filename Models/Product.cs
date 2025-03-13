namespace Market.Models
{
	public class Product
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public required string Description { get; set; }
		public required int ItemId { get; set; }

		public ICollection<CategoryToProduct>? CategoryToProducts { get; set; }
		public Item? Item { get; set; }

		public List<OrderDetails>? Details { get; set; }
	}
}
