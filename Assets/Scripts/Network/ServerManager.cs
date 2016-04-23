class ServerManager : Manager<ServerManager>
{
    public Server _server;

    public ServerManager() {
        if (_instance != null)
            throw new ManagerException();
    }

    public void Init(Server server)
    {
        _server = server;
    }

    public override void Reset()
    {
        if (_server != null)
        {
            if (_server.Client1 != null)
                _server.Client1.Stop();
            if (_server.Client2 != null)
                _server.Client2.Stop();
            _server.Stop();
        }
        base.Reset();
    }
}
