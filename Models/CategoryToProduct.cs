namespace Market.Models
{
	public class CategoryToProduct
	{
		public int CategoryId { get; set; }
		public int ProductId { get; set; }

		// navigation Property
		public Category? Category { get; set; }
		public Product? Product { get; set; }
	}
}
