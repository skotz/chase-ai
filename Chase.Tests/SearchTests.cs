using Chase.Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chase.Tests
{
    [TestClass]
    public class SearchTests
    {
        [TestMethod]
        public void TestGetBestMove()
        {
            Search search = new Search();
            SearchResult result = search.GetBestMove(Position.FromStringNotation("1bcdedcba/9/9/9/9/9/9/4a4/BBBDEDBBB b"), 1);

            // The only sensible move is to capture on A5
            Assert.AreEqual(76, result.BestMove.ToIndex);
        }
    }
}
