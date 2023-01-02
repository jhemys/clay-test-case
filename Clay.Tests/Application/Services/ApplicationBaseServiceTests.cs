using Clay.Application.Interfaces.Repositories;
using Clay.Application.Services;

namespace Clay.Tests.Application.Services
{
    public class ApplicationBaseServiceTests
    {
        [Fact]
        public void Should_Fail_To_Create_Service()
        {
            var action = () =>  new FakeService(null);

            action.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'UnitOfWork')");
        }

        [Fact]
        public void Should_Create_Service()
        {
            var service = new FakeService(A.Fake<IUnitOfWork>());

            service.UnitOfWork.Should().NotBeNull();
        }
    }

    internal class FakeService : ApplicationBaseService
    {
        internal FakeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
