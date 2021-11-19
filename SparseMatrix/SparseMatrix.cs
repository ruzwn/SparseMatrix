using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SparseMatrix
{
	public class SparseMatrix : IEnumerable
	{
		private readonly (int row, int col) _size;

		public (int, int) Size => _size;

		private readonly Dictionary<(int, int), int> elements;

		private bool IsOutOfRange(int row, int col)
		{
			if (row == 0 || col == 0)
				return true;
			if (row > _size.row || col > _size.col)
				return true;
			
			return false;
		}
		
		public int this[int row, int col] // access by matrix index
		{
			get
			{
				(row, col) = (Math.Abs(row), Math.Abs(col));
				if (IsOutOfRange(row, col))
					throw new Exception("Index out of range");
				
				foreach (var (key, value) in elements)
				{
					if (key != (row, col)) continue;
					return value;
				}

				return 0; // default element
			}
		}

		public SparseMatrix()
		{
			_size = (1, 1);
			elements = new Dictionary<(int, int), int>();
		}

		public SparseMatrix(int row, int col)
		{
			row = Math.Max(Math.Abs(row), 1);
			col = Math.Max(Math.Abs(col), 1);
			_size = (row, col);
			elements = new Dictionary<(int, int), int>();
		}

		public SparseMatrix(int[,] table)
		{
			_size = (table.GetLength(0), table.GetLength(1));
			if (_size.row == 0 || _size.col == 0)
			{
				_size = (1, 1);
				return;
			}
			elements = new Dictionary<(int, int), int>();
			
			for (int i = 0; i < _size.row; ++i)
			{
				for (int j = 0; j < _size.col; ++j)
				{
					if (table[i, j] != 0)
						elements.Add((i, j), table[i, j]);
				}
			}
		}

		public SparseMatrix(SparseMatrix other)
		{
			_size = other._size;
			elements = new Dictionary<(int, int), int>(other.elements);
		}
		
		public bool ChangeElem(int row, int col, int value)
		{
			(row, col) = (Math.Abs(row), Math.Abs(col));
			if (IsOutOfRange(row, col))
				throw new Exception("Index out of range");
			
			foreach (var (key, i) in elements)
			{
				if (key != (row, col)) continue;
				elements[key] = value;
				return true;
			}
			
			elements.Add((row, col), value);
			return true;
		}

		public bool DeleteElem(int value)
		{
			foreach (var (key, val) in elements)
			{
				if (val != value) continue;
				elements.Remove(key);
				return true;
			}

			return false;
		}
		
		public bool DeleteElem(int row, int col)
		{
			foreach (var (key, i) in elements)
			{
				if (key != (row, col)) continue;
				elements.Remove(key);
				return true;
			}

			return false;
		}

		private void AddNeighbors(Dictionary<(int, int), int> neighbors, int row, int col)
		{
			for (int i = -1; i < 2; ++i)
			{
				for (int j = -1; j < 2; ++j)
				{
					if (i == 0 && j == 0) continue; // сам элемент
					
					if (row + i == 0)
					{
						if (col + j == 0)
							neighbors.Add((_size.row, _size.col), this[_size.row, _size.col]);
						else if (col + j > _size.col)
							neighbors.Add((_size.row, 1), this[_size.row, 1]);
						else
							neighbors.Add((_size.row, col + j), this[_size.row, col + j]);
					}
					else if (row + i > _size.row)
					{
						if (col + j == 0)
							neighbors.Add((1, _size.col), this[1, _size.col]);
						else if (col + j > _size.col)
							neighbors.Add((1, 1), this[1, 1]);
						else
							neighbors.Add((1, col + j), this[1, col + j]);
					}
					else
					{
						if (col + j == 0)
							neighbors.Add((row + i, _size.col), this[row + i, _size.col]);
						else if (col + j > _size.col)
							neighbors.Add((row + i, 1), this[row + i, 1]);
						else
							neighbors.Add((row + i, col + j), this[row + i, col + j]);
					}
				}
			}
		}

		public Dictionary<(int, int), int> GetNeighbors(int row, int col)
		{
			(row, col) = (Math.Abs(row), Math.Abs(col));
			if (IsOutOfRange(row, col))
				throw new Exception("Index out of range");

			Dictionary<(int row, int col), int> neighbors = new Dictionary<(int, int), int>(); // ?
			
			AddNeighbors(neighbors, row, col);
			return neighbors;
		}

		public override string ToString()
		{
			string str = string.Empty;
			
			for (int i = 1; i < _size.row + 1; ++i)
			{
				for (int j = 1; j < _size.col + 1; ++j)
				{
					str += this[i, j];
					if (j + 1 < _size.col + 1)
						str += "\t";
				}
				if (i + 1 < _size.row + 1)
					str += "\n";
			}

			return str;
		}

		public IEnumerator GetEnumerator() // изменить для упорядоченного вывода соседей?
		{
			return elements.GetEnumerator();
		}
	}
}