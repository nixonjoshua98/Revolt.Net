using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Revolt.Net.Commands.Extensions;
using Revolt.Net.Core.Extensions;
using Revolt.Net.Rest.Extensions;
using Revolt.Net.Samples.BasicCommands;
using Revolt.Net.WebSocket.Extensions;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.AddConsole();

builder.Logging.SetMinimumLevel(LogLevel.Debug);

builder.Services.AddRevolt(cfg => cfg
    .AddRestClient("https://api.revolt.chat/", builder.Configuration["BOT_TOKEN"] ?? throw new Exception("Missing BOT_TOKEN"))
    .AddWebSocket()
    .AddCommands(cmd => cmd
        .AddModulesFromAssemblyContaining<Program>()
        .AddCheckHandlersFromAssemblyContaining<Program>()
    )
);

builder.Services
    .AddHostedService<CommandMessageHandler>();

var app = builder.Build();

await app.RunAsync();