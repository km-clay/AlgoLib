using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using AlgoLib.Geometry;

namespace AlgoLib.Algo.Graph {
	/// <summary>
	/// An iterator that performs a breadth-first search (BFS) from a starting point within
	/// specified bounds and up to a maximum radius.
	/// Use Next() to get the next point, and HasNext() to check if there
	/// are more points.
	/// </summary>
	public class BFSIterator : GridIterator {
		private PriorityQueue<Point, float> toVisit;
		private HashSet<Point> visited;
		private Point center;
		private Rectangle bounds;
		private int maxRadius;
		private bool square;

		public BFSIterator(Point start, Rectangle bounds, int maxRadius, bool square = false) {
			this.center = start;
			this.bounds = bounds;
			this.maxRadius = maxRadius;
			this.square = square;
			toVisit = new PriorityQueue<Point, float>();
			visited = new HashSet<Point>();
			if (bounds.Contains(start)) {
				toVisit.Enqueue(start, 0);
			}
		}

		/// <summary>
		/// Checks if there are more points to yield.
		/// </summary>
		public override bool HasNext() {
			return toVisit.Count > 0;
		}

		private bool NeighborIsValid(Point neighbor) {
			return !visited.Contains(neighbor) && bounds.Contains(neighbor);
		}

		/// <summary>
		/// Gets the next point in the BFS traversal.
		/// </summary>
		public override Point Next() {
			Point current = toVisit.Dequeue();

			foreach(Point neighbor in GridUtils.GetNeighbors(current, square)) {
				if (NeighborIsValid(neighbor)) {
					float nDist = Vector2.Distance(neighbor.ToVector2(), center.ToVector2());
					toVisit.Enqueue(neighbor, nDist);
					visited.Add(neighbor);
				}
			}

			return current;
		}
	}
}
