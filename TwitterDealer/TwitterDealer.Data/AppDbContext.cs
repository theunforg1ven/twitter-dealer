using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TwitterDealer.Data.Entities;

namespace TwitterDealer.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) 
			: base(options) { }

		public DbSet<SavedThread> SavedThreads { get; set; }

		public DbSet<User> Users { get; set; }

		public DbSet<Photo> Photos { get; set; }
	}
}
