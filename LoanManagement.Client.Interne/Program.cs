namespace LoanManagement.Client;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                    {
                        options.Limits.MaxRequestBodySize = null;
                        options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(30);
                    });
                    webBuilder.UseStartup<Startup>();
                });
}
