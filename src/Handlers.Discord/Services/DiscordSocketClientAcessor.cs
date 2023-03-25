using Discord.WebSocket;

namespace Handlers.Discord.Services;

public static class DiscordSocketClientAcessor
{
    public static DiscordSocketClient Instance { get; set; }
}