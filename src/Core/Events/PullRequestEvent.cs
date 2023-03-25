using MediatR;

namespace Core.Events;

public class PullRequestEvent : INotification
{
    public string EventType { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
    public int PullRequestId { get; set; }
    public string Repository { get; set; }
    public string Project { get; set; }
    public bool IsDraft { get; set; }
}