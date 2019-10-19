using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TwitterDealer.Data.Entities;

namespace TwitterDealer.Data
{
	public class AppDbContext : IdentityDbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) 
			: base(options) { }

		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
	}
}
