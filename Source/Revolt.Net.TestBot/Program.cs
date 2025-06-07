using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Revolt.Net.Commands.Handlers;
using Revolt.Net.Commands.Hosting.Extensions;
using Revolt.Net.Hosting.Extensions;
using Revolt.Net.Rest.Hosting.Extensions;
using Revolt.Net.WebSocket.Hosting.Extensions;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.AddConsole();

builder.Logging.SetMinimumLevel(LogLevel.Debug);

builder.Services.AddRevolt(cfg => cfg
    .AddRestClient("https://api.revolt.chat/", builder.Configuration["BOT_TOKEN"]!)
    .AddWebSocket()
    .AddCommandHandler<CommandMessageHandler>()
);

var app = builder.Build();

await app.RunAsync();