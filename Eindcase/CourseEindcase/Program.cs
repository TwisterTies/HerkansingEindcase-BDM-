using CourseEindcase;
using CourseEindcase.Controllers;
using CourseEindcase.Interfaces;
using CourseEindcase.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace CourseEindcase
{
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
                    webBuilder.UseStartup<Startup>();
                });
    }
}
