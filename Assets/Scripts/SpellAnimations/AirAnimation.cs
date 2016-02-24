using UnityEngine;
using System.Collections;

public class AirAnimation : SpellAnimation {

    
    public float _initialScale;
    public float _finalScale;
    public float _speed;
	
	// Update is called once per frame
	void Update () {
        if (_play)
        {
            Vector3 next = Vector3.MoveTowards(transform.position, _to.transform.position, Time.deltaTime * _speed);
            transform.position = next;
            float distFromToNext = Vector3.Distance(_from.transform.position, next);
            float distFromToTo = Vector3.Distance(_from.transform.position, _to.transform.position);
            float percentage = distFromToNext / distFromToTo;
            float scale = _initialScale + (_finalScale - _initialScale) * percentage;
            gameObject.transform.localScale = new Vector3(scale, scale, scale);
            if(next == _to.transform.position)
            {
                _play = false;
                gameObject.SetActive(false);
            }
        }
	}

    public void Reset()
    {
        transform.localScale = new Vector3(_initialScale, _initialScale, _initialScale);
        transform.position = _from.transform.position;
        Vector3 look = new Vector3(_to.transform.position.x, _from.transform.position.y, _to.transform.position.z);
        gameObject.transform.LookAt(look);
    }

    
}
