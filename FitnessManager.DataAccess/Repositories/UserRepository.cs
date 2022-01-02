using FitnessManager.DataAccess.Context;
using FitnessManager.DataAccess.Repositories.Interfaces;

namespace FitnessManager.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
    }
}