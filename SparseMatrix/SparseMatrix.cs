using System;
using System.Collections;
using System.Collections.Generic;

namespace SparseMatrix
{
	public class SparseMatrix : IEnumerable 
	{
		private static readonly Point SDefaultElem = new Point();

		private readonly (int row, int col) _size;
		
		public (int, int) Size => _size;

		private readonly Dictionary<(int, int), Point> _elements;

		public SparseMatrix()              
		{
			_size = (1, 1);
			_elements = new Dictionary<(int, int), Point>();
		}

		public SparseMatrix(int row, int col)
		{
			row = Math.Max(Math.Abs(row), 1);
			col = Math.Max(Math.Abs(col), 1);
			_size = (row, col);
			_elements = new Dictionary<(int, int), Point>();
		}

		public SparseMatrix(Point[,] table)
		{
			_size = (table.GetLength(0), table.GetLength(1));
			_elements = new Dictionary<(int, int), Point>();
			if (_size.row == 0 || _size.col == 0)
			{
				_size = (1, 1);
				return;
			}
			
			for (int i = 0; i < _size.row; ++i)
			{
				for (int j = 0; j < _size.col; ++j)
				{
					if (table[i, j].Position != (0, 0))
						_elements.Add((i, j), table[i, j]);
				}
			}
		}

		public SparseMatrix(SparseMatrix other)
		{
			_size = other._size;
			_elements = new Dictionary<(int, int), Point>(other._elements);
		}
		
		public Point this[int row, int col] // access by matrix index
		{
			get
			{
				(row, col) = GetLoopSize(row, col);
				
				return _elements.ContainsKey((row, col)) ? _elements[(row, col)] : SDefaultElem;
			}
		}
		
		public bool ChangeElem(int row, int col, Point value)
		{
			(row, col) = GetLoopSize(row, col);

			if (_elements.ContainsKey((row, col)))
			{
				_elements[(row, col)] = value;
				return true;
			}
			
			_elements.Add((row, col), value);
			return true;
		}

		public bool DeleteElem(int row, int col)
		{
			(row, col) = GetLoopSize(row, col);

			if (!_elements.ContainsKey((row, col))) return false;
			
			_elements.Remove((row, col));
			return true;
		}

		public Dictionary<(int, int), Point> GetNeighbors(int row, int col)
		{
			Dictionary<(int row, int col), Point> neighbors = new Dictionary<(int, int), Point>();

			AddNeighbors(neighbors, row, col);
			return neighbors;
		}
		
		public IEnumerator GetEnumerator()
		{
			return _elements.GetEnumerator();
		}
		
		private (int, int) GetLoopSize(int row, int col)
		{
			if (row < 1)
				row = row % _size.row + _size.row;
			else if (row > _size.row)
				row = (row % _size.row == 0) ? _size.row : row % _size.row;

			if (col < 1)
				col = col % _size.col + _size.col;
			else if (col > _size.col)
				col = (col % _size.col == 0) ? _size.col : col % _size.col;
			
			return (row, col);
		}

		private void AddNeighbors(Dictionary<(int, int), Point> neighbors, int row, int col)
		{
			(row, col) = GetLoopSize(row, col);

			for (int i = -1; i < 2; ++i)
			{
				for (int j = -1; j < 2; ++j)
				{
					if (i == 0 && j == 0) continue; // сам элемент
					
					(int row, int col) neighbor = GetLoopSize(row + i, col + j);
					neighbors.Add(neighbor, this[neighbor.row, neighbor.col]);
				}
			}
		}
	}
}