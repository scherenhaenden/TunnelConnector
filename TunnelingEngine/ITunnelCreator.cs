namespace TunnelConnector.TunnelingEngine
{
    public interface ITunnelCreator
    {
        void InitiateTunneling();
        void StopTunneling();

        int GetCurrentForwardPort();

        bool IsConnected();
    }
}