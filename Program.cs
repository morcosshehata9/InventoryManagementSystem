
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Sevices.ServiceImplementation;
using InventoryManagementSystem.Sevices.ServiceInterface;
using InventoryManagementSystem.UOW;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem
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

            builder.Services.AddDbContext<AppDbContext> (options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("CS"));
            });

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IInventoryTransactionService, InventoryTransactionService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
