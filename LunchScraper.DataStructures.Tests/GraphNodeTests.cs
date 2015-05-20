using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LunchScraper.DataStructures.Tests
{
	[TestClass]
	public class GraphNodeTests
	{
		[TestMethod]
		public void GetChildren_Weight()
		{
			// Arrange
			var matrix = new SparseMatrix();
			matrix[1, 2] = 2;
			matrix[1, 4] = 4;
			var node = new GraphNode(matrix, 1, 0);

			// Act
			var children = node.Children;

			// Assert
			Assert.AreEqual(2, children.Count);
			Assert.AreEqual(2, children[0].NodeIndex);
			Assert.AreEqual(2, children[0].Weight);
			Assert.AreEqual(4, children[1].NodeIndex);
			Assert.AreEqual(4, children[1].Weight);
		}

		[TestMethod]
		public void GetChildren_CircularReferences()
		{
			// Arrange
			var matrix = new SparseMatrix();
			matrix[1, 2] = 7;
			matrix[2, 1] = 8;

			// Act
			var node = new GraphNode(matrix, 1, 0);

			// Assert
			var children = node.Children;
			Assert.AreEqual(7, children[0].Weight);

			var grandChildren = children[0].Children;
			Assert.AreEqual(8, grandChildren[0].Weight);

			var grandGrandChildren = grandChildren[0].Children;
			Assert.AreEqual(7, grandGrandChildren[0].Weight);
		}

	}
}
