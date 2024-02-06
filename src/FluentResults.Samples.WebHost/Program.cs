using SlugEnt.FluentResults.Extensions.AspNetCore;
using SlugEnt.FluentResults.Samples.WebHost.Controllers;

namespace SlugEnt.FluentResults.Samples.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AspNetCoreResult.Setup(config => config.DefaultProfile = new CustomAspNetCoreResultEndpointProfile());
                
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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