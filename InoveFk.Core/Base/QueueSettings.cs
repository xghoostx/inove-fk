namespace InoveFk.Core.Base;

public class QueueSettings
{
    public string Queue {  get; set; }
    public bool Durable { get; set; }
    public bool Exclusive { get; set; }
    public bool AutoDelete { get; set; }
    public IDictionary<string, object>? Arguments { get; set; }

    public QueueSettings(
        string name, bool durable, bool exclusive, bool autoDelete, IDictionary<string, object> arguments)
    {
        Queue = name;
        Durable = durable;
        Exclusive = exclusive;
        AutoDelete = autoDelete;
        Arguments = arguments;
    }

    public QueueSettings(string name, bool durable, bool exclusive, bool autoDelete) 
    {
        Queue = name;
        Durable = durable;
        Exclusive = exclusive;
        AutoDelete = autoDelete;
        Arguments = null;
    }
}
