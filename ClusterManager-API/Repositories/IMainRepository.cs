using ClusterManager_API.Models;

namespace ClusterManager_API.Repositories
{
    public interface IMainRepository
    {
        Task<ClusterConfig> LoadDataIntoMemory();
    }
}
