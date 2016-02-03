using System.Collections.Generic;
using System.Collections;

public class AStar<T> where T:IAStar<T>
{
	public List<AStarNode<T>> open;
	public List<T> close;
	public T start;
	public T end;

	public AStar ()
	{
	    open=new List<AStarNode<T>>();
	    close=new List<T>();
	}

	public void reset()
    {
		open=new List<AStarNode<T>>();
		close=new List<T>();
	}

	public List<T> CalculateBestPath(T s,T e)
	{
		start = s;
		end = e;
		AStarNode<T> startNode = new AStarNode<T>(start,null);
		open.Add(startNode);

		
		while (open.Count > 0)
		{
			AStarNode<T> best =GetBest(open);   // This is the best node
			if (best.t.Equals(end))     // We are finished
			{
				List<T> sol = new List<T>(); // The solution
				while (best.parent != null)
				{
					sol.Add(best.t);
					best = best.parent;
				}
				return sol; // Return the solution when the parent is null (the first point)
			}
			close.Add(best.t);
			open.Remove(best);
			AddToOpen(best);
		}
		// No path found
		return null;
	}

	public void AddToOpen(AStarNode<T> p)
    {
		List<T> v=p.t.GetNeighbours();
		foreach (T h in v)
        {
			if(h!=null&&!close.Contains(h))
            {
				AStarNode<T> n=new AStarNode<T>(h,p);
				if(!OpenContainsNode(n))
					open.Add(n);
			}
		}
	}

	public AStarNode<T> GetBest(List<AStarNode<T>> ns)
    {
		double min = int.MaxValue;
		AStarNode<T> resu=null;
		foreach (AStarNode<T> n in ns)
        {
			double h=n.Cost()+n.t.Distance(end);
			if(h<min)
            {
				min=h;
				resu=n;
			}
		}
		return resu;

	}

	public bool OpenContainsNode(AStarNode<T> n)
    {
		foreach (AStarNode<T> no in open)
        {
			if(no.t.Equals(n.t))
				return true;
		}
		return false;
	}
}