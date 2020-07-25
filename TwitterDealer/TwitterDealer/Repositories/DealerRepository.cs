using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterDealer.Data;
using TwitterDealer.Data.Entities;
using TwitterDealer.Interfaces;

namespace TwitterDealer.Repositories
{
	public class DealerRepository : IDealerRepository
	{
		private readonly AppDbContext _context;

		public DealerRepository(AppDbContext context)
		{
			_context = context;
		}

		public void Add<T>(T entity) where T : class
		{
			_context.Add(entity);
		}

		public void Delete<T>(T entity) where T : class
		{
			_context.Remove(entity);
		}

		public async Task<User> GetUser(int id)
		{
			var user = await _context.Users
									.Include(u => u.Photos)
									.FirstOrDefaultAsync(u => u.Id == id);

			return user;
		}

		public async Task<IEnumerable<User>> GetUsers()
		{
			var users = await _context.Users
									.Include(u => u.Photos)
									.ToListAsync();

			return users;
		}

		public async Task<bool> SaveAll()
		{
			if (await _context.SaveChangesAsync() > 0)
				return true;

			return false;
		}
	}
}
