using CourseEindcase.Controllers;
using CourseEindcase.Data;
using CourseEindcase.Interfaces;
using CourseEindcase.Parsers;
using CourseEindcase.Parsers.CoursePropertyParsers;
using CourseEindcase.Repositories;
using CourseEindcase.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace CourseEindcase
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            // Wanneer dit getest moet worden op een andere database, vervang dan 'DefaultConnection' door 'LocalDbConnection'
            // Zorg er ook voor dat er een Database aanwezig is met de naam 'CaseContext', anders werkt het niet ;)
            services.AddDbContext<CaseContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddTransient<ICourseParser>(sp => new CourseParser(new CourseTitleParser(), new CourseCodeParser(), new CourseDurationParser(), new CourseStartdateParser(), new EmptyParser()));
            
            services.AddScoped<ICoursesOverviewService,CourseOverviewService>();
            services.AddScoped<ICoursesOverviewRepository, CourseOverviewRepository>();
            
            services.AddScoped<ICoursesImportRepository, CourseImportRepository>();
            services.AddScoped<ICoursesImportService, CourseImportService>();

            services.AddCors();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // vervang de WithOrigins() met het localhost-adres van de client
            app.UseCors(options =>
            options.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
