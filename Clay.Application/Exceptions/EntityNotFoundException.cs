using System.Runtime.Serialization;

namespace Clay.Application.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() : base("Entry not found.") 
        {
        }

        public EntityNotFoundException(string? message) : base(message)
        {
        }
    }
}