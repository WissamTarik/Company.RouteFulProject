using Company.RouteFullProject.DAL.Data.Contexts;
using Company.RouteFulProject.BLL.Interfaces;
using Company.RouteFulProject.BLL.Repositories;
using Microsoft.EntityFrameworkCore;

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
                options.UseSqlServer("Server=. ; Database=CompanyRouteFull ; Trusted_Connection=True; TrustServerCertificate=True ;");
            });

            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();
          
            
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
