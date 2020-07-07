using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterDealer.Data.Entities;

namespace TwitterDealer.Interfaces
{
	public interface IAuthRepository
	{
		Task<User> Register(User user, string password);

		Task<User> Login(string username, string password);

		Task<bool> UserExists(string username);
	}
}
