using Microsoft.Extensions.Configuration;
using Revolt.Commands;
using Revolt.Net.Clients;
using Revolt.Net.TestBot;

var secrets = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

var client = new RevoltBotClient(token: secrets["TestBotToken"]!);

var commands = new CommandService(new CommandServiceConfig());
await commands.AddModuleAsync<HelloWorldModule>(null!);

client.Message.Add(async e =>
{
    var ctx = new RevoltCommandContext(e.Message);

    var result = await commands.ExecuteAsync(ctx, 0, null!, MultiMatchHandling.Best);
});

try
{
    await client.RunAsync();
}
finally
{
    await client.LogoutAsync();
}