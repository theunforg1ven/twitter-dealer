using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using TwitterDealer.Data;
using TwitterDealer.Data.Entities;

namespace TwitterDealer
{
	public class Startup
	{
		public Startup(IWebHostEnvironment webHostEnvironment)
		{
			Configuration = new ConfigurationBuilder()
				.SetBasePath(webHostEnvironment.ContentRootPath)
				.AddJsonFile("appsettings.json")
				.Build();
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers()
				 .AddNewtonsoftJson(options =>
				 {
					 var resolver = options.SerializerSettings.ContractResolver;
					 if (resolver != null)
						 ((DefaultContractResolver)resolver).NamingStrategy = null; // use real values to serialize
				 }); ;

			services.AddDbContext<AppDbContext>(options =>
			options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddDefaultIdentity<ApplicationUser>()
				.AddEntityFrameworkStores<AppDbContext>();

			services.Configure<IdentityOptions>(options => 
			{
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 5;
			});

			services.AddCors();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			//app.UseCors(options => options.WithOrigins(Configuration["ApplicationSettings:ClientUrl"].ToString())
			//								.AllowAnyMethod()
			//								.AllowAnyHeader());

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();
			app.UseAuthentication();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
