
public class SendBoardResponse
{
    public readonly bool _exist;
    public readonly bool _final;
    public readonly Range _range;
    public readonly Area _area;
    public bool _updateArea = false;

    public SendBoardResponse(bool exist, bool final)
    {
        _exist = exist;
        _final = final;
    }

    public SendBoardResponse(bool exist, bool final, int minRange, int maxRange, bool isPiercing, bool enemyTargetable, Orientation.EnumOrientation orientation, Area area)
    {
        _exist = exist;
        _final = final;
        _range = new Range(minRange, maxRange, isPiercing, enemyTargetable, orientation);
        _area = area;
    }

    public SendBoardResponse(bool exist, bool final, int minRange, int maxRange, bool isPiercing, bool enemyTargetable, Orientation.EnumOrientation orientation)
    {
        _exist = exist;
        _final = final;
        _range = new Range(minRange, maxRange, isPiercing, enemyTargetable, orientation);
    }
}
