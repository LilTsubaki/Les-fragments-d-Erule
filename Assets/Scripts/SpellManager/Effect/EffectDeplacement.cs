using System;


public abstract class EffectDeplacement : EffectDirect
{
	protected int _nbDeplacement;
    protected int _damage;

    public EffectDeplacement() : base() { }

	public EffectDeplacement (int id, int nbDeplacement, int damage): base(id)
	{
		_nbDeplacement = nbDeplacement;
        _damage = damage;
	}

}
