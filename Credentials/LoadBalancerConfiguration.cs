using TunnelConnector.Protocls;

namespace TunnelConnector.Credentials
{
    public class LoadBalancerConfiguration
    {
        public string Localhost { get; init; }
        public int [] LocalPorts { get; init; }
        public string ForeignHost { get; init; }
        public int ForeignPort { get; init; }
        public string User { get; init; }
        public string Password { get; init; }
        public int ProtocolPort{ get; init; }
        public AvailableProtocols CurrentProtocol { get; init; }
    }
}