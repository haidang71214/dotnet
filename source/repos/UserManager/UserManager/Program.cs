using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using UserManager.Data; 

namespace UserManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ✅ Thêm connection string từ appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // ✅ Đăng ký DbContext với MySQL (Pomelo)
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            // ✅ Thêm controller
            builder.Services.AddControllers();

            // ✅ Thêm Swagger (dành cho test API)
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // ✅ Cấu hình pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
