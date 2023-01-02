using Clay.Domain.Aggregates.Door;
using Clay.Domain.DomainObjects.Exceptions;

namespace Clay.Tests.Domain.Aggregates.Door
{
    public class RoleTests
    {
        [Fact]
        public void Should_Create_Valid_Entity()
        {
            var roleEntity = Role.Create("Test Role");

            roleEntity.Should().NotBeNull();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Should_Fail_To_Create_Valid_Entity(string name)
        {
            var action = () => Role.Create(name);

            action.Should().Throw<DomainException>().WithMessage("The parameter Name is required.");
        }
    }
}
