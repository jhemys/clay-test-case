using Clay.Domain.DomainObjects;

namespace Clay.Tests.Domain.DomainObjects
{
    public class EntityTests
    {
        [Fact]
        public void Should_Create_Entity()
        {
            var entity = new FakeEntity();

            entity.Should().NotBeNull();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Should_SetIsActive(bool isActive)
        {
            var entity = new FakeEntity();

            entity.SetIsActive(isActive);

            entity.IsActive.Should().Be(isActive);
        }

        [Theory]
        [InlineData(1, 2, false)]
        [InlineData(1, 1, true)]
        public void Should_Equals_Compare_Entities_By_Id(int idFirstEntity, int idSecondEntity, bool areEqual)
        {
            var entity = new FakeEntity();
            var entity2 = new FakeEntity();

            entity.SetId(idFirstEntity);
            entity2.SetId(idSecondEntity);

            entity.Equals(entity2).Should().Be(areEqual);
        }

        [Fact]
        public void Should_Equals_Compare_Entities_By_Id_With_Null_Entity()
        {
            var entity = new FakeEntity();
            FakeEntity? entity2 = null;

            entity.Equals(entity2).Should().Be(false);
        }
    }

    public class FakeEntity : Entity
    {
        public FakeEntity() : base() { }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}
