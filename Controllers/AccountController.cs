using Market.Data.Repositories;
using Market.Models;
using Market.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Market.Controllers
{
	public class AccountController : Controller
	{
		private readonly IUserRepository _userRepository;

		public AccountController(IUserRepository repository)
		{
			_userRepository = repository;
		}

		#region Register

		[Route("/Register")]
		public IActionResult Register()
		{
			return View();
		}

		[Route("/Register")]
		[HttpPost]
		public IActionResult Register(RegisterViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			if (_userRepository.ExistsByUsername(model.Username.ToLower()))
			{
				ModelState.AddModelError("Username", "Username already exists");
				return View(model);
			}

			if (_userRepository.ExistsByEmail(model.Email.ToLower()))
			{
				ModelState.AddModelError("Email", "Email already exists");
				return View(model);
			}

			User user = new()
			{
				Email = model.Email.ToLower(),
				Username = model.Username.ToLower(),
				Password = model.Password,
				RegirsterDate = DateTime.Now,
				IsAdmin = false
			};
			_userRepository.AddUser(user);

			return View("SuccessfulRegister", model);
		}

		#endregion

		#region Login

		[Route("/Login")]
		public IActionResult Login()
		{
			return View();
		}

		[Route("/Login")]
		[HttpPost]
		public IActionResult Login(LoginViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var user = _userRepository.GetUserForLogin(model.Username, model.Password);
			if (user == null)
			{
				ModelState.AddModelError("Username", "Account info not correct");
				return View(model);
			}

			var claims = new List<Claim>
			{
				new(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new(ClaimTypes.Name, user.Username),
				new(ClaimTypes.Email, user.Email),
			};
			var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var principal = new ClaimsPrincipal(identity);
			var properties = new AuthenticationProperties { IsPersistent = model.RememberMe };

			HttpContext.SignInAsync(principal, properties);
			return RedirectToAction("Index", "Home");
		}

		#endregion

		[Route("/Logout")]
		public IActionResult Logout()
		{
			HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home");
		}
	}
}
