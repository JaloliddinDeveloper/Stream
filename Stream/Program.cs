using Stream.Brokers.Storages;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        builder.Services.AddTransient<IStorageBroker, StorageBroker>();

        var app = builder.Build();

        app.UseStaticFiles();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Video}/{action=Index}/{id?}");

        app.Run();
    }
}