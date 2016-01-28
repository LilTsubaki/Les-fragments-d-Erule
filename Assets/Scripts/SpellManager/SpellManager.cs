using System.Collections.Generic;

public class SpellManager
{
    private SpellManager _spellManager;

    private Dictionary<int, Area> _areas;
    private Dictionary<int, Range> _ranges;
    private Dictionary<int, Effect> _effects;
    private ElementNode _elementNode;

    private SpellManager() { }

    public SpellManager getInstance()
    {
        if (_spellManager == null)
            _spellManager = new SpellManager();

        return _spellManager;
    }
   
    
}
