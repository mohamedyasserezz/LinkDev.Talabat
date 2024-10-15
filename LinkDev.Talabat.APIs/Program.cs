using LinkDev.Talabat.APIs.Extentions;
using LinkDev.Talabat.APIs.Services;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Infrastructure.Persistence;
using LinkDev.Talabat.Core.Application;
namespace LinkDev.Talabat.APIs
{
    public class Program
    {
        // Entry Point
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region ConfigureServices
            // Add services to the container.

            builder.Services.
                AddControllers()
                .ConfigureApiBehaviorOptions(opthions =>
                {
                    opthions.SuppressModelStateInvalidFilter = true;
                })
                .AddApplicationPart(typeof(Controllers.AssemblyInformation).Assembly); // Register Required Services by Asp .Net Core Wep Apis to DI Container


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // builder.Services.AddDbContext<StoreContext>(optionsBuilder =>
            // {
            //     optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("StoreContext"));
            // });
            builder.Services.AddPersistanceServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped(typeof(ILoggedInUserService), typeof(LoggedInUserService));
            #endregion

            var app = builder.Build();

            #region Databases Initialization
         
            await app.InitializeStoreContextAsync();
            #endregion
             
            #region Configure Kestrel Middlewares
            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.UseStaticFiles();


            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
