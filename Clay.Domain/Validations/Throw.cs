using Clay.Domain.DomainObjects.Exceptions;
using System.Net.Mail;

namespace Clay.Domain.Validations
{
    public static class Throw
    {
        public static void IfArgumentIsNullOrWhitespace(string parameter, string message)
        {
            if (parameter is null || string.IsNullOrWhiteSpace(parameter))
                throw new DomainException(message);
        }

        public static void IfAssertArgumentsAreEqual(object object1, object object2, string message)
        {
            if (object1.Equals(object2))
                throw new DomainException(message);
        }

        public static void IfAssertArgumentsAreNotEqual(object object1, object object2, string message)
        {
            if (!object1.Equals(object2))
                throw new DomainException($"{message}");
        }

        public static void IfArgumentIsTrue(bool boolValue, string message)
        {
            if (boolValue)
                throw new DomainException($"{message}");
        }

        public static void IfArgumentIsFalse(bool boolValue, string message)
        {
            if (!boolValue)
                throw new DomainException(message);
        }

        public static void IfArgumentIsInvalidEmail(string value, string message)
        {
            if (!MailAddress.TryCreate(value, value, out var _))
                throw new DomainException(message);
        }
    }
}
