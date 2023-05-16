namespace ClusterManager_API.Models
{
    public class Machine
    {
        public string MachineId { get; set; }
    }

    public class Host
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Port { get; set; }
        public List<Machine> Machines { get; set; }
    }

    public class Cluster
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<Host> Hosts { get; set; }
    }

    public class ClusterConfig
    {
        public List<Cluster> Clusters { get; set; }
    }

}
