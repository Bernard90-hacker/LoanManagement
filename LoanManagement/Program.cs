
using LoanManagement.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
	options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(30);
});


// Add services to the container.
builder.Services.ConfigureLoggerService();
builder.Services.AddControllers(config =>
{
	config.CacheProfiles.Add("LocalCacheProfile", new CacheProfile
	{
		Duration = 30
	});
})
.AddNewtonsoftJson(options =>
{
	options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
	options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
	options.SerializerSettings.DateFormatString = "dd/MM/yyyy HH:mm:ss";
});
//Configuration for SQL server
builder.Services.ConfigureDbContextService(builder.Configuration);
//Configuration for UnitOfWork
builder.Services.ConfigureUnitOfWorkService();
builder.Services.ConfigureApiService();
//Configuration for AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Projet API web de gestion des prêts et autorisations", Version = "v1" });
});
//Configuration for AutoMapper
//Configuration for Response Caching
builder.Services.AddResponseCaching();
//Configuration for Memory Cache
builder.Services.AddMemoryCache();
//builder.Services.ConfigureCacheService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Example.API v1"));
}
else
{
	app.UseHsts();
}

app.Use(async (context, next) =>
{
	if (context.Request.Path.StartsWithSegments("/api"))
	{
		context.Features.Get<IHttpMaxRequestBodySizeFeature>().MaxRequestBodySize = null;
	}

	await next(context);
});

//app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseResponseCaching();

app.UseRouting();
//Routing area
app.Use(async (context, next) =>
{
	Console.WriteLine($"Trouvé : {context.GetEndpoint()?.DisplayName}");
	await next(context);
});

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapGet("/example", async context =>
{
	await context.Response.WriteAsync("Mise en place d'un modèle de service");
});

app.MapControllers();

app.Run();

