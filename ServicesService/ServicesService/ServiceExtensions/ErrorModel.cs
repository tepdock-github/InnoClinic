using System.Text.Json;

namespace ServicesService.ServiceExtensions
{
    public class ErrorMOdel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
