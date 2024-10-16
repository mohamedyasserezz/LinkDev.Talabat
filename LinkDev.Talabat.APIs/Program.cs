using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.APIs.Extentions;
using LinkDev.Talabat.APIs.Middlewares;
using LinkDev.Talabat.APIs.Services;
using LinkDev.Talabat.Core.Application;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
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
                    opthions.SuppressModelStateInvalidFilter = false;
                    opthions.InvalidModelStateResponseFactory = ((actionContext) =>
                    {
                        var errors = actionContext.ModelState.Where(P => P.Value!.Errors.Count > 0)
                                          .Select(P => new ApiValdiationErrorResponse.ValidationError()
                                          {
                                              Field = P.Key,
                                              Errors = P.Value!.Errors.Select(E => E.ErrorMessage)
                                          });
                        return new BadRequestObjectResult(new ApiValdiationErrorResponse() { Errors = errors });
                    });
                })
                .AddApplicationPart(typeof(Controllers.AssemblyInformation).Assembly); // Register Required Services by Asp .Net Core Wep Apis to DI Container

            //builder.Services.Configure<ApiBehaviorOptions>(opthions =>
            //{
            //    opthions.SuppressModelStateInvalidFilter = false;
            //    opthions.InvalidModelStateResponseFactory = ((actionContext) =>
            //    {
            //        var errors = actionContext.ModelState.Where(P => P.Value!.Errors.Count > 0)
            //                          .SelectMany(P => P.Value!.Errors)
            //                          .Select(P => P.ErrorMessage);
            //        return new BadRequestObjectResult(new ApiValdiationErrorResponse() { Errors = errors });
            //    });
            //});

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
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //app.UseStatusCodePagesWithReExecute("/Errors/{0}");
            app.UseStatusCodePagesWithReExecute("/Errors/{0}");

            app.UseStaticFiles();


            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
