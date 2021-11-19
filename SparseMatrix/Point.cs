using System;

namespace SparseMatrix
{
	public class Point
	{
		public (double x, double y) Position { get; private set; }
		
		public Point(double x = 0, double y = 0)
		{
			Position = (x, y);
		}

		public void Move(double x = 0, double y = 0)
		{
			Position = (Position.x + x, Position.y + y);
		}
	}
}