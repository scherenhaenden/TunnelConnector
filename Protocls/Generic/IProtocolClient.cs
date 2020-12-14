using Renci.SshNet;

namespace TunnelConnector.Protocls.Generic
{
    public interface IProtocolClient
    {
        void CreateClient();

        void Connect();
        
        void AddForwardedPort(ForwardedPortLocal portForwarded);
        
        bool IsConnected();
    }
}