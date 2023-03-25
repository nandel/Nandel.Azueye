using Discord;
using Discord.WebSocket;
using Handlers.Discord.Services;
using Microsoft.Extensions.DependencyInjection;
using Nandel.Modules;

namespace Handlers.Discord;

public class DiscordHandlersModule : IModule, IHasStart
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMediatR(opt => opt.RegisterServicesFromAssemblyContaining<DiscordHandlersModule>());
    }

    public async Task StartAsync(IServiceProvider services, CancellationToken cancellationToken)
    {
        var client = new DiscordSocketClient();
        var token = "MTA4OTAxNzc1MTM0MDg1MTI0MA.GmYG8Q.UpAast2_mCxkzRy_V5CSt6_F97Ojzz5-NQ2Mtk";

        await client.LoginAsync(TokenType.Bot, token);
        await client.StartAsync();

        DiscordSocketClientAcessor.Instance = client;
    }
}