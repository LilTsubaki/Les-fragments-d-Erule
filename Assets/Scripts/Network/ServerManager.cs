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
        _server.Client1.Stop();
        _server.Client2.Stop();
        _server.Stop();
        base.Reset();
    }
}
