
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()
        .Build();

Console.WriteLine(config);