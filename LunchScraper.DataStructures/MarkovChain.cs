using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LunchScraper.DataStructures
{

	public class MarkovChain : IEnumerable<string>
	{
		private readonly SparseMatrix _matrix = new SparseMatrix();
		private readonly Dictionary<string, int> _wordIndexLookup = new Dictionary<string, int>();
		private readonly Dictionary<int, string> _indexWordLookup = new Dictionary<int, string>();
		private readonly GraphNode _rootNode;
		private readonly Random _randomizer = new Random((int)DateTime.Now.Ticks);
		private int _currentIndex;

		public MarkovChain()
		{
			_rootNode = new GraphNode(_matrix, 0, 0);
		}

		public GraphNode RootNode
		{
			get
			{
				return _rootNode;
			}
		}

		public void AddWord(string word)
		{
			int wordIndex;
			if (!_wordIndexLookup.TryGetValue(word, out wordIndex))
			{
				wordIndex = _matrix.Width;
				_wordIndexLookup.Add(word, wordIndex);
				_indexWordLookup.Add(wordIndex, word);
			}

			if (_matrix.Height == 0)
			{
				// first word
				_matrix[0, 0] = 0;
			}
			else
			{
				_matrix.Add(_currentIndex, wordIndex, 1);
			}

			_currentIndex = wordIndex;
		}

		public void AddMultipleWords(string inputText)
		{
			var wordList = inputText.Split(new[] { ' ', '-', '.', ',', '”', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var word in wordList)
			{
				this.AddWord(word);
			}
		}

		public IEnumerator<string> GetEnumerator()
		{
			var currentNode = _rootNode;

			while (currentNode.Children.Count != 0)
			{
				yield return _indexWordLookup[currentNode.NodeIndex];

				var random = _randomizer.Next(currentNode.Children.Sum(c => c.Weight));
				var offSet = 0;

				foreach (var child in currentNode.Children)
				{
					if (random < child.Weight + offSet)
					{
						currentNode = child;
						break;
					}

					offSet += child.Weight;
				}
			}

			yield return _indexWordLookup[currentNode.NodeIndex];
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public string GenerateSentence(int wordCount)
		{
			string description = "";
			int counter = 1;

			foreach (var word in this)
			{
				description = String.Concat(description, " ", word);
				counter++;

				if (counter > wordCount)
				{
					break;
				}
			}

			return description;
		}

	}
}
