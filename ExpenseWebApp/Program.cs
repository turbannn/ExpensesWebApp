using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ExpenseWebAppBLL.Services;
using ExpenseWebAppDAL.Data;
using ExpenseWebAppDAL.Interfaces;
using ExpenseWebAppDAL.Repositories;
using FluentValidation;

namespace ExpenseWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Validators
            builder.Services.AddValidatorsFromAssembly(Assembly.Load("ExpenseWebAppBLL"));

            // Mappers
            builder.Services.AddAutoMapper(Assembly.Load("ExpenseWebAppBLL"));

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            //!!! VERSION TROUBLE 9.0.0-preview.1.24081.2 for Relational, Core and Tools || 9.0.0-preview.1 for Pomelo MySql !!!
            builder.Services.AddDbContext<WebAppContext>(opt =>
            {
                opt.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 29)));
                //opt.LogTo(Console.WriteLine);
                
            });

            builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ExpenseService>();
            builder.Services.AddScoped<CategoryService>();

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
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
