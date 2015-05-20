using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LunchScraper.DataStructures.Tests
{
	[TestClass]
	public class SparseMatrixTests
	{
		[TestMethod]
		public void Add_2plus3()
		{
			var matrix = new SparseMatrix();

			matrix.Add(4, 5, 2);
			matrix.Add(4, 5, 3);

			Assert.AreEqual(5, matrix[4, 5]);
		}

		[TestMethod]
		public void Set_2plus3()
		{
			var matrix = new SparseMatrix();

			matrix.Set(4, 5, 2);
			matrix.Set(4, 5, 3);

			Assert.AreEqual(3, matrix[4, 5]);
		}

		[TestMethod]
		public void UninitializedValuesAreZero()
		{
			var matrix = new SparseMatrix();

			matrix.Set(4, 5, 2);

			Assert.AreEqual(0, matrix[1, 2]);
		}

		[TestMethod]
		public void SquareBracketOperator_Set()
		{
			var matrix = new SparseMatrix();

			matrix[1, 2] = 2;
			matrix[1, 2] = 3;

			Assert.AreEqual(3, matrix[1, 2]);
		}

		[TestMethod]
		public void SquareBracketOperator_Add()
		{
			var matrix = new SparseMatrix();

			matrix[1, 2] = 2;
			matrix[1, 2] += 3;

			Assert.AreEqual(5, matrix[1, 2]);
		}

		[TestMethod]
		public void GetRow()
		{
			var matrix = new SparseMatrix();

			matrix[1, 1] = 2;
			matrix[1, 3] = 3;

			var rowOne = matrix.GetRow(1);

			CollectionAssert.AreEqual(new[] { 0, 2, 0, 3 }, rowOne);
		}

		[TestMethod]
		public void DimensionsAreUpdatedOnSet()
		{
			var matrix = new SparseMatrix();

			matrix[8, 1] = 14;
			matrix[1, 5] += 14;

			Assert.AreEqual(9, matrix.Height);
			Assert.AreEqual(6, matrix.Width);
		}

		[TestMethod]
		public void DimensionsAreUpdatedOnAdd()
		{
			var matrix = new SparseMatrix();

			matrix.Add(8, 1, 14);
			matrix.Add(1, 5, 14);

			Assert.AreEqual(9, matrix.Height);
			Assert.AreEqual(6, matrix.Width);
		}
	}
}
