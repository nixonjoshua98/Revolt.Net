using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Revolt.Net.Clients;
using Revolt.Net.Hosting;

var secrets = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

var builder = Host.CreateDefaultBuilder()
    .AddRevoltBotClient((context, config) =>
    {
        config.Token = secrets["TestBotToken"]!;
    });

var host = builder.Build();

var client = host.Services.GetRequiredService<RevoltClient>();

client.Message.Add(async e =>
{
    Console.WriteLine($"{e.Message.Author!.Username} | {e.Message.Content}");
});

host.Run();