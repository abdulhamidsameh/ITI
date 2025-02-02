﻿using ITI.BLL;
using ITI.BLL.Interfaces;
using ITI.BLL.Repositories;
using ITI.DAL.Data;
using ITI.DAL.Models;
using ITI.PL.Helpers;
using ITI.PL.Services.EmailSender;
using ITI.PL.Services.SmsMessage;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;

namespace ITI.PL.Extenshions
{
	public static class ApplicationServicesExtenshion
	{

		public static IServiceCollection ApplicationServices(this IServiceCollection services,IConfiguration configuration)
		{

			

			services.AddControllersWithViews();

			services.AddScoped<IUnitOfWork, UnitOfWork>();

			services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));

			services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{
				options.Password.RequiredUniqueChars = 2;
				options.Password.RequireDigit = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = true;

				options.Lockout.AllowedForNewUsers = true;
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);

				options.User.RequireUniqueEmail = true;

			}).AddEntityFrameworkStores<ApplicationDbContext>()
			.AddDefaultTokenProviders();


			services.ConfigureApplicationCookie(options =>
			{
				options.AccessDeniedPath = "/Home/error";
				options.LoginPath = "/Account/SignIn";
				options.ExpireTimeSpan = TimeSpan.FromDays(1);
			});
			services.AddTransient<IEmailSender, EmailSender>();

			services.AddTransient<ISmsServices, SmsServices>();

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
			}).AddGoogle(options =>
			{
				IConfiguration googleConfigration = configuration.GetSection("External");
				options.ClientId = googleConfigration["ClientId"]!;
				options.ClientSecret = googleConfigration["ClientSecret"]!;
			});

			return services;
		}
	}
}
