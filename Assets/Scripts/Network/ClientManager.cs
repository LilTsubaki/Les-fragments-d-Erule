class ClientManager
{
    private static ClientManager manager;

    public Client _client;

    private ClientManager()
    {

    }

    public static ClientManager GetInstance()
    {
        if (manager == null)
            manager = new ClientManager();
        return manager;
    }

    public void Init(Client client)
    {
        _client = client;
    }
}
