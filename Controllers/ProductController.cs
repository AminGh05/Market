using Market.Data;
using Market.Models;
using Market.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Market.Controllers
{
	[Authorize]
	public class ProductController : Controller
	{
		private readonly MarketContext _context;

		public ProductController(MarketContext context)
		{
			_context = context;
		}

		[AllowAnonymous]
		// product details page
		public IActionResult Details(int id)
		{
			var product = _context.Products.Include(p => p.Item).SingleOrDefault(p => p.Id == id);
			if (product == null)
			{
				return NotFound();
			}
			// find related categories of product with linq
			var categories = _context.Products
				.Where(p => p.Id == id)
				.SelectMany(selector: c => c.CategoryToProducts)
				.Select(ca => ca.Category)
				.ToList();
			// create a view model for page
			var vm = new DetailsViewModel() { Product = product, Categories = categories };
			return View(vm);
		}

		public IActionResult AddToCart(int id)
		{
			var product = _context.Products.Include(p => p.Item).SingleOrDefault(p => p.ItemId == id);
			// add a new order including the product if it isn't null
			if (product != null && product.Item != null)
			{
				int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
				// check if there's an open order
				var order = _context.Orders.FirstOrDefault(o => o.UserId == userId && !o.IsFinal);
				if (order != null)
				{
					var orderDetails = _context.OrderDetails.FirstOrDefault(od =>
						od.OrderId == order.Id && od.ProductId == product.Id);
					if (orderDetails != null)
					{
						orderDetails.Count += 1;
					}
					else
					{
						_context.OrderDetails.Add(new OrderDetails()
						{
							OrderId = order.Id,
							ProductId = product.Id,
							Price = product.Item.Price,
							Count = 1
						});
					}
				}
				else
				{
					// add a new order to database
					order = new() { UserId = userId, CreatedDate = DateTime.Now, IsFinal = false };
					_context.Orders.Add(order);
					_context.SaveChanges();

					_context.OrderDetails.Add(new OrderDetails()
					{
						OrderId = order.Id,
						ProductId = product.Id,
						Price = product.Item.Price,
						Count = 1
					});
				}

				_context.SaveChanges();
			}

			return RedirectToAction("Cart");
		}

		public IActionResult Cart()
		{
			int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
			var order = _context.Orders.Where(o => o.UserId == userId && !o.IsFinal)
				.Include(o => o.Details).ThenInclude(c => c.Product).FirstOrDefault();

			return View(order);
		}

		public IActionResult RemoveCartItem(int id)
		{
			var orderDetails = _context.OrderDetails.Find(id);
			if (orderDetails != null)
			{
				_context.Remove(orderDetails);
				_context.SaveChanges();
			}
			// refresh the page after removing
			return RedirectToAction("Cart");
		}

		[AllowAnonymous]
		[Route("Group/{id}/{name}")]
		public IActionResult ProductsByGroup(int id, string name)
		{
			ViewData["GroupName"] = name; // set the title of the page
			var products = _context.CategoryToProducts
				.Where(c => c.CategoryId == id)
				.Include(c => c.Product)
				.Select(c => c.Product)
				.ToList();

			return View(products);
		}
	}
}
