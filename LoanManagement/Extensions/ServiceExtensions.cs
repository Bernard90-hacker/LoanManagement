using FluentAssertions.Common;
using LoanManagement.core;
using LoanManagement.service.Services.Loan_Management;
using LoanManagement.service.Services.Users_Management;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Security.Permissions;
using System.Text;

namespace LoanManagement.API.Extensions
{
	public static class ServiceExtensions
	{
        public const string AllowSpecificOrigins = "default_policy";
        public const string AllowTestOrigins = "test_policy";

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

		public static void ConfigureApiService(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddTransient<IDirectionService, DirectionService>();
			services.AddTransient<IDepartmentService, DepartementService>();
			services.AddTransient<IApplicationService, ApplicationService>();
			services.AddTransient<IEmployeService, EmployeService>();
			services.AddTransient<IHabilitationProfilService, HabilitationProfilService>();
			services.AddTransient<IJournalService, JournalService>();
			services.AddTransient<IMenuService, MenuService>();
			services.AddTransient<IMotDePasseService, MotDePasseService>();
			services.AddTransient<IParamGlobalService, ParamGlobalService>();
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
			services.AddTransient<ParamGlobalService>();
			services.AddTransient<ProfilService>();
			services.AddTransient<TypeJournalService>();
			services.AddTransient<UtilisateurService>();
			services.AddTransient<JournalisationService>();
			services.AddTransient<EmailService>();
			services.AddTransient<TokenService>();
			services.AddTransient<FicheAssuranceService>();
			services.AddTransient<IClientService, ClientService>();
			services.AddTransient<ClientService>();
			services.AddTransient<ICompteService, CompteService>();
			services.AddTransient<CompteService>();
			services.AddTransient<IDeroulementService, DeroulementService>();
			services.AddTransient<DeroulementService>();
			services.AddTransient<IDossierClientService, DossierClientService>();
			services.AddTransient<DossierClientService>();
			services.AddTransient<IEtapeDeroulementService, EtapeDeroulementService>();
			services.AddTransient<EtapeDeroulementService>();
			services.AddTransient<ISanteClientService, InfoSanteClientService>();
			services.AddTransient<InfoSanteClientService>();
			services.AddTransient<IMembreOrganeService, MembreOrganeService>();
			services.AddTransient<MembreOrganeService>();
			services.AddTransient<INatureQuestionService, NatureQuestionService>();
			services.AddTransient<NatureQuestionService>();
			services.AddTransient<IOrganeDecisionService, OrganeDecisionService>();
			services.AddTransient<OrganeDecisionService>();
			services.AddTransient<IRoleOrganeService, RoleOrganeService>();
			services.AddTransient<RoleOrganeService>();
			services.AddTransient<IStatutDossierClientService, StatutDossierClientService>();
			services.AddTransient<StatutDossierClientService>();
			services.AddTransient<IPretAccordService, PretAccordService>();
			services.AddTransient<PretAccordService>();
			services.AddTransient<IPeriodicitePaiementService, PeriodicitePaiementService>();
			services.AddTransient<PeriodicitePaiementService>();
			services.AddTransient<IStatutMaritalService, StatutMaritalService>();
			services.AddTransient<StatutMaritalService>();
			services.AddTransient<ITypePretService, TypePretService>();
			services.AddTransient<IEmployeurService, EmployeurService>();
			services.AddTransient<EmployeurService>();
			services.AddTransient<TypePretService>();
			services.AddTransient<ITokenService, TokenService>();
			services.AddCustomAuthentification(configuration);
			services.AddCustomCors(configuration);
        }

		public static void ConfigureUnitOfWorkService(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
		}

		public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
			app.UseMiddleware<ExceptionMiddleware>();
		}

        public static void AddCustomCors(this IServiceCollection services, IConfiguration configuration)
        {
            CorsOption corsOption = new();
            configuration.GetSection("Cors").Bind(corsOption);

            services.AddCors(options =>
            {
                options.AddPolicy(AllowSpecificOrigins, builder =>
                {
                    builder.WithOrigins(corsOption.SpecOrigin)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });

                options.AddPolicy(AllowTestOrigins, builder =>
                {
                    builder.WithOrigins(corsOption.TestOrigin)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        /// <summary>
        /// Ajout de la configuration pour JWT
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddCustomAuthentification(this IServiceCollection services, IConfiguration configuration)
        {
            SecurityOptions securityOption = new();
            configuration.GetSection("Jwt").Bind(securityOption);
            var key = securityOption.Key;
            var issuer = securityOption.Issuer;
            var audience = securityOption.Audience;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var utilisateurService = context.HttpContext.RequestServices.GetRequiredService<IUtilisateurService>();
                        var utilisateurId = int.Parse(context.Principal.Identity.Name); //Par défaut, il est mis en correspondance avec le claim Name
                        var utilisateur = utilisateurService.GetUserById(utilisateurId);
                        if (utilisateur == null)
                        {
                            //retourne unauthorized si l'utilisateur n'existe plus
                            context.Fail("Unauthorized");
                        }

                        return Task.CompletedTask;
                    }
                };
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
				options.IncludeErrorDetails = true;
                //options.TokenValidationParameters = new TokenValidationParameters()
                //{
                //    ValidateIssuer = true,
                //    ValidateAudience = true,
                //    ValidateLifetime = true,
                //    ValidateIssuerSigningKey = true,
                //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                //    ValidIssuer = issuer,
                //    ValidAudience = audience
                //};
            });
        }
    }
}
