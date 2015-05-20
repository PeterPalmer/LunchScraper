using System.Collections.Generic;
using System.Linq;

namespace LunchScraper.DataStructures
{
	public class SparseMatrix
	{
		private readonly Dictionary<int, Dictionary<int, int>> _matrix;
		private int _width;
		private int _height;

		public SparseMatrix()
		{
			_matrix = new Dictionary<int, Dictionary<int, int>>();
		}

		public int Height
		{
			get { return _height; }
		}

		public int Width
		{
			get
			{
				return _width;
			}
		}

		public int this[int row, int col]
		{
			get
			{
				return this.Get(row, col);
			}
			set
			{
				this.Set(row, col, value);
			}
		}

		public int Get(int row, int column)
		{
			Dictionary<int, int> rowDict;

			if (!_matrix.TryGetValue(row, out rowDict))
			{
				return 0;
			}

			int value = 0;

			rowDict.TryGetValue(column, out value);
			return value;
		}

		public void Set(int row, int column, int value)
		{
			Dictionary<int, int> rowDict = this.GetOrAddRow(row);

			if (column >= _width || !rowDict.ContainsKey(column))
			{
				rowDict.Add(column, value);

				if (column + 1 > _width)
				{
					_width = column + 1;
				}
			}
			else
			{
				rowDict[column] = value;
			}
		}

		public void Add(int row, int column, int value)
		{
			Dictionary<int, int> rowDict = this.GetOrAddRow(row);

			if (column >= _width || !rowDict.ContainsKey(column))
			{
				rowDict.Add(column, value);

				if (column + 1 > _width)
				{
					_width = column + 1;
				}
			}
			else
			{
				rowDict[column] += value;
			}
		}

		public int[] GetRow(int row)
		{
			Dictionary<int, int> rowDict;

			if (!_matrix.TryGetValue(row, out rowDict))
			{
				return new int[0];
			}

			var result = new int[rowDict.Keys.Max() + 1];
			foreach (var keyValue in rowDict)
			{
				result[keyValue.Key] = keyValue.Value;
			}

			return result;
		}

		private Dictionary<int, int> GetOrAddRow(int row)
		{
			Dictionary<int, int> rowDict;
			if (!_matrix.TryGetValue(row, out rowDict))
			{
				rowDict = new Dictionary<int, int>();
				_matrix.Add(row, rowDict);

				if (row + 1 > _height)
				{
					_height = row + 1;
				}
			}

			return rowDict;
		}
	}
}

