using Application.Services;
using Core.Repositories;
using Core.Services;
using dotenv.net;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Presentation.Services;
using System.Text;

namespace Presentation
{
    public class Program
    {
        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Eficaz API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        Array.Empty<string>()
                    }
                });
            });
        }
        private static void InjectRepositoryDependency(IHostApplicationBuilder builder)
        {
            var connectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION"); // Obtém a string de conexão do .env

            builder.Services.AddDbContext<EficazDbContext>(options =>
                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString),
                    b => b.MigrationsAssembly("Presentation")
            ));
        }

        private static void AddControllersAndDependencies(IHostApplicationBuilder builder)
        {
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAddressService, AddressService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITokenService, TokenService>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAddressRepository, AddressRepository>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
        }

        private static void AuthenticationMiddleware(IHostApplicationBuilder builder)
        {
            var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY"); // Obtém a chave secreta do .env

            // Configuração de autenticação e autorização
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                });
            builder.Services.AddAuthorization();
        }

        private static void InitializeSwagger(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        private static void SeedOnInitialize(WebApplication app)
        {
            // Configura o banco de dados com dados de seed
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<EficazDbContext>();
                    SeedData.Initialize(services);
                }
                catch (Exception ex)
                {
                    // Log error
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
        }

        public static void Main(string[] args)
        {
            DotEnv.Load(); // Carrega as variáveis do .env

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            ConfigureSwagger(builder.Services);
            InjectRepositoryDependency(builder);
            AddControllersAndDependencies(builder);
            AuthenticationMiddleware(builder);

            // Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Scan(scan => scan
                .FromAssemblies(
                typeof(Core.Services.IUserService).Assembly,
                typeof(Infrastructure.Repositories.UserRepository).Assembly,
                typeof(Application.Services.UserService).Assembly)
                .AddClasses(classes => classes.Where(type => type.IsClass && !type.IsAbstract))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                InitializeSwagger(app);
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthorization();

            SeedOnInitialize(app);

            app.MapControllers();

            app.Run();
        }
    }
}
