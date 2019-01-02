using System.Threading.Tasks;

namespace Esfa.Recruit.Subscriptions.Api.Repositories
{
    public interface IResultsCacheRepository
    {
        Task Get(string rowKey);
    }
}