using System.Collections;
using System.Collections.Generic;

public class ManagerManager
{
    List<Manager> _managers;
    static ManagerManager _instance;

    public static ManagerManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ManagerManager();
        }
        return _instance;
    }

    public void Register(Manager manager)
    {
        _managers.Add(manager);
    }

    public void Remove(Manager manager)
    {
        _managers.Remove(manager);
    }

    public void KillAll()
    {
        for (int i = 0; i < _managers.Count; i++)
        {
            _managers[i].Kill();
        }
        _managers.Clear();
    }
}