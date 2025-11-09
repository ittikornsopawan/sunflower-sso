using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Migrator;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
Console.WriteLine($"Running migration in environment: {environment}");

var baseFile = "appsettings.json";
var envFile = $"appsettings.{environment}.json";

var configBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(baseFile, optional: false, reloadOnChange: true)
    .AddJsonFile(envFile, optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var config = configBuilder.Build();

var connStr = config.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connStr))
{
    Console.ForegroundColor = ConsoleColor.Red;

    // ตรวจสอบว่าไฟล์ไหนมีหรือไม่มี
    var baseFileExists = File.Exists(baseFile);
    var envFileExists = File.Exists(envFile);

    Console.WriteLine($"❌ Error: Connection string 'DefaultConnection' not found for environment '{environment}'.");

    if (!baseFileExists)
        Console.WriteLine($"   - File missing: {baseFile}");
    if (!envFileExists && environment != "Production")
        Console.WriteLine($"   - File missing: {envFile}");

    Console.ResetColor();
    return;
}

if (args.Length < 2)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("⚙️ Usage: dotnet run -- [up|down] [schema|seed]");
    Console.ResetColor();
    return;
}

var direction = args[0];
var type = args[1];

Console.WriteLine($"🚀 Starting migration: {direction} {type}");

var runner = new MigrationRunner(connStr);

try
{
    await runner.RunAsync(direction, type);
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("✅ Migration completed successfully.");
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"❌ Migration failed: {ex.Message}");
}
finally
{
    Console.ResetColor();
}