using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace AlgoLib.Algo {
	/// <summary>
	/// An abstract iterator that yields points on a grid.
	/// Use Next() to get the next point, and HasNext() to check if there
	/// are more points.
	/// Inherits from IEnumerable&lt;Point&gt; to allow usage in foreach loops, and gives access to LINQ methods.
	/// </summary>
	public abstract class GridIterator : IEnumerable<Point> {
		public abstract Point Next();
		public abstract bool HasNext();

		IEnumerator<Point> IEnumerable<Point>.GetEnumerator() {
			while (HasNext()) {
				yield return Next();
			}
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return ((IEnumerable<Point>)this).GetEnumerator();
		}
	}
}
