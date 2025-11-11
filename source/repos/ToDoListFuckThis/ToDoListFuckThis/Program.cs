using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ToDoListFuckThis.Data;
using ToDoListFuckThis.Mapping;
using ToDoListFuckThis.Repository;
using ToDoListFuckThis.Repository.IRepository;
using UserManager.repository;
using UserManager.repository.IRepository;

namespace UserManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Đọc Secret Key
            var secretKey = builder.Configuration["ApiSettings:Secret"]
                            ?? throw new InvalidOperationException("Missing ApiSettings:Secret in appsettings.json");

            // 2. DB Context
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            // 3. DI Repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAuthentication, AuthenticationRepository>();
            builder.Services.AddScoped<ISectionRepository, TodoSectionRepository>();
            builder.Services.AddScoped<ITodoRepository,TodoListRepository>();
            builder.Services.AddScoped<IProjectRepository,ProjectRepository>();
            // 4. AutoMapper
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<UserMapping>();
                cfg.AddProfile<RegisterMapping>();
                cfg.AddProfile <TodolistMapping>();
                cfg.AddProfile<SectionMapping>();
            });

            // 5. JSON Enum Converter
            builder.Services.AddControllers().AddNewtonsoftJson()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            // 6. JWT Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.FromMinutes(5),

                    ValidIssuer = "YourApp",     // PHẢI KHỚP VỚI TOKEN
                    ValidAudience = "YourApp",   // PHẢI KHỚP VỚI TOKEN

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });

            builder.Services.AddAuthorization();

            // 7. Swagger + Bearer Auth (CÚ PHÁP ĐÚNG)
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "UserManager API", Version = "v1" });

                // Bearer Auth Definition
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                // Áp dụng cho toàn bộ API (xóa nếu chỉ muốn từng endpoint)
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });
            });

            var app = builder.Build();

            // 8. Middleware Pipeline
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserManager API v1");
                c.RoutePrefix = "swagger"; // → http://localhost:xxxx/swagger
            });

            // BỎ HTTPS (như mày muốn)
            // app.UseHttpsRedirection();

            // THỨ TỰ QUAN TRỌNG:
            app.UseAuthentication();  // PHẢI TRƯỚC
            app.UseAuthorization();   // PHẢI SAU

            app.MapControllers();

            // Route test
            app.MapGet("/", () => Results.Json(new
            {
                message = "API UserManager đang chạy! Truy cập /swagger để test."
            }));

            app.Run();
        }
    }
}