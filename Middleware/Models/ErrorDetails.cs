namespace Middleware.Models
{
    public class ErrorDetails
    {
        public int Status { get; set; }
        public string Message { get; set; } = default!;

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
