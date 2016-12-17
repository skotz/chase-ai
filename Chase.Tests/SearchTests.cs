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
            SearchResult result;

            // The only sensible move is to capture on A5
            result = search.GetBestMove(Position.FromStringNotation("1bcdedcba/9/9/9/9/9/9/4a4/BBBDEDBBB b"), new SearchArgs(1, -1));
            Assert.AreEqual(76, result.BestMove.ToIndex);

            // Splitting on the chamber captures two pieces, so we need to make sure pieces to distribute is 2
            Position position = Position.FromStringNotation("1bcdedcb1/9/9/9/5a3/4a4/5B3/9/A1CDEDCBA r");
            result = search.GetBestMove(position, new SearchArgs(1, -1));
            Assert.AreEqual("C6-CH", result.BestMove.ToString());
            position.MakeMove(result.BestMove);
            Assert.AreEqual(2, position.PointsToDistribute);
        }
    }
}
