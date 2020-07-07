using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TwitterDealer.Data;
using TwitterDealer.Data.Entities;
using TwitterDealer.Interfaces;

namespace TwitterDealer.Repositories
{
	public class AuthRepository : IAuthRepository
	{
		private readonly AppDbContext _context;

		public AuthRepository(AppDbContext context)
		{
			_context = context;
		}


		public async Task<User> Login(string username, string password)
		{
			var user = await _context.Users.Include(u => u.Photos).FirstOrDefaultAsync(u => u.UserName == username);

			if (user == null)
				return null;

			if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
				return null;

			return user;
		}

		public async Task<User> Register(User user, string password)
		{
			byte[] passwordHash, passwordSalt;
			CreatePasswordHash(password, out passwordHash, out passwordSalt);

			user.PasswordHash = passwordHash;
			user.PasswordSalt = passwordSalt;

			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();

			return user;
		}

		public async Task<bool> UserExists(string username)
		{
			if (await _context.Users.AnyAsync(u => u.Username == username))
				return true;

			return false;
		}

		private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512(passwordSalt))
			{
				var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

				for (int i = 0; i < computedHash.Length; i++)
					if (computedHash[i] != passwordHash[i])
						return false;
			}

			return true;
		}

		private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
			}
		}
	}
}
