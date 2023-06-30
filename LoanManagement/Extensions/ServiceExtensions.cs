using FluentAssertions.Common;
using LoanManagement.core;
using LoanManagement.service.Services.Users_Management;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection.Extensions;

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

		public static void ConfigureDetectionService(this IServiceCollection services)
		{
			services.AddBrowserDetection();
		}

		public static void ConfigureApiService(this IServiceCollection services)
		{
			services.AddTransient<IDirectionService, DirectionService>();
			services.AddTransient<IDepartmentService, DepartementService>();
			services.AddTransient<IApplicationService, ApplicationService>();
			services.AddTransient<IEmployeService, EmployeService>();
			services.AddTransient<IHabilitationProfilService, HabilitationProfilService>();
			services.AddTransient<IJournalService, JournalService>();
			services.AddTransient<IMenuService, MenuService>();
			services.AddTransient<IMotDePasseService, MotDePasseService>();
			services.AddTransient<IParamMotDePasseService, ParamMotDePasseService>();
			services.AddTransient<IProfilService, ProfilService>();
			services.AddTransient<ITypeJournalService, TypeJournalService>();
			services.AddTransient<IUtilisateurService, UtilisateurService>();
			services.AddTransient<DepartementService>();
			services.AddTransient<DirectionService>();
			services.AddTransient<ApplicationService>();
			services.AddTransient<EmployeService>();
			services.AddTransient<HabilitationProfilService>();
			services.AddTransient<JournalService>();
			services.AddTransient<MenuService>();
			services.AddTransient<MotDePasseService>();
			services.AddTransient<ParamMotDePasseService>();
			services.AddTransient<ProfilService>();
			services.AddTransient<TypeJournalService>();
			services.AddTransient<UtilisateurService>();
			services.AddTransient<JournalisationService>();
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
