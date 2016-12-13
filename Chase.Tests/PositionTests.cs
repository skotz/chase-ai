using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chase.Engine;

namespace Chase.Tests
{
    [TestClass]
    public class PositionTests
    {
        [TestMethod]
        public void TestGetIndexInDirection()
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

            Position position = Position.NewPosition();

            // Regular moves
            Assert.AreEqual(21, position.GetIndexInDirection(22, Direction.Left));
            Assert.AreEqual(23, position.GetIndexInDirection(22, Direction.Right));
            Assert.AreEqual(31, position.GetIndexInDirection(22, Direction.DownLeft));
            Assert.AreEqual(32, position.GetIndexInDirection(22, Direction.DownRight));
            Assert.AreEqual(13, position.GetIndexInDirection(22, Direction.UpLeft));
            Assert.AreEqual(14, position.GetIndexInDirection(22, Direction.UpRight));
            Assert.AreEqual(40, position.GetIndexInDirection(32, Direction.DownLeft));
            Assert.AreEqual(41, position.GetIndexInDirection(32, Direction.DownRight));
            Assert.AreEqual(22, position.GetIndexInDirection(32, Direction.UpLeft));
            Assert.AreEqual(23, position.GetIndexInDirection(32, Direction.UpRight));

            // Wrapping moves
            Assert.AreEqual(8, position.GetIndexInDirection(0, Direction.Left));
            Assert.AreEqual(9, position.GetIndexInDirection(17, Direction.Right));
            Assert.AreEqual(62, position.GetIndexInDirection(63, Direction.UpLeft));
            Assert.AreEqual(27, position.GetIndexInDirection(44, Direction.UpRight));
            Assert.AreEqual(44, position.GetIndexInDirection(27, Direction.DownLeft));
            Assert.AreEqual(27, position.GetIndexInDirection(26, Direction.DownRight));

            // Invalid moves
            Assert.AreEqual(Constants.InvalidMove, position.GetIndexInDirection(4, Direction.UpLeft));
            Assert.AreEqual(Constants.InvalidMove, position.GetIndexInDirection(76, Direction.DownRight));
        }
    }
}
