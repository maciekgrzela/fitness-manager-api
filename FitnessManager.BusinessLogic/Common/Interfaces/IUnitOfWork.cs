using System.Threading.Tasks;

namespace FitnessManager.BusinessLogic.Common.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitTransactionsAsync();
    }
}