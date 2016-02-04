using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TurnManager
{
    private TurnManager _turnManager;

    public TurnManager GetInstance()
    {
        if (_turnManager == null)
            return new TurnManager();
        return _turnManager;
    }
}
