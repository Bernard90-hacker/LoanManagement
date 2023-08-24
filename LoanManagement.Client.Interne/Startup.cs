using AspNetCore.RouteAnalyzer;
using LoanManagement.Client.Interne.Services;

namespace LoanManagement.Client;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddBrowserDetection();
        services.AddTransient<JournalisationService>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<HttpContextAccessor>();
        services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                options.SerializerSettings.DateFormatString = "dd/MM/yyyy HH:mm:ss";
            });
        services.AddLocalization(options => options.ResourcesPath = "Resources");
        services.AddRazorPages()
            .AddCookieTempDataProvider();
        services.AddControllersWithViews()
            .AddCookieTempDataProvider()
            .AddDataAnnotationsLocalization(options =>
            {
                var type = typeof(SharedResource);
                var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName ?? string.Empty);
                var factory = GetServiceProvider(services).GetService<IStringLocalizerFactory>();
                var localizer = factory?.Create(nameof(SharedResource), assemblyName.Name ?? string.Empty);
                options.DataAnnotationLocalizerProvider = (t, f) => localizer;
            });
        services.AddMvcCore()
            .AddApiExplorer();
        services.AddRouting(options => options.LowercaseUrls = false);
        services.AddHttpContextAccessor();
        services.AddDistributedMemoryCache();
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(10);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
            options.Cookie.Name = ".LoanManagement.Session";
        });
        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.SlidingExpiration = true;
            options.Cookie.Name = ".LoanManagement.Lmgs";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
        });
        services.AddDataProtection();
        services.AddSingleton<DataProtectionPurposeStrings>();
        //Configuration for large uploads
        services.Configure<FormOptions>(o =>
        {
            o.ValueLengthLimit = int.MaxValue;
            o.MultipartBodyLengthLimit = int.MaxValue;
            o.MemoryBufferThreshold = int.MaxValue;
        });
        services.AddMemoryCache();
        services.AddHealthChecks();
        services.AddRouteAnalyzer();
        services.AddSession();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/erreur");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        var supportedCultures = new[]
        {
            new CultureInfo("fr"),
            new CultureInfo("en"),
            new CultureInfo("de"),
            new CultureInfo("pt")
        };
        var localizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("fr"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures,
            ApplyCurrentCultureToResponseHeaders = true
        };
        app.UseRequestLocalization(localizationOptions);

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseCookiePolicy();
        app.UseSession();

        app.UseHealthChecks("/health");

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}");
        });
    }

    ServiceProvider GetServiceProvider(IServiceCollection services)
    {
        return services.BuildServiceProvider();
    }
}
