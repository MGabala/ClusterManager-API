using ClusterManager_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClusterManager_API.Controllers
{
    [ApiController, Route("api/[controller]")]
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
        [HttpGet("GetMachines")]
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



        [HttpGet]
        [Route("{machineId}/snapshots")]
        public ActionResult<IEnumerable<string>> GetSnapshots(string clusterName, string host, string machineId)
        {
            return NotFound("Method is not implemented.");
        }

        [HttpGet]
        [Route("{machineId}/status")]
        public ActionResult<string> GetMachineStatus(string clusterName, string host, string machineId)
        {
            return NotFound("Method is not implemented.");
        }
    }
}
