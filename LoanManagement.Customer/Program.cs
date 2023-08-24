using LoanManagement.Customer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<HttpContextAccessor>();
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
        options.SerializerSettings.DateFormatString = "dd/MM/yyyy HH:mm:ss";
    });
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddRazorPages()
    .AddCookieTempDataProvider();
builder.Services.AddControllersWithViews()
    .AddCookieTempDataProvider()
    .AddDataAnnotationsLocalization(options =>
    {
        var type = typeof(SharedResource);
        var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName ?? string.Empty);
        var factory = GetServiceProvider(builder.Services).GetService<IStringLocalizerFactory>();
        var localizer = factory?.Create(nameof(SharedResource), assemblyName.Name ?? string.Empty);
        options.DataAnnotationLocalizerProvider = (t, f) => localizer;
    });
builder.Services.AddMvcCore()
    .AddApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = false);
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = ".LoanManagement.Session";
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.SlidingExpiration = true;
    options.Cookie.Name = ".LoanManagement.Lmgs";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
});
builder.Services.AddDataProtection();
builder.Services.AddSingleton<DataProtectionPurposeStrings>();
//Configuration for large uploads
builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});
builder.Services.AddMemoryCache();
builder.Services.AddHealthChecks();
builder.Services.AddRouteAnalyzer();
builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public partial class Program
{
    static ServiceProvider GetServiceProvider(IServiceCollection services)
    {
        return services.BuildServiceProvider();
    }
}