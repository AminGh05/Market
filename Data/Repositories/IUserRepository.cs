using Market.Models;

namespace Market.Data.Repositories
{
	public interface IUserRepository
	{
		bool ExistsByEmail(string email);
		bool ExistsByUsername(string username);
		void AddUser(User user);
		User GetUserForLogin(string username, string password);
	}

	public class UserRepository : IUserRepository
	{
		private readonly MarketContext _context;

		public UserRepository(MarketContext context)
		{
			_context = context;
		}

		public void AddUser(User user)
		{
			_context.Users.Add(user);
			_context.SaveChanges();
		}

		public bool ExistsByEmail(string email)
		{
			return _context.Users.Any(u => u.Email == email);
		}

		public bool ExistsByUsername(string username)
		{
			return _context.Users.Any(u => u.Username == username);
		}

		public User GetUserForLogin(string username, string password)
		{
			return _context.Users
				.SingleOrDefault(u => u.Username == username && u.Password == password);
		}
	}
}
