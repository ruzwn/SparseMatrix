using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SparseMatrix
{
	public class SparseMatrix : IEnumerable // IEnumerator
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
		
		public int this[int row, int col] // Доступ по индексу к матрице, а не массиву (не с 0, а с 1 начало)
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

		public SparseMatrix() // ?
		{
			_size = (1, 1);
			elements = new Dictionary<(int, int), int>(); // ?
		}

		public SparseMatrix(int row, int col)
		{
			row = Math.Max(Math.Abs(row), 1);
			col = Math.Max(Math.Abs(col), 1);
			_size = (row, col);
			elements = new Dictionary<(int, int), int>(); // ?
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

		private void AddToNeighbors(Dictionary<(int, int), int> neighbors)
		{
			for (int i = -1; i < 2; ++i)
			{
				for (int j = -1; j < 2; ++j)
				{
					
				}
			}
		}

		public Dictionary<(int, int), int> GetNeighbors(int row, int col)
		{
			(row, col) = (Math.Abs(row), Math.Abs(col));
			if (IsOutOfRange(row, col))
				throw new Exception("Index out of range");

			Dictionary<(int row, int col), int> neighbors = new Dictionary<(int, int), int>(); // ?
			AddToNeighbors(neighbors);

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