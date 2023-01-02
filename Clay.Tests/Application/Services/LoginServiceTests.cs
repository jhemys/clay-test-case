using Clay.Application.DTOs;
using Clay.Application.Exceptions;
using Clay.Application.Interfaces.Repositories;
using Clay.Application.Services;
using Clay.Domain.Aggregates.Employee;
using Clay.Domain.Aggregates.Login;
using Clay.Domain.DomainObjects.Exceptions;

namespace Clay.Tests.Application.Services
{
    public class LoginServiceTests
    {
        [Fact]
        public async Task GetByEmailAndPassword_Should_Fail_With_Invalid_Entry()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new LoginService(unitOfWork);
            var entity = PrepareLoginData(true);
            A.CallTo(() => unitOfWork.LoginRepository.GetByEmailAndPassword(A<string>._, A<string>.Ignored)).Returns(entity);

            var action = () => service.GetByEmailAndPassword("test@email.com", "123456");

            await action.Should().ThrowAsync<EntityNotFoundException>().WithMessage("Entry not found.");
        }

        [Fact]
        public async Task GetByEmailAndPassword_Should_Return_Data()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new LoginService(unitOfWork);
            var entity = PrepareLoginData();
            A.CallTo(() => unitOfWork.LoginRepository.GetByEmailAndPassword(A<string>._, A<string>.Ignored)).Returns(entity);

            var data = await service.GetByEmailAndPassword("test@email.com", "123456");

            data.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateLogin_Should_Fail_With_Invalid_Entry()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new LoginService(unitOfWork);
            var login = new LoginDTO()
            {
                Email = "email@email.com",
                Password = "password",
                Role = "Role",
            };

            var action = async () => await service.CreateLogin(login);

            await action.Should().ThrowAsync<DomainException>().WithMessage("The parameter Name is required.");
            A.CallTo(() => unitOfWork.LoginRepository.AddAsync(A<Login>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => unitOfWork.CommitAsync()).MustNotHaveHappened();
        }

        [Fact]
        public async Task CreateLogin_Should_Create_Login()
        {
            var unitOfWork = A.Fake<IUnitOfWork>();
            var service = new LoginService(unitOfWork);
            var login = new LoginDTO()
            {
                Name = "Test",
                Email = "email@email.com",
                Password = "password",
                Role = "Role",
                PermissionType = "FullAccess",
                TagIdentification = "1234"
            };

            await service.CreateLogin(login);

            A.CallTo(() => unitOfWork.LoginRepository.AddAsync(A<Login>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => unitOfWork.CommitAsync()).MustHaveHappenedOnceExactly();
        }

        private Login? PrepareLoginData(bool createNullObject = false)
        {
            if (createNullObject)
                return null;

            var employee = PrepareEmployeeData();

            return Login.Create("email@email.com", "123456", employee);
        }

        private Employee? PrepareEmployeeData(bool createNullObject = false)
        {
            return Employee.Create("Name", "Role");
        }
    }
}
