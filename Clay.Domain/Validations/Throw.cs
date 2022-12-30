using Clay.Domain.Core.DomainObjects;
using System.Text.RegularExpressions;

namespace Clay.Domain.Validations
{
    public static class Throw
    {
        public static void IfArgumentIsNullOrEmpty(object object1, string message)
        {
            if (object1 == null)
                throw new DomainException($"{nameof(object1)}: {message}");
        }

        public static void IfArgumentIsNullOrEmpty(string parameter, string message)
        {
            if (parameter == null || string.IsNullOrEmpty(parameter))
                throw new DomainException(message);
        }

        public static void IfAssertArgumentsAreNotEquals(object object1, object object2, string message)
        {
            if (!object1.Equals(object2))
                throw new DomainException($"{nameof(object1)}/{object1}: {message}");
        }

        public static void AssertArgumentFalse(bool boolValue, string message)
        {
            if (boolValue)
                throw new DomainException($"{nameof(boolValue)}: {message}");
        }

        public static void AssertArgumentLength(string stringValue, int maximum, string message)
        {
            int length = stringValue.Trim().Length;

            if (length > maximum)
                throw new DomainException($"{stringValue}/{maximum}: {message}");
        }

        public static void AssertArgumentLength(string stringValue, int minimum, int maximum, string message)
        {
            int length = stringValue.Trim().Length;

            if (length < minimum || length > maximum)
                throw new DomainException($"{stringValue}/{minimum}/{maximum}: {message}");
        }

        public static void AssertArgumentMatches(string pattern, string stringValue, string message)
        {
            var regex = new Regex(pattern);

            if (!regex.IsMatch(stringValue))
                throw new DomainException(message);
        }

        public static void AssertArgumentNotEmpty(string stringValue, string message)
        {
            if (string.IsNullOrEmpty(stringValue))
                throw new DomainException(message);
        }

        public static void IfAssertArgumentsAreEqual(object object1, object object2, string message)
        {
            if (object1.Equals(object2))
                throw new DomainException(message);
        }

        public static void AssertArgumentRange(double value, double minimum, double maximum, string message)
        {
            if (value < minimum || value > maximum)
                throw new DomainException($"{value}: {message}");
        }

        public static void AssertArgumentLessThanOrEqual(int value, string message)
        {
            if (value <= 0)
                throw new DomainException(message);
        }

        public static void AssertArgumentLessThan(int value, int minimum, string message)
        {
            if (value <= minimum)
                throw new DomainException(message);
        }

        public static void AssertArgumentRange(float value, float minimum, float maximum, string message)
        {
            if (value < minimum || value > maximum)
                throw new DomainException(message);
        }

        public static void AssertArgumentRange(int value, int minimum, int maximum, string message)
        {
            if (value < minimum || value > maximum)
                throw new DomainException(message);
        }

        public static void AssertArgumentRange(long value, long minimum, long maximum, string message)
        {
            if (value < minimum || value > maximum)
                throw new DomainException(message);
        }

        public static void AssertArgumentTrue(bool boolValue, string message)
        {
            if (!boolValue)
                throw new DomainException(message);
        }

        public static void AssertStateFalse(bool boolValue, string message)
        {
            if (boolValue)
                throw new DomainException(message);
        }

        public static void AssertStateTrue(bool boolValue, string message)
        {
            if (!boolValue)
                throw new DomainException(message);
        }
    }
}
