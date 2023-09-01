using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Revolt.Net.Client;
using Revolt.Net.TestBot;

var secrets = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

var services = new ServiceCollection();

var client = new RevoltClient(token: secrets["TestBotToken"]!);

var commands = new CommandService();

client.Log += async message => Console.WriteLine($"{message.Message} | {message.Exception}");

commands.Log += async message => Console.WriteLine($"{message.Message} | {message.Exception}");

var provider = services.BuildServiceProvider();

await CommandHandler.SetupAsync(client, commands, provider);

try
{
    await client.RunAsync();
}
finally
{
    await client.LogoutAsync();
}