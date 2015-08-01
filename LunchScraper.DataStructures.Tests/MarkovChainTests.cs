using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LunchScraper.DataStructures.Tests
{
	[TestClass]
	public class MarkovChainTests
	{
		[TestMethod]
		public void AddDistinctWords()
		{
			var chain = new MarkovChain();

			chain.AddWord("A");
			chain.AddWord("B");
			chain.AddWord("C");

			Assert.AreEqual(0, chain.RootNode.NodeIndex);
			Assert.AreEqual(1, chain.RootNode.Children[0].NodeIndex);
			Assert.AreEqual(2, chain.RootNode.Children[0].Children[0].NodeIndex);
			Assert.AreEqual(1, chain.RootNode.Children[0].Children[0].Children.Count);
		}

		[TestMethod]
		public void AddRepeatedWords()
		{
			var chain = new MarkovChain();

			chain.AddWord("A");
			chain.AddWord("B");
			chain.AddWord("A");
			chain.AddWord("C");

			Assert.AreEqual(2, chain.RootNode.Children[0].Children.Count);
			Assert.AreEqual(1, chain.RootNode.Children[0].NodeIndex);
			Assert.AreEqual(3, chain.RootNode.Children[0].Children[1].NodeIndex);
		}

		[TestMethod]
		public void ForEachDistinctWords()
		{
			var chain = new MarkovChain();

			chain.AddWord("A");
			chain.AddWord("B");
			chain.AddWord("C");

			var listOfWords = new List<string>();
			foreach (var word in chain)
			{
				listOfWords.Add(word);
			}

			CollectionAssert.AreEqual(new[] { "A", "B", "C" }, listOfWords);
		}

		[TestMethod]
		public void ForEachCircularGraph_DoesNotEnd()
		{
			var chain = new MarkovChain();

			chain.AddWord("A");
			chain.AddWord("B");
			chain.AddWord("A");
			chain.AddWord("C");
			chain.AddWord("A");

			int iterations = 0;
			foreach (var word in chain)
			{
				Debug.Write(string.Concat(word, " "));

				iterations++;

				if (iterations > 100)
				{
					break;
				}
			}

			Assert.AreEqual(101, iterations);
		}

		[TestMethod]
		public void AddMultipleWords()
		{
			var chain = new MarkovChain();

			chain.AddMultipleWords("A B C");

			var listOfWords = new List<string>();
			foreach (var word in chain)
			{
				listOfWords.Add(word);
			}

			CollectionAssert.AreEqual(new[] { "A", "B", "C" }, listOfWords);
		}
	}
}
