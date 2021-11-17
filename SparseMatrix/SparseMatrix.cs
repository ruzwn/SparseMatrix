using System.Collections;
using System.Collections.Generic;

namespace SparseMatrix
{
	public class SparseMatrix // : ICollection
	{
		private readonly (int, int) _size = (row: 0, col: 0);

		public (int, int) Size { get; }

		private Dictionary<(int, int), int /* Template? */> _dictionary;

		public SparseMatrix()
		{
		}

		public SparseMatrix(int row, int col)
		{
			_size = (row, col);
		}

		public SparseMatrix(SparseMatrix other)
		{
			_size = other._size;
			//_dictionary = other._dictionary.Copy;
		}

		public bool ChangeElem(int row, int col, int /* Template? */ value)
		{
			foreach (var elem in _dictionary)
			{
				if (elem.Key == (row, col))
					elem.Key = value;
			}
		}
	}
}