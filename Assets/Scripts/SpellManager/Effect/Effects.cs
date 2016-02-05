using System.Collections.Generic;

public class Effects
{
    private List<int> _ids;

    public Effects(JSONObject js)
    {
		
		_ids = new List<int> ();

		if (js != null) {
			foreach (JSONObject id in js.list) {
				_ids.Add ((int)id.n);
			}
		}
    }

    public List<int> GetIds()
    {
        return _ids;
    }

}

