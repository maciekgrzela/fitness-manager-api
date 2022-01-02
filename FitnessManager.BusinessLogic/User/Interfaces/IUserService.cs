using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.User;

namespace FitnessManager.BusinessLogic.User.Interfaces
{
    public interface IUserService
    {
        Task<BusinessLogicResponse<UserEntity>> UpdateAsync(string id, UpdateUserDto dto);
        Task<BusinessLogicResponse<UserEntity>> DeleteAsync(string id);
    }
}