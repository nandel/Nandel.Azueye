using Core.Events;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApi.Controllers;

[ApiController]
[Route("api/webhook")]
public class WebhookController : ControllerBase
{
    private readonly IMediator _mediator;

    public WebhookController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Consumes("text/plain")]
    public async Task<IActionResult> HandleWebhook(CancellationToken cancellationToken)
    {
        string requestBody;
        using (var reader = new StreamReader(Request.Body))
        {
            requestBody = await reader.ReadToEndAsync(cancellationToken);
        }
        
        var eventData = JsonConvert.DeserializeObject<dynamic>(requestBody);
        string eventType = eventData.eventType;

        if (IsPullRequestEvent(eventType))
        {
            var pullRequestEvent = ParsePullRequestEvent(eventData);
            await _mediator.Publish(pullRequestEvent, cancellationToken);
        }

        return Ok();
    }
    
    private static bool IsPullRequestEvent(string eventType)
    {
        return eventType == "git.pullrequest.created" || eventType == "git.pullrequest.updated" || eventType == "git.pullrequest.completed";
    }
    
    private static PullRequestEvent ParsePullRequestEvent(dynamic eventData)
    {
        string title = eventData.resource.title;
        string description = eventData.resource.description;
        string author = eventData.resource.createdBy.displayName;
        int pullRequestId = eventData.resource.pullRequestId;
        string repository = eventData.resource.repository.name;
        string project = eventData.resource.repository.project.name;
        bool isDraft = (eventData.resource.status == "draft");

        // Create a new PullRequestEvent object and populate it with relevant data
        var pullRequestEvent = new PullRequestEvent()
        {
            EventType = eventData.eventType,
            Title = title,
            Description = description,
            Author = author,
            PullRequestId = pullRequestId,
            Repository = repository,
            Project = project,
            IsDraft = isDraft
        };

        return pullRequestEvent;
    }
}