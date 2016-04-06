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
}
