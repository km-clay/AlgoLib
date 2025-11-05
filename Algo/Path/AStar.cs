#nullable enable
using Terraria;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using AlgoLib.Geometry;
using AlgoLib.Algo.Line;

namespace AlgoLib.Algo.Path {
	public class AStarPathfinder {
		/// <summary>
		/// Finds the shortest path between two points using the A* pathfinding algorithm.
		/// </summary>
		/// <param name="start">The starting point of the path.</param>
		/// <param name="end">The goal/destination point.</param>
		/// <param name="isWalkable">A predicate function that returns true if a point can be traversed.</param>
		/// <param name="maxSearchDistance">The maximum Manhattan distance from the start point to search. Prevents searching too far.</param>
		/// <param name="stagnationLimit">Optional limit on the number of iterations without progress before terminating the search early. Default is -1 (no limit).</param>
		/// <returns>A list of points representing the path from start to end (inclusive), or null if no path exists.</returns>
		public static List<Point>? FindPath(
				Point start,
				Point end,
				Func<Point,
				bool> isWalkable,
				int maxSearchDistance,
				int stagnationLimit = -1
			) {
			PriorityQueue<Point, int> work = new();
			Dictionary<Point, Point?> cameFrom = new();
			Dictionary<Point, int> gScores = new();
			HashSet<Point> closedSet = new();
			int iterationsSinceLastProgress = 0;
			int bestH = GridUtils.ManhattanDistance(start, end);
			gScores[start] = 0;
			cameFrom[start] = null;
			work.Enqueue(start, 0);

			while (work.Count > 0) {
				Point current = work.Dequeue();

				if (stagnationLimit > 0) {
					int currentH = GridUtils.ManhattanDistance(current, end);
					if (currentH < bestH) {
						bestH = currentH;
						iterationsSinceLastProgress = 0;
					} else if (++iterationsSinceLastProgress > stagnationLimit) {
						return null;
					}
				}

				if (current == end) {
					return BuildPath(cameFrom, current);
				}
				int tentativeG = gScores[current] + 1;

				foreach (var neighbor in GridUtils.GetNeighbors(current)) {
					if (closedSet.Contains(neighbor)) {
						continue;
					}
					int h = GridUtils.ManhattanDistance(neighbor, end);
					int f = tentativeG + h;
					bool walkable = isWalkable(neighbor);
					bool shouldQueue = ((gScores.ContainsKey(neighbor) && walkable && gScores[neighbor] > tentativeG)
							|| (!gScores.ContainsKey(neighbor) && walkable && tentativeG <= maxSearchDistance));

					if (shouldQueue) {
						gScores[neighbor] = tentativeG;
						cameFrom[neighbor] = current;
						closedSet.Add(neighbor);
						work.Enqueue(neighbor, f);
					}
				}
			}

			return null;
		}

		private static List<Point> BuildPath(Dictionary<Point, Point?> cameFrom, Point goal) {
			List<Point> path = new();
			Point current = goal;

			while (cameFrom.ContainsKey(current)) {
				if (cameFrom[current] == null) {
					path.Add(current);
					path.Reverse();
					break;
				} else {
					path.Add(current);
					current = cameFrom[current].Value;
				}
			}

			return path;
		}
	}
}
