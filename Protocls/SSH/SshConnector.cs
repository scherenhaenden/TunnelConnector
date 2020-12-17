using System;
using Renci.SshNet;
using TunnelConnector.Credentials;

namespace TunnelConnector.Protocls.SSH
{
    public class SshConnector: ISshConnector
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _user;
        private readonly string _password;
        private readonly int _timeoutInSeconds;
        
        private SshClient _client;
        
        public SshConnector(CredentialSsh credentialSsh,  int? timeoutInSeconds)
        {
            _host = credentialSsh.Host;
            _port = credentialSsh.Port;
            _user = credentialSsh.User;
            _password = credentialSsh.Password;
            _timeoutInSeconds = timeoutInSeconds ?? 120;
        }
        
        [Obsolete]
        public SshConnector(string host, int port, string user, string password, int? timeoutInSeconds)
        {
            _host = host;
            _port = port;
            _user = user;
            _password = password;
            _timeoutInSeconds = timeoutInSeconds ?? 120;
        }

        public void CreateClient()
        {
            _client = new SshClient(
                _host,
                _port,
                _user,
                _password
            ) {ConnectionInfo = {Timeout = TimeSpan.FromSeconds(_timeoutInSeconds)}};
        }

        public SshClient GetClient()
        {
            if (_client == null)
            {
                CreateClient();
            }
            return _client;
        }

        public void Connect()
        {
            if (_client == null)
            {
                CreateClient();
            }
            _client.Connect();
        }

        public void AddForwardedPort(ForwardedPortLocal portForwarded)
        {
            if (_client == null)
            {
                CreateClient();
            }
            _client.AddForwardedPort(portForwarded);
        }

        public bool IsConnected()
        {
            if (_client == null)
            {
                CreateClient();
            }
            return _client.IsConnected;
        }
    }
}