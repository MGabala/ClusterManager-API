using ClusterManager_API.Models;
using Newtonsoft.Json;

namespace ClusterManager_API.Repositories
{
    public class MainRepository : IMainRepository
    {
        public async Task<ClusterConfig> LoadDataIntoMemory()
        {
            string clusterConfigPath = "ClusterConfig.json";
            string json = await System.IO.File.ReadAllTextAsync(clusterConfigPath);
            ClusterConfig clusterConfig = JsonConvert.DeserializeObject<ClusterConfig>(json);
            return clusterConfig!;
        }
    }
}
