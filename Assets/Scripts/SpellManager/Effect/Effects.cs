using System.Collections.Generic;

class Effects
{
    private List<int> _ids;

    public Effects(JSONObject js)
    {
        _ids = new List<int>();
        JSONObject idArray = (JSONObject) js.list[0];

        foreach (JSONObject id in idArray.list)
        {
            _ids.Add((int)id.n);
        }
    }

    public List<int> GetIds()
    {
        return _ids;
    }

}

