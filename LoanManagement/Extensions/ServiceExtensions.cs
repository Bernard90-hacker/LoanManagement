using LoanManagement.core;

namespace LoanManagement.API.Extensions
{
	public static class ServiceExtensions
	{
		public static void ConfigureLoggerService(this IServiceCollection services)
		{
			services.AddSingleton<ILoggerManager, LoggerManager>();
		}

		public static void ConfigureDbContextService(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<LoanManagementDbContext>(
					options => options.UseSqlServer(configuration.GetConnectionString("Default"),
					x => x.MigrationsAssembly("LoanManagement.Data.SqlServer"))
				);
		}

		public static void ConfigureLoggingService(this IServiceCollection services)
		{
			services.AddSingleton<ILoggerManager, LoggerManager>();
		}

		public static void ConfigureApiService(this IServiceCollection services)
		{
			services.AddTransient<IUserService, UserService>();
			services.AddTransient<UserService>();
			services.AddTransient<MailService>();
			services.AddTransient<IUserTokenService, UserTokenService>();
			services.AddTransient<UserTokenService>();
		}

		public static void ConfigureUnitOfWorkService(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
		}

		public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<ExceptionMiddleware>();
		}
	}
}
