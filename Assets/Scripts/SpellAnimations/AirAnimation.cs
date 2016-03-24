using UnityEngine;
using System.Collections;

public class AirAnimation : SpellAnimation {

    
    public float _initialScale;
    public float _finalScale;
    public float _speed;
	public float _height;

	// Update is called once per frame
	void Update () {
        if (_play)
        {
            float distFromToNext = Vector3.Distance(_from.transform.position, gameObject.transform.position);
            float distFromToTo = Vector3.Distance(_from.transform.position, _to.transform.position);
            float percentage = distFromToNext / distFromToTo;
            float scale = _initialScale * (1-percentage) + _finalScale * percentage;
            gameObject.transform.localScale = new Vector3(scale, scale, scale);
			Vector3 next = Vector3.MoveTowards(transform.position, _to.transform.position + new Vector3(0,_height,0), Time.deltaTime * _speed);
            transform.position = next;
			if(Vector3.Distance(next, _to.transform.position) < 1.1f)
            {
                _play = false;
                transform.localScale = new Vector3(_initialScale, _initialScale, _initialScale);
                gameObject.SetActive(false);
            }
        }
	}

    public void Reset()
    {
        transform.localScale = new Vector3(_initialScale, _initialScale, _initialScale);
		transform.position = new Vector3 (_from.transform.position.x, _from.transform.position.y + _height, _from.transform.position.z);
        Vector3 look = new Vector3(_to.transform.position.x, _from.transform.position.y, _to.transform.position.z);
        gameObject.transform.LookAt(look);
    }

    
}
