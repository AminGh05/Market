using Microsoft.AspNetCore.Mvc;
using Market.Data.Repositories;

namespace Market.Components
{
	public class ProductGroupsComponent : ViewComponent
	{
		private readonly IGroupRepository _groupRepository;

		public ProductGroupsComponent(IGroupRepository groupRepository)
		{
			_groupRepository = groupRepository;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View("/Views/Components/ProductGroupsComponent.cshtml", _groupRepository.GetGroups());
		}
	}
}
