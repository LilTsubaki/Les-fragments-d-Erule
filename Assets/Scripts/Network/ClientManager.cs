class ClientManager : Manager<ClientManager>
{
    public Client _client;

    public ClientManager() {
        if (_instance != null)
            throw new ManagerException();
    }

    public void Init(Client client)
    {
        _client = client;
    }
}
