namespace Market.Models.ViewModels
{
	public class DetailsViewModel
	{
		public required Product Product { get; set; }
		public required List<Category> Categories { get; set; }
	}
}
