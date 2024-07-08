using ITI.PL.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Text.Json;

namespace ITI.PL.Middlewares
{
	public class ExceptionMiddleware
	{
		private readonly IConfiguration _configuration;
		private readonly RequestDelegate _next;
		private readonly ILoggerFactory _loggerFactory;
		private readonly IWebHostEnvironment _env;

		public ExceptionMiddleware(IConfiguration configuration,RequestDelegate next ,ILoggerFactory loggerFactory,IWebHostEnvironment env)
        {
			_configuration = configuration;
			_next = next;
			_loggerFactory = loggerFactory;
			_env = env;
		}

		public async Task InvokeAsync(HttpContext context) 
		{
			try
			{
				await _next.Invoke(context);
			}
			catch (Exception ex)
			{
				// 1. Log Exception

				var loger = _loggerFactory.CreateLogger<ExceptionMiddleware>();
				loger.LogError(ex.Message);

				context.Response.StatusCode = 500;
				context.Response.ContentType = "application/json";


				var response = _env.IsDevelopment() ? new ApiExceptionResponse(500,ex.Message,ex.StackTrace?.ToString())
					: new ApiExceptionResponse(500);

				var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

				var json =  JsonSerializer.Serialize(response, options);


				context.Response.Redirect($"{_configuration["BaseUrl"]}/Home/Error");

				await context.Response.WriteAsync(json);
				
			}
		}
    }
}
