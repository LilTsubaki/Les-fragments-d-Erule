using System;


public abstract class EffectDeplacement : EffectDirect
{
	protected uint _nbDeplacement;
    protected uint _damage;

    public EffectDeplacement() : base() { }

	public EffectDeplacement (uint id, uint nbDeplacement, uint damage): base(id)
	{
		_nbDeplacement = nbDeplacement;
        _damage = damage;
	}

}
