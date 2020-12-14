namespace TunnelConnector.LoadBalancer
{
    public interface ILoadBalancer
    {
        void Initiate();
        bool Initiated();
        
        int RunClientAndGetActivePort();
    }
}