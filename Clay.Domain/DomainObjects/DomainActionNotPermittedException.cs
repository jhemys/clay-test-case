namespace Clay.Domain.DomainObjects
{
    public class DomainActionNotPermittedException : Exception
    {
        public DomainActionNotPermittedException()
        {
        }

        public DomainActionNotPermittedException(string message) : base(message)
        {
        }

        public DomainActionNotPermittedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
