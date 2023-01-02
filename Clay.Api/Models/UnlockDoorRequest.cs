using System.Diagnostics.CodeAnalysis;

namespace Clay.Api.Models
{
    [ExcludeFromCodeCoverage]
    public class UnlockDoorRequest
    {
        public string TagIdentification { get; set; }
    }
}
