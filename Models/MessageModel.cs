namespace Botrix.Models
{
    public class MessageModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
        public string Type { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
