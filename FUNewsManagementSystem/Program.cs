using LeNgocHaiMVC.DAO;
using LeNgocHaiMVC.Models;
using LeNgocHaiMVC.Repositories;
using LeNgocHaiMVC.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace LeNgocHaiMVC
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddDbContext<FunewsManagementContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("FUNewsManagementDB")));

			builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
			builder.Services.AddScoped<ICategoryDAO, CategoryDAO>();
			builder.Services.AddScoped<CategoryService>();
			builder.Services.AddScoped<INewsRepository, NewsRepository>();
			builder.Services.AddScoped<INewsDAO, NewsDAO>();
			builder.Services.AddScoped<NewsService>();
			builder.Services.AddScoped<IAccountRepository, AccountRepository>();
			builder.Services.AddScoped<IAccountDAO, AccountDAO>();
			builder.Services.AddScoped<AccountService>();
			builder.Services.AddScoped<EmailService>();
			builder.Services.AddHttpContextAccessor();
			builder.Services.AddControllersWithViews();

			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
		   .AddCookie(options =>
		   {
			   options.LoginPath = "/Account/Login"; // Chuyển hướng nếu chưa đăng nhập
			   options.AccessDeniedPath = "/Account/AccessDenied"; // Chuyển hướng nếu không có quyền
		   });

			builder.Services.AddAuthorization();

			// Bật Session
			builder.Services.AddDistributedMemoryCache(); // Cần thiết để session hoạt động
			builder.Services.AddSession(); // Chỉ bật session, không đặt thời gian

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseSession(); // Kích hoạt session
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}