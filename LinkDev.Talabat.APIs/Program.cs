using LinkDev.Talabat.Infrastructure.Persistence;
namespace LinkDev.Talabat.APIs
{
    public class Program
    {
        // Entry Point
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region ConfigureServices
            // Add services to the container.

            builder.Services.AddControllers(); // Register Required Services by Asp .Net Core Wep Apis to DI Container


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // builder.Services.AddDbContext<StoreContext>(optionsBuilder =>
            // {
            //     optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("StoreContext"));
            // });
             builder.Services.AddPersistanceServices(builder.Configuration);
            #endregion

            var app = builder.Build();

            #region Configure Kestrel Middlewares
            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();



            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
