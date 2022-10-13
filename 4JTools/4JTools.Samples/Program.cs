
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()
        .Build();

services.Configure<DocuSignWebhookAuthOptions>
    (Configuration
        .GetSection(
            DocuSignWebhookAuthOptions
            .DocuSignWebhookAuth));

Console.WriteLine(config);