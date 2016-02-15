class ServerManager
{
    private static ServerManager manager;

    public Server _server;

    private ServerManager()
    {

    }

    public static ServerManager GetInstance()
    {
        if (manager == null)
            manager = new ServerManager();
        return manager;
    }

    public void Init(Server server)
    {
        _server = server;
    }
}
