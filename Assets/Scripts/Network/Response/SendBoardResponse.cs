
public class SendBoardResponse
{
    public readonly bool _exist;
    public readonly bool _final;

    public SendBoardResponse(bool exist, bool final)
    {
        _exist = exist;
        _final = final;
    }
}
