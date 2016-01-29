
using System;
using System.Collections.Generic;


public class Pool<T> where T: Poolable<T>,new()
{
	T _patern;
	int _maxPoolable;
	private List<T> list=new List<T>();
	public Pool (T pat,int maxPoolable,int nbStart=0)
	{
		_patern = pat;
		_maxPoolable = maxPoolable;
		for (var i = 0; i < Math.Min(nbStart,maxPoolable); ++i) {
			Create ();
		}
	}

	/// <summary>
	/// Gets the poolable.
	/// </summary>
	/// <returns>The poolable.</returns>
	public T GetPoolable(){
		for (int i=0; i<list.Count; ++i) {
			if(list[i].IsReady()){
				list[i].Pick();
				return list[i];
			}
		}
		if (list.Count < _maxPoolable) {
			T t = Create ();
			t.Pick ();
			return t;
		}

		return default(T);
	}

	/// <summary>
	/// Create an Instance of Poolable.
	/// </summary>
	///<returns>The poolable.</returns>
	private T Create(){
		T t = new T ();
		t.Copy (_patern);
		list.Add (t);
		return t;
	}
}

