using FitnessManager.DataAccess.Entities;

namespace FitnessManager.BusinessLogic.Common.Interfaces
{
    public interface IWebTokenGenerator
    {
        string CreateToken(UserEntity user, string role);
    }
}