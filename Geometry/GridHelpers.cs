using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;

namespace AlgoLib.Geometry {
	/// <summary>
	/// Utility class for grid-based operations and coordinate conversions.
	/// Provides helpers for neighbor lookup, distance calculations, and world/tile coordinate conversions.
	/// </summary>
	public class GridUtils {
		/// <summary>
		/// Gets the neighboring points of a given coordinate.
		/// </summary>
		/// <param name="x">X coordinate of the center point</param>
		/// <param name="y">Y coordinate of the center point</param>
		/// <param name="diagonal">If true, includes diagonal neighbors (8 total). If false, only cardinal directions (4 total).</param>
		/// <returns>Array of neighboring points</returns>
		public static Point[] GetNeighbors(int x, int y, bool diagonal = true) {
			if (diagonal) {
				return new Point[] {
					new Point(x + 1, y),
					new Point(x - 1, y),
					new Point(x, y + 1),
					new Point(x, y - 1),
					new Point(x + 1, y + 1),
					new Point(x - 1, y - 1),
					new Point(x + 1, y - 1),
					new Point(x - 1, y + 1)
				};
			} else {
				return new Point[] {
					new Point(x + 1, y),
					new Point(x - 1, y),
					new Point(x, y + 1),
					new Point(x, y - 1)
				};
			}
		}

		/// <summary>
		/// Gets the neighboring points of a given point.
		/// </summary>
		/// <param name="pnt">Center point</param>
		/// <param name="diagonal">If true, includes diagonal neighbors (8 total). If false, only cardinal directions (4 total).</param>
		/// <returns>Array of neighboring points</returns>
		public static Point[] GetNeighbors(Point pnt, bool diagonal = true) {
			return GetNeighbors(pnt.X, pnt.Y, diagonal);
		}

		/// <summary>
		/// Converts world pixel coordinates to tile grid coordinates.
		/// Each tile is 16x16 pixels in world space.
		/// </summary>
		/// <param name="worldPos">World position in pixels</param>
		/// <returns>Tile coordinate</returns>
		public static Point WorldToTile(Vector2 worldPos) {
			return new Point((int)(worldPos.X / 16f), (int)(worldPos.Y / 16f));
		}
		/// <summary>
		/// Converts world pixel coordinates to tile grid coordinates.
		/// Each tile is 16x16 pixels in world space.
		/// </summary>
		/// <param name="worldX">World X position in pixels</param>
		/// <param name="worldY">World Y position in pixels</param>
		/// <returns>Tile coordinate</returns>
		public static Point WorldToTile(float worldX, float worldY) {
			return WorldToTile(new Vector2(worldX, worldY));
		}

		/// <summary>
		/// Converts tile grid coordinates to world pixel coordinates.
		/// Each tile is 16x16 pixels in world space.
		/// </summary>
		/// <param name="worldPos">Tile coordinate (will be treated as Vector2 with X/Y as tile positions)</param>
		/// <param name="centered">If true, returns the center of the tile (offset +8 pixels). If false, returns top-left corner.</param>
		/// <returns>World position in pixels</returns>
		public static Vector2 TileToWorld(Point tilePos, bool centered = true) {
			float offset = centered ? 8f : 0f;
			return new Vector2(tilePos.X * 16f + offset, tilePos.Y * 16f + offset);
		}
		/// <summary>
		/// Converts tile grid coordinates to world pixel coordinates.
		/// Each tile is 16x16 pixels in world space.
		/// </summary>
		/// <param name="tileX">Tile X coordinate</param>
		/// <param name="tileY">Tile Y coordinate</param>
		/// <param name="centered">If true, returns the center of the tile (offset +8 pixels). If false, returns top-left corner.</param>
		/// <returns>World position in pixels</returns>
		public static Vector2 TileToWorld(int tileX, int tileY, bool centered = true) {
			return TileToWorld(new Point(tileX, tileY), centered);
		}

		/// <summary>
		/// Calculates the Manhattan distance (taxicab distance) between two points.
		/// Manhattan distance is the sum of absolute differences of coordinates: |x1-x2| + |y1-y2|
		/// </summary>
		/// <param name="a">First point</param>
		/// <param name="b">Second point</param>
		/// <returns>Manhattan distance as an integer</returns>
		public static int ManhattanDistance(Point a, Point b) {
			return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
		}
		/// <summary>
		/// Calculates the Euclidean distance (straight-line distance) between two points.
		/// Uses the Pythagorean theorem: sqrt((x1-x2)² + (y1-y2)²)
		/// </summary>
		/// <param name="a">First point</param>
		/// <param name="b">Second point</param>
		/// <returns>Euclidean distance as a float</returns>
		public static float EuclideanDistance(Point a, Point b) {
			return Vector2.Distance(a.ToVector2(), b.ToVector2());
		}

		public static int ChebyshevDistance(Point a, Point b) {
			return Math.Max(Math.Abs(a.X - b.X), Math.Abs(a.Y - b.Y));
		}

		/// <summary>
		/// Calculates the centroid (geometric center) of a collection of points.
		/// The centroid is the average position of all points.
		/// </summary>
		/// <param name="points">Collection of points to find the center of</param>
		/// <returns>Centroid as a Vector2. Returns Vector2.Zero if the collection is empty.</returns>
		public static Vector2 GetCentroid(IEnumerable<Point> points) {
			Vector2 sum = Vector2.Zero;
			int count = 0;
			foreach (Point p in points) {
				sum += p.ToVector2();
				count++;
			}
			if (count == 0) return Vector2.Zero;
			return sum / count;
		}

		public static Vector2 GetDirection(Point from, Point to) {
			Vector2 dir = new Vector2(to.X - from.X, to.Y - from.Y);
			if (dir != Vector2.Zero) {
				dir.Normalize();
			}
			return dir;
		}
	}

	public class TileUtils {
		public static bool GetTileSafe(int x, int y, out Tile tile) {
			tile = Main.tile[x, y];
			if (!WorldGen.InWorld(x, y) || tile == null) {
				return false;
			}
			return true;
		}
		public static bool GetTileSafe(Point pnt, out Tile tile) {
			return GetTileSafe(pnt.X, pnt.Y, out tile);
		}
	}
}
