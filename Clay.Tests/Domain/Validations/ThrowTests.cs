using Clay.Domain.DomainObjects;
using Clay.Domain.Validations;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace Clay.Tests.Domain.Validations
{
    public class ThrowTests
    {
        [Theory]
        [InlineData("", "Empty value.")]
        [InlineData(null, "Null value.")]
        [InlineData(" ", "Whitespace value.")]
        public void Should_IfArgumentIsNullOrWhitespace_Throw_Exception(string value, string message)
        {
            Assert.Throws<DomainException>(() => Throw.IfArgumentIsNullOrWhitespace(value, message)).Message.Should().Be(message);
        }

        [Fact]
        public void Should_IfArgumentIsNullOrWhitespace_Not_Throw_Exception()
        {
            var message = "messsage";
            var action = () => Throw.IfArgumentIsNullOrWhitespace("value", message);

            action.Should().NotThrow<DomainException>();
        }

        [Theory]
        [InlineData("", "", "Equal empty values.")]
        [InlineData("123", "123", "Equal filled values.")]
        public void Should_IfAssertArgumentsAreEqual_Throw_Exception(string value1, string value2, string message)
        {
            Assert.Throws<DomainException>(() => Throw.IfAssertArgumentsAreEqual(value1, value2, message)).Message.Should().Be(message);
        }

        [Fact]
        public void Should_IfAssertArgumentsAreEqual_Not_Throw_Exception()
        {
            var message = "messsage";
            var action = () => Throw.IfAssertArgumentsAreEqual("value1", "value2", message);

            action.Should().NotThrow<DomainException>();
        }

        [Theory]
        [InlineData("", " ", "Different empty values.")]
        [InlineData("123", "321", "Different values.")]
        public void Should_IfAssertArgumentsAreNotEqual_Throw_Exception(string value1, string value2, string message)
        {
            Assert.Throws<DomainException>(() => Throw.IfAssertArgumentsAreNotEqual(value1, value2, message)).Message.Should().Be(message);
        }

        [Fact]
        public void Should_IfAssertArgumentsAreNotEqual_Not_Throw_Exception()
        {
            var message = "messsage";
            var action = () => Throw.IfAssertArgumentsAreNotEqual("value1", "value1", message);

            action.Should().NotThrow<DomainException>();
        }

        [Fact]
        public void Should_IfArgumentIsTrue_Throw_Exception()
        {
            var errorMessage = "True value";
            Assert.Throws<DomainException>(() => Throw.IfArgumentIsTrue(true, errorMessage)).Message.Should().Be(errorMessage);
        }

        [Fact]
        public void Should_IfArgumentIsTrue_Not_Throw_Exception()
        {
            var errorMessage = "True value";
            var action = () => Throw.IfArgumentIsTrue(false, errorMessage);

            action.Should().NotThrow<DomainException>();
        }

        [Fact]
        public void Should_IfArgumentIsFalse_Throw_Exception()
        {
            var errorMessage = "False value";
            Assert.Throws<DomainException>(() => Throw.IfArgumentIsFalse(false, errorMessage)).Message.Should().Be(errorMessage);
        }

        [Fact]
        public void Should_IfArgumentIsFalse_Not_Throw_Exception()
        {
            var errorMessage = "False value";
            var action = () => Throw.IfArgumentIsFalse(true, errorMessage);

            action.Should().NotThrow<DomainException>();
        }

        [Theory]
        [InlineData("email", "No @ for e-mail.")]
        [InlineData("@email.com", "No value before @.")]
        [InlineData("email@@email.com", "Two @s.")]
        [InlineData("email@.com.", "Missing domain.")]
        [InlineData(".@email.com.", "invalid e-mail.")]
        public void Should_IfArgumentIsInvalidEmail_Throw_Exception(string email, string message)
        {
            Assert.Throws<DomainException>(() => Throw.IfArgumentIsInvalidEmail(email, message)).Message.Should().Be(message);
        }
    }
}
