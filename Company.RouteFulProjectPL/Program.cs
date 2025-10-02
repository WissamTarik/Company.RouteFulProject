using Company.RouteFullProject.DAL.Data.Contexts;
using Company.RouteFulProject.BLL;
using Company.RouteFulProject.BLL.Interfaces;
using Company.RouteFulProject.BLL.Repositories;
using Company.RouteFulProject.PL.Mapping;
using Company.RouteFulProject.PL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Company.RouteFulProjectPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
           
           
            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();//Allow Dependency injection for Employee repository
            //builder.Services.AddAutoMapper(typeof(EmployeeProfile));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new DepartmentProfile()));

            //Methods that allow dependency injection of object of classes by CLR
            //Methods differ in life time
            //builder.Services.AddScoped<>();//Create object life time per request-Unreachable object
            //builder.Services.AddTransient();//Create an object its lifetime per operation
            //builder.Services.AddSingleton()//Create an object its lifetime per application


            builder.Services.AddScoped<IScopedService, ScopedService>();//Per request
            builder.Services.AddTransient<ITransientService, TransientService>();//per operation
            builder.Services.AddSingleton<ISingletonService, SingletonService>();//per application
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

            app.UseRouting();

           

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
