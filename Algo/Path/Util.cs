using System;
using System.Collections.Generic;
using AlgoLib.Algo.Line;
using Microsoft.Xna.Framework;

namespace AlgoLib.Algo.Path {
	public class PathUtils {
		/// <summary>
		/// Smooths a path by removing redundant waypoints using greedy line-of-sight optimization.
		/// Reduces the number of waypoints to only essential turning points while maintaining walkability.
		/// </summary>
		/// <param name="path">The original path to smooth. Must contain at least 3 points.</param>
		/// <param name="isWalkable">A predicate function that returns true if a point can be traversed.</param>
		/// <returns>A simplified path with fewer waypoints that maintains the same walkable route.</returns>
		public static List<Point> SmoothPath(List<Point> path, Func<Point, bool> isWalkable) {
			if (path.Count <= 3) return path;
			List<Point> smoothPath = new([path[0]]);
			int currentIdx = 0;

			while (currentIdx < path.Count - 1) {
				int farthestIdx = currentIdx + 1;

				for (int i = currentIdx + 2; i < path.Count; i++) {
					Point current = path[i];
					if (CanWalkTo(smoothPath[^1], current, isWalkable)) {
						farthestIdx = i;
					} else {
						break;
					}
				}

				smoothPath.Add(path[farthestIdx]);
				currentIdx = farthestIdx;
			}

			if (smoothPath[^1] != path[^1]) {
				smoothPath.Add(path[^1]);
			}
			return smoothPath;
		}

		private static bool CanWalkTo(Point a, Point b, Func<Point, bool> isWalkable) {
			var line = new BresenhamIterator(a,b);
			foreach (Point pnt in line) {
				if (!isWalkable(pnt)) return false;
			}
			return true;
		}
	}
}
