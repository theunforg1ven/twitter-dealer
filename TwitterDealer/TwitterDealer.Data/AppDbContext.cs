using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace TwitterDealer.Data
{
	public class AppDbContext : IdentityDbContext
	{
		public AppDbContext(DbContextOptions options) : base(options)
		{ }


	}
}
