using Renci.SshNet;
using TunnelConnector.Protocls.Generic;

namespace TunnelConnector.Protocls.SSH
{
    public interface ISshConnector: IProtocolClient
    {
        SshClient GetClient();
    }
}