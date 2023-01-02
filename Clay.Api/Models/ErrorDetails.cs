using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Clay.Api.Models
{
    [ExcludeFromCodeCoverage]
    public class ErrorDetails
    {
        public int StatusCode { get; internal set; }
        public string Message { get; internal set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
