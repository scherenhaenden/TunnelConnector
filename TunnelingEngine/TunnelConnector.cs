using System;
using Renci.SshNet;
using TunnelConnector.Protocls.Generic;

namespace TunnelConnector.TunnelingEngine
{
    public class TunnelCreator: ITunnelCreator
    {
        private readonly IProtocolClient _protocolClient;
        private readonly string _localhost;
        private readonly int _portToBoundInLocalhost;
        private readonly string _foreignHost;
        private readonly int _portInForeignHost;
        private ForwardedPortLocal _forwardedPortLocal;
        
        private bool _connected = false;


        public TunnelCreator(IProtocolClient protocolClient, string localhost, int portToBoundInLocalhost
            , string foreignHost, int portInForeignHost)
        {
            _protocolClient = protocolClient;
            _localhost = localhost;
            _portToBoundInLocalhost = portToBoundInLocalhost;
            _foreignHost = foreignHost;
            _portInForeignHost = portInForeignHost;
        }

        public void InitiateTunneling()
        {
            if (!_protocolClient.IsConnected())
            {
                _protocolClient.Connect();
            }
            var localPort = Convert.ToUInt32(_portToBoundInLocalhost);
            var foreignPort = Convert.ToUInt32(_portInForeignHost);
            
            
            _forwardedPortLocal = new ForwardedPortLocal(_localhost,
                localPort,
                _foreignHost,
                foreignPort);
            _protocolClient.AddForwardedPort(_forwardedPortLocal);
            _forwardedPortLocal.Start();
            _connected = true;
        }

        public int GetCurrentForwardPort()
        {
            return _portToBoundInLocalhost;
        }

        public bool IsConnected()
        {
            return _connected;
        }

        public void StopTunneling()
        {
            _forwardedPortLocal.Stop();
            _connected = false;
        }
        
    }
}