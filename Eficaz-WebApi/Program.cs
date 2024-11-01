namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}