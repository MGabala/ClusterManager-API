using ClusterManager_API.Models;
using ClusterManager_API.Repositories;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetMachines(string clusterName, string host)
        {
            return NotFound("Method is not implemented.");
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
