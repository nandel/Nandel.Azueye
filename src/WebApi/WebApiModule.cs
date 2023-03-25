using Handlers.Discord;
using Nandel.Modules;

namespace WebApi;

[DependsOn(
    typeof(DiscordHandlersModule))]
public class WebApiModule : IModule
{
    public void ConfigureServices(IServiceCollection services)
    {
        // null
    }
}