using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

using Serilog;
using FluentValidation;
using MediatR;

using Presentation.Common.Behaviors;

using Application;
using Domain;

using Infrastructure;
using Infrastructure.Persistence;

using Shared.Configurations;
using Shared.Common;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddDbContext<AppDbContext>(options =>
    options
        .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine, LogLevel.Information)
);

builder.Services
    .AddApplication()
    .AddDomain()
    .AddInfrastructure(builder.Configuration);

// Dynamically register interface-implementation pairs across assemblies (e.g., IMemberRepository -> MemberRepository)
var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies()
    .Where(x => !x.IsDynamic && !string.IsNullOrEmpty(x.FullName))
    .ToList();

var projectAssemblies = loadedAssemblies
    .Where(x => x.FullName!.StartsWith("Microservices"))
    .ToList();

var allInterfaces = projectAssemblies
    .SelectMany(x => x.GetTypes())
    .Where(x => x.IsInterface)
    .ToList();

var allImplementations = projectAssemblies
    .SelectMany(x => x.GetTypes())
    .Where(x => x.IsClass && !x.IsAbstract && !x.IsGenericTypeDefinition)
    .ToList();

foreach (var impl in allImplementations)
{
    var iface = allInterfaces.FirstOrDefault(x => x.Name == $"I{impl.Name}");
    if (iface != null)
    {
        builder.Services.AddScoped(iface, impl);
        Console.WriteLine($"[DI] Registered {iface.FullName} => {impl.FullName}");
    }
}

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<AppSettings>(builder.Configuration);
builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<AppSettings>>().Value);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();
builder.Services.AddAuthorization();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var response = new ResponseModel
        {
            status = new StatusResponseModel
            {
                statusCode = HttpStatusCode.BadRequest,
                timestamp = DateTime.UtcNow
            }
        };

        return new BadRequestObjectResult(response);
    };
});

var assemblies = AppDomain.CurrentDomain.GetAssemblies();

builder.Services.AddValidatorsFromAssemblies(assemblies);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Loyalty Program", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token."
    };

    c.AddSecurityDefinition("Bearer", securityScheme);
});  // เพิ่ม Swagger

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Lifetime.ApplicationStarted.Register(() =>
{
    var server = app.Services.GetRequiredService<IServer>();
    var addresses = server.Features.Get<IServerAddressesFeature>();

    if (addresses != null)
    {
        foreach (var address in addresses.Addresses)
        {
            Console.WriteLine($"🚀 Application is running on: {address}");
        }
    }
});

try
{
    Log.Information("Starting Products API");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Product API terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
