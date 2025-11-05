using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;
using ToDoListFuckThis.Data;
using ToDoListFuckThis.Mapping;
using UserManager.repository;
using UserManager.repository.IRepository;
namespace UserManager

{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Kết nối MySQL (Pomelo)
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            // Thêm controller + DI
            builder.Services.AddControllers();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // AutoMapper
            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<UserMapping>());
            // này là để map với thằng enums
            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            var app = builder.Build();

            // BẬT SWAGGER LUÔN (không chỉ dev)
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserManager API v1");
                c.RoutePrefix = "swagger"; // → truy cập: http://localhost:5064/swagger
            });

            // XÓA DÒNG NÀY (gây lỗi HTTPS)
            // app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapControllers();

            // THÊM DÒNG NÀY: Tạo route mặc định để test
            app.MapGet("/", () => Results.Json(new { message = "API UserManager đang chạy! Truy cập /swagger để test." }));

            app.Run();
        }
    }
}