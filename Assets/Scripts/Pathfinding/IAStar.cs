
using System.Collections.Generic;

public interface IAStar<T>
{
	List<T> GetNeighbours();
	int Distance(T t);
	int Cost();
}

