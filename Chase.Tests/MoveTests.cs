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
    public class MoveTests
    {
        [TestMethod]
        public void TestParseMove()
        {
            //    0,  1,  2,  3,  4,  5,  6,  7,  8, // i
            //  9, 10, 11, 12, 13, 14, 15, 16, 17,   // h
            //   18, 19, 20, 21, 22, 23, 24, 25, 26, // g
            // 27, 28, 29, 30, 31, 32, 33, 34, 35,   // f
            //   36, 37, 38, 39, 40, 41, 42, 43, 44, // e
            // 45, 46, 47, 48, 49, 50, 51, 52, 53,   // d
            //   54, 55, 56, 57, 58, 59, 60, 61, 62, // c
            // 63, 64, 65, 66, 67, 68, 69, 70, 71,   // b
            //   72, 73, 74, 75, 76, 77, 78, 79, 80  // a

            Move move;

            move = Move.ParseMove("CH-e4");
            Assert.AreEqual(40, move.FromIndex);
            Assert.AreEqual(39, move.ToIndex);
            Assert.AreEqual(0, move.Increment);

            move = Move.ParseMove("A1+=3");
            Assert.AreEqual(-1, move.FromIndex);
            Assert.AreEqual(72, move.ToIndex);
            Assert.AreEqual(3, move.Increment);

            move = Move.ParseMove("b1-b2+=2");
            Assert.AreEqual(63, move.FromIndex);
            Assert.AreEqual(64, move.ToIndex);
            Assert.AreEqual(2, move.Increment);

            move = Move.ParseMove("boom");
            Assert.IsFalse(move.IsValid);
        }
    }
}
