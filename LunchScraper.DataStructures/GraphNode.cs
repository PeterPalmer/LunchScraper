using System.Collections.Generic;

namespace LunchScraper.DataStructures
{
	public class GraphNode
	{
		private readonly SparseMatrix _matrix;

		public GraphNode(SparseMatrix matrix, int nodeIndex, int weight)
		{
			_matrix = matrix;
			this.NodeIndex = nodeIndex;
			this.Weight = weight;
		}

		public int NodeIndex { get; private set; }
		public int Weight { get; private set; }

		public List<GraphNode> Children
		{
			get
			{
				var matrixRow = _matrix.GetRow(NodeIndex);

				var result = new List<GraphNode>();
				for (int col = 0; col < matrixRow.Length; col++)
				{
					if (matrixRow[col] == 0)
					{
						continue;
					}

					result.Add(new GraphNode(_matrix, col, matrixRow[col]));
				}

				return result;
			}
		}
	}
}
