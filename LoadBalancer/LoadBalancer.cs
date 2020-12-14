using System.Collections.Generic;
using TunnelConnector.Protocls.SSH;
using TunnelConnector.TunnelingEngine;

namespace TunnelConnector.LoadBalancer
{
    public class LoadBalancer: ILoadBalancer
    {
        private readonly string _localhost;
        private readonly int[] _localPorts;
        private readonly string _foreignHost;
        private readonly int _foreignPort;
        private readonly string _user;
        private readonly string _password;
        private readonly int _protocolPort;
        private readonly string _protocol;
        private List<ITunnelCreator> _tunnelCreators;
        private bool _initiated = false;

        private int LastUseId = 0;

        public LoadBalancer(string localhost, int [] localPorts, string foreignHost, int foreignPort, string user, string password, int protocolPort, string protocol = "SSH")
        {
            _localhost = localhost;
            _localPorts = localPorts;
            _foreignHost = foreignHost;
            _foreignPort = foreignPort;
            _user = user;
            _password = password;
            _protocolPort = protocolPort;
            _protocol = protocol;
        }


        public void Initiate()
        {
            _tunnelCreators = new List<ITunnelCreator>();
            foreach (var localport in _localPorts)
            {
                //IProtocolClient sshConnector;
                
                if (_protocol == "SSH")
                {
                    var sshConnector = new SshConnector(_foreignHost, _protocolPort, _user, _password, null);
                    ITunnelCreator tunnelCreator = new TunnelCreator(sshConnector
                        , _localhost,localport , _foreignHost, _foreignPort );
                    _tunnelCreators.Add(tunnelCreator);
                }
            }

            _initiated = true;
        }

        public bool Initiated()
        {
            return _initiated;
        }

        public int RunClientAndGetActivePort()
        {
            if (_tunnelCreators == null)
            {
                Initiate();
            }

            var countOfConnectors = _tunnelCreators.Count;

            LastUseId ++ ;
            if (countOfConnectors <= LastUseId)
            {
                LastUseId = 0;
            }
            if (!_tunnelCreators[LastUseId].IsConnected())
            {
                _tunnelCreators[LastUseId].InitiateTunneling();
            }
            return _tunnelCreators[LastUseId].GetCurrentForwardPort();
        }
    }
}