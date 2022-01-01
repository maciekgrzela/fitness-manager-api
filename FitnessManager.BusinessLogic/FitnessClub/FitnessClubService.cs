using AutoMapper;
using FitnessManager.BusinessLogic.Common.Interfaces;
using FitnessManager.BusinessLogic.FitnessClub.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.DataAccess.Repositories.Interfaces;

namespace FitnessManager.BusinessLogic.FitnessClub
{
    public class FitnessClubService : IFitnessClubService
    {
        private readonly IBaseRepository<FitnessClubEntity> _fitnessClubRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FitnessClubService(IBaseRepository<FitnessClubEntity> fitnessClubRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _fitnessClubRepository = fitnessClubRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}