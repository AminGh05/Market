using System.Diagnostics;
using Market.Data;
using Market.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly MarketContext _context;

		public HomeController(ILogger<HomeController> logger, MarketContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
			var products = _context.Products.ToList();
			return View(products);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[Route("ContactUs")]
		public IActionResult ContactUs()
		{
			return View();
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
