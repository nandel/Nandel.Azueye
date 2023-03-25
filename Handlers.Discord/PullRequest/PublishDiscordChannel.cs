using Core.Events;
using Discord;
using Handlers.Discord.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Handlers.Discord.PullRequest;

public class PublishDiscordChannel : INotificationHandler<PullRequestEvent>
{
    private readonly ILogger<PublishDiscordChannel> _logger;

    public PublishDiscordChannel(ILogger<PublishDiscordChannel> logger)
    {
        _logger = logger;
    }

    public async Task Handle(PullRequestEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Notificação de {EventType} recebida com o payload {Payload}",
            nameof(PullRequestEvent), notification);      
        
        // Create a new Discord message
        var client = DiscordSocketClientAcessor.Instance;
        var message = 
            $"New pull request created:\n" +
            $"Title: {notification.Title}\n" +
            $"Description: {notification.Description}\n" +
            $"Author: {notification.Author}\n" +
            $"Repository: {notification.Repository}\n" +
            $"Project: {notification.Project}\n" +
            $"Is Draft: {notification.IsDraft}";

        // Send the message to a Discord channel
        ulong channelId = 1088983690249515128; // Replace with your channel ID
        var channel = client.GetChannel(channelId) as IMessageChannel;
        await channel.SendMessageAsync(message);
    }
}