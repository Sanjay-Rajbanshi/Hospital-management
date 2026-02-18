using HIMS.Data;

using HIMS.Interfaces;
using HIMS.Services;
using Microsoft.EntityFrameworkCore;

namespace HIMS
{
    public partial class Program
    {
        
        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

          

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddScoped<AppointmentService>();

            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IStaffService, StaffsService>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });


            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
              //  app.MapOpenApi();
            }

           

            // Ensure CORS middleware runs before authorization and endpoint mapping.
            app.UseCors("AllowAngular");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync();
            app.MapFallbackToFile("index.html");
            await app.RunAsync(); 
        }
    }
}