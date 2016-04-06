using System.Collections;
using System.Collections.Generic;

public abstract class Manager
{
    public abstract void Kill();
}

public class Manager<T> : Manager where T : Manager, new()
{
    protected static T _instance;

    /// <summary>
    /// Gets the only instance of T. Creates it if null.
    /// </summary>
    /// <returns></returns>
    public static T GetInstance()
    {
        if (_instance == null)
        {
            _instance = new T();
            ManagerManager.GetInstance().Register(_instance);
        }
        return _instance;
    }
    
    /// <summary>
    /// Kill the instanciated manager.
    /// </summary>
    public override void Kill()
    {
        _instance = null;
    }
}
