using Clay.Application.Interfaces.Repositories;

namespace Clay.Application.Services
{
    public abstract class ApplicationBaseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationBaseService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("UnitOfWork");

            _unitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork;
            }
        }
    }
}
