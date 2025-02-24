using Crayon.Cloud.Sales.Application.Contracts;
using Crayon.Cloud.Sales.Application.Services;
using Crayon.Cloud.Sales.Integration.Clients;
using Crayon.Cloud.Sales.Integration.ContextDB;
using Crayon.Cloud.Sales.Integration.Contracts;
using Crayon.Cloud.Sales.Integration.Repositories;
using Crayon.Cloud.Sales.Integration.SwaggerRequestExamples;
using Crayon.Cloud.Sales.WebAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //register services and repos
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

        builder.Services.AddScoped<ICCPClient, CCPClient>();
        builder.Services.AddScoped<ISoftwareService, SoftwareService>();
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Cloud Sales API",
                Version = "v1"
            });
            options.ExampleFilters();
            options.OperationFilter<SwaggerRouteParameterExampleFilter>();
        });
        builder.Services.AddSwaggerExamplesFromAssemblyOf<ProvisionSubscriptionDTOExample>();
        builder.Services.AddSwaggerExamplesFromAssemblyOf<ExtendSubscriptionValidDateDTOExample>();
        builder.Services.AddSwaggerExamplesFromAssemblyOf<ChangeSubscriptionQuantityDTOExample>();

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<MockAuthenticationMiddleware>();
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            DbInitializer.Initialize(context);
        }
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}