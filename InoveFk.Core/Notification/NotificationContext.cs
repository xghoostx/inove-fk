using InoveFk.Core.Error;
using System.Net;

namespace InoveFk.Core.Notification;

public class NotificationContext
{
    private readonly List<ErrorResponse> _notifications;
    public IReadOnlyCollection<ErrorResponse> Notifications => _notifications;
    public bool HasNotifications => _notifications.Any();

    public NotificationContext() =>
        _notifications = new List<ErrorResponse>();

    public void AddNotification(string key, string message, string statusCode) =>
        _notifications.Add(new ErrorResponse(key, message, statusCode));

    public void AddNotification(ErrorResponse notification) =>
        _notifications.Add(notification);

    public void AddNotifications(IReadOnlyCollection<ErrorResponse> notifications) =>
        _notifications.AddRange(notifications);

    public void AddNotifications(IEnumerable<ErrorResponse> notifications) =>
        _notifications.AddRange(notifications);

    public void AddNotifications(IEnumerable<KeyValuePair<string, bool>> notifications)
    {
        foreach (var error in notifications)
        {
            _notifications.Add(new ErrorResponse(
                key: Constants.SpecificationErroMessage,
                message: error.Key,
                statusCode: HttpStatusCode.BadRequest.ToString()));
        }
    }
}

