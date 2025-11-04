using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace AlgoLib.Algo.Line {
	/// <summary>
	/// An iterator that yields all the points on a line between two points using Bresenham
	/// line algorithm.
	/// Use Next() to get the next point, and HasNext() to check if there are more points.
	/// You can also use CollectLine() to get all the points at once.
	/// </summary>
	public class BresenhamIterator : GridIterator {
		private int x;
		private int y;

		private int endX;
		private int endY;

		private int dx;
		private int dy;
		private int sx;
		private int sy;
		private int err;

		private bool finished;

		public BresenhamIterator(Point start, Point end) {
			x = start.X;
			y = start.Y;
			endX = end.X;
			endY = end.Y;

			dx = Math.Abs(endX - x);
			dy = Math.Abs(endY - y);
			sx = x < endX ? 1 : -1;
			sy = y < endY ? 1 : -1;
			err = dx - dy;

			finished = false;
		}

		/// <summary>
		/// Checks if there are more points to yield.
		/// </summary>
		public override bool HasNext() {
			return !finished;
		}

		/// <summary>
		/// Gets the next point on the line.
		/// </summary>
		public override Point Next() {
			Point current = new(x,y);

			if (x == endX && y == endY) {
				finished = true;
				return current;
			}

			int e2 = 2 * err;
			if (e2 > -dy) {
				err -= dy;
				x += sx;
			}
			if (e2 < dx) {
				err += dx;
				y += sy;
			}

			return current;
		}
	}
}
