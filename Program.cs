using Bookstore.Data;
using Bookstore.Services;
using Microsoft.EntityFrameworkCore;

namespace Bookstore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


			builder.Services.AddDbContext<BookstoreContext>(options =>
			{
				options.UseMySql(
					builder
						.Configuration
						.GetConnectionString("BookstoreContext"),
					ServerVersion
						.AutoDetect(
							builder
								.Configuration
								.GetConnectionString("BookstoreContext")
						)
				);
			});

            builder.Services.AddScoped<GenreService>();
            builder.Services.AddScoped<BookService>();
            builder.Services.AddScoped<SeedingService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {
                app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingService>().Seed();
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
