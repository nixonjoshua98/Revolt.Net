using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Revolt.Net.Commands;
using Revolt.Net.TestBot;
using Revolt.Net.WebSocket;

var client = new RevoltSocketClient(token: GetBotToken());

var commands = new CommandService();

var services = new ServiceCollection();

var provider = services.BuildServiceProvider();

await CommandHandler.SetupAsync(client, commands, provider);

AddLoggingCallbacks(client, commands);

await client.RunAsync();

static void AddLoggingCallbacks(RevoltSocketClient client, CommandService commands)
{
    client.Log += async message => Console.WriteLine($"{message.Message} | {message.Exception}");
    commands.Log += async message => Console.WriteLine($"{message.Message} | {message.Exception}");
}

static string GetBotToken()
{
    var config = new ConfigurationBuilder()
        .AddUserSecrets<Program>()
        .Build();

    return config["TestBotToken"]!;
}