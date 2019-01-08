using System.Threading.Tasks;

namespace Esfa.Recruit.Subscriptions.Infrastructure
{
    public interface IResultsCacheRepository
    {
        Task Get(string rowKey);
    }
}