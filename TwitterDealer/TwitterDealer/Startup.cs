using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using TwitterDealer.Data;
using TwitterDealer.Data.Entities;
using TwitterDealer.Interfaces;
using TwitterDealer.Models;
using TwitterDealer.Repositories;
using TwitterDealer.Services;
using TwitterDealer.TwitterApi;

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
			services.AddTransient<ISaveThreadRepository, SaveThreadRepository>();

			services.AddTransient<IUserService, UserService>();
			services.AddTransient<ITweetDataService, TweetDataService>();
			services.AddTransient<ITweetThreadService, TweetThreadService>();

			services.AddHttpContextAccessor();

			// inject app settings
			services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

			services.AddControllers();
				 //.AddNewtonsoftJson(options =>
				 //{
					// var resolver = options.SerializerSettings.ContractResolver;
					// if (resolver != null)
					//	 ((DefaultContractResolver)resolver).NamingStrategy = null; // use real values to serialize
				 //});

			services.AddDbContext<AppDbContext>(options =>
			options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


			services.Configure<IdentityOptions>(options => 
			{
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 5;
			});

			services.AddCors();

			// jwt web token auth

			var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JwtSecret"].ToString());

			services.AddAuthentication(x => 
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x => 
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = false;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					ClockSkew = TimeSpan.Zero
				};
			});

		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			
			app.UseCors(options => options.WithOrigins(Configuration["ApplicationSettings:ClientUrl"].ToString())
														.AllowAnyMethod()
														.AllowAnyHeader()
														.AllowCredentials());
			app.UseAuthentication();
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();
		

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			AuthInit.AuthenticateTwitter();
		}
	}
}
