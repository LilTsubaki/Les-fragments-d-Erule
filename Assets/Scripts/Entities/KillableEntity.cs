
public abstract class  KillableEntity : Entity
{

    public readonly int _lifeMax;
    public int _lifeCurrent;

    public KillableEntity(int lifeMax)
    {
        _lifeMax = lifeMax;
        _lifeCurrent = _lifeMax;
    }

    public KillableEntity(Hexagon position, int lifeMax):base(position)
    {
        _lifeMax = lifeMax;
        _lifeCurrent = _lifeMax;
    }

    public abstract int ReceiveDamage(int value, Element element);
    
}