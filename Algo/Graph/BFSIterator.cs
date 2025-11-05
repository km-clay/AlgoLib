using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using AlgoLib.Geometry;
using System;

namespace AlgoLib.Algo.Graph {
	/// <summary>
  /// An iterator that performs a breadth-first search (BFS) from a starting point.
  /// Expands level-by-level outward from the start. The caller is responsible for
  /// stopping iteration when desired (e.g., based on distance, bounds, or other criteria).
  /// Use HasNext() to check if there are more points, and Next() to get the next point.
  /// </summary>
	public class BFSIterator : GridIterator {
		private Queue<Point> toVisit;
		private HashSet<Point> visited;
		private Point center;
		private bool square;
		private Func<Point,bool> validator;

		public BFSIterator(Point start, Func<Point,bool> validator = null, bool square = false) {
			this.center = start;
			this.square = square;
			this.validator = validator;
			toVisit = new Queue<Point>();
			visited = new HashSet<Point>();
			toVisit.Enqueue(start);
		}

		/// <summary>
		/// Checks if there are more points to yield.
		/// </summary>
		public override bool HasNext() {
			return toVisit.Count > 0;
		}

		private bool NeighborIsValid(Point neighbor) {
			return !visited.Contains(neighbor) && (validator == null || validator(neighbor));
		}

		/// <summary>
		/// Gets the next point in the BFS traversal.
		/// </summary>
		public override Point Next() {
			Point current = toVisit.Dequeue();

			foreach(Point neighbor in GridUtils.GetNeighbors(current, square)) {
				if (NeighborIsValid(neighbor)) {
					toVisit.Enqueue(neighbor);
					visited.Add(neighbor);
				}
			}

			return current;
		}
	}
}
