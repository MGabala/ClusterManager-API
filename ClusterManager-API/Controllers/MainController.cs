using ClusterManager_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClusterManager_API.Controllers
{
    [ApiController, Route("api")]
    public class MainController : ControllerBase
    {
        private readonly IMainRepository _repository;
        public MainController(IMainRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Pobiera listę maszyn z określonego hosta w klastrze.
        /// </summary>
        /// <param name="clusterName">Nazwa klastra.</param>
        /// <param name="host">Nazwa hosta.</param>
        /// <returns>Lista identyfikatorów maszyn.</returns>
        [HttpGet("clusters/{clusterName}/{host}/machines")]
        public async Task<ActionResult<IEnumerable<string>>> GetMachines(string clusterName, string host)
        {
            var clusterConfig = await _repository.LoadDataIntoMemory();
            var cluster = clusterConfig.Clusters.FirstOrDefault(c => c.Name == clusterName);

            if (cluster != null && cluster.Hosts.Any(h => h.Name == host))
            {
                var machines = cluster.Hosts.FirstOrDefault(h => h.Name == host)?.Machines;
                var machineIds = machines!.Select(m => m.MachineId);
                return Ok(machineIds);
            }
            return BadRequest("Nie znaleziono klastra lub hosta.");
        }

        /// <summary>
        /// Pobiera listę snapshotów dla określonej maszyny w klastrze.
        /// </summary>
        /// <param name="clusterName">Nazwa klastra.</param>
        /// <param name="host">Nazwa hosta.</param>
        /// <param name="machineId">Identyfikator maszyny.</param>
        /// <returns>Lista snapshotów.</returns>
        [HttpGet]
        [Route("clusters/{clusterName}/host/machines/{machineId}/snapshots")]
        public async Task<ActionResult<IEnumerable<string>>> GetSnapshots(string clusterName, string host, string machineId)
        {
            var clusterConfig = await _repository.LoadDataIntoMemory();
            var cluster = clusterConfig.Clusters.FirstOrDefault(c => c.Name == clusterName);

            if (cluster != null && cluster.Hosts.Any(h => h.Name == host))
            {
                var machine = cluster.Hosts.FirstOrDefault(h => h.Name == host)?.Machines.FirstOrDefault(m => m.MachineId == machineId);

                if (machine != null)
                {
                    return new List<string> { "Snapshot1", "Snapshot2", "Snapshot3" };
                }
            }
            return BadRequest("Nie znaleziono klastra, hosta lub maszyny.");
        }

        /// <summary>
        /// Pobiera informacje o wykorzystaniu danej maszyny (CPU, dysków, ruch sieciowy itp.).
        /// </summary>
        /// <param name="clusterName">Nazwa klastra.</param>
        /// <param name="host">Nazwa hosta.</param>
        /// <param name="machineId">Identyfikator maszyny.</param>
        /// <returns>Informacje o wykorzystaniu maszyny.</returns>
        [HttpGet]
        [Route("clusters/{clusterName}/{host}/machines/{machineId}/status")]
        public async Task<ActionResult<object>> GetMachineStatus(string clusterName, string host, string machineId)
        {
            var clusterConfig = await _repository.LoadDataIntoMemory();
            var cluster = clusterConfig.Clusters.FirstOrDefault(c => c.Name == clusterName);

            if (cluster != null && cluster.Hosts.Any(h => h.Name == host))
            {
                var machine = cluster.Hosts.FirstOrDefault(h => h.Name == host)?.Machines.FirstOrDefault(m => m.MachineId == machineId);

                if (machine != null)
                {
                    var machineStatus = new
                    {
                        CPUUsage = "30% 4,05 Ghz",
                        RAM = "15,9/31,9GB (50%)",
                        DiskUsage = "60%",
                        Ethernet = "Wysył.: 1,4 Mb/s Odebr.: 344 Kb/s",
                        GPU = "6% (35°C)"
                    };

                    return machineStatus;
                }
            }
            return BadRequest("Nie znaleziono klastra, hosta lub maszyny.");
        }

    }
}
