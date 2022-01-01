using AutoMapper;
using FitnessManager.BusinessLogic.Common.Interfaces;
using FitnessManager.BusinessLogic.FitnessClass.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.DataAccess.Repositories.Interfaces;

namespace FitnessManager.BusinessLogic.FitnessClass
{
    public class FitnessClassService : IFitnessClassService
    {
        private readonly IBaseRepository<FitnessClassEntity> _fitnessClassRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FitnessClassService(IBaseRepository<FitnessClassEntity> fitnessClassRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _fitnessClassRepository = fitnessClassRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}