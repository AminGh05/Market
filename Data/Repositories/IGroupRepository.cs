using Market.Models;
using Market.Models.ViewModels;

namespace Market.Data.Repositories
{
	public interface IGroupRepository
	{
		IEnumerable<Category> GetCategories();
		IEnumerable<GroupViewModel> GetGroups();
	}

	public class GroupRepository : IGroupRepository
	{
		private MarketContext _context { get; set; }

		public GroupRepository(MarketContext context)
		{
			_context = context;
		}

		public IEnumerable<Category> GetCategories()
		{
			var categories = _context.Categories.ToList();
			return categories;
		}

		public IEnumerable<GroupViewModel> GetGroups()
		{
			return _context.Categories
				.Select(c => new GroupViewModel()
				{
					GroupId = c.Id,
					Name = c.Name,
					Count = _context.CategoryToProducts.Count(g => g.CategoryId == c.Id)
				}).ToList();
		}
	}
}
