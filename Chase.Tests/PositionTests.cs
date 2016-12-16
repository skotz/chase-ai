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

        [TestMethod]
        public void TestGetDestinationIndexIfValidMove()
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

            Position position = Position.EmptyPosition();

            // Long richochet
            Assert.AreEqual(Constants.ChamberIndex, position.GetDestinationIndexIfValidMove(49, Direction.DownLeft, 71));

            // Add a piece
            position.SetPiece(74, 5);

            // Regular moves
            Assert.AreEqual(39, position.GetDestinationIndexIfValidMove(11, Direction.DownRight, 3));

            // Capture and bump moves
            Assert.AreEqual(74, position.GetDestinationIndexIfValidMove(76, Direction.Left, 2));

            // Wrapping moves
            Assert.AreEqual(27, position.GetDestinationIndexIfValidMove(53, Direction.UpRight, 2));

            // Richochet moves
            Assert.AreEqual(32, position.GetDestinationIndexIfValidMove(12, Direction.UpRight, 4));

            // Can't move through the chamber
            Assert.AreEqual(Constants.InvalidMove, position.GetDestinationIndexIfValidMove(39, Direction.Right, 2));

            // Can't move through another piece
            Assert.AreEqual(Constants.InvalidMove, position.GetDestinationIndexIfValidMove(55, Direction.DownRight, 6));
        }

        [TestMethod]
        public void TestFromStringNotation()
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

            Position position = Position.FromStringNotation("a2de2ba/9/b8/Ec2c1E2/C1DB4b/5B3/9/9/AA3A2A b");

            Assert.AreEqual(2, position.PointsToDistribute);
            Assert.AreEqual(-3, position.Board[36]);
            Assert.AreEqual(Player.Blue, position.PlayerToMove);
        }

        [TestMethod]
        public void TestToStringNotation()
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

            string original = "a2de2ba/9/b8/Ec2c1E2/C1DB4b/5B3/9/9/AA3A2A b";

            Position position = Position.FromStringNotation(original);

            string csn = position.ToStringNotation();

            Assert.AreEqual(original, csn);
        }

        [TestMethod]
        public void TestGetHash()
        {
            Position position = Position.FromStringNotation("a2de2ba/9/b8/Ec2c1E2/C1DB4b/5B3/9/9/AA3A2A b");

            ulong hash = position.GetHash();

            // This test mainly verifies that this position always hashes to the same value (meaning the algorithm hasn't changed)
            Assert.AreEqual(16319590400867719686UL, hash);
        }
    }
}
