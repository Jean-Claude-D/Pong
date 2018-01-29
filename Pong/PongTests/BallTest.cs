using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongLibrary;

namespace PongTests
{
    [TestClass]
    public class BallTest
    {
        [TestMethod]
        public void Ball_BallDiameterEquals0()
        {
            int screenWidth = 50;
            int screenHeight = 25;
            Paddle paddle = GetPaddleWithScreenWidthAndHeight(screenWidth, screenHeight, 5);
            Action createBallwith0Diameter = delegate { new Ball(0, screenWidth, screenHeight, paddle); };

            Assert.ThrowsException<ArgumentException>(createBallwith0Diameter);
        }

        [TestMethod]
        public void Ball_BallDiameterAndPaddleEqualsScreenHeight()
        {
            int screenWidth = 50;
            int screenHeight = 25;
            int ratio = 5;
            Paddle paddle = GetPaddleWithScreenWidthAndHeight(screenWidth, screenHeight, ratio);
            Action createLargeBallInSmallScreen = delegate { new Ball(screenHeight - ratio, screenWidth, screenHeight, paddle); };

            Assert.ThrowsException<ArgumentException>(createLargeBallInSmallScreen);
        }

        [TestMethod]
        public void Ball_BallDiameterEqualsScreenWidth()
        {
            int screenWidth = 5;
            int screenHeight = 25;
            int ratio = 5;
            Paddle paddle = GetPaddleWithScreenWidthAndHeight(screenWidth, screenHeight, ratio);
            Action createLargeBallInSmallScreen = delegate { new Ball(screenWidth, screenWidth, screenHeight, paddle); };

            Assert.ThrowsException<ArgumentException>(createLargeBallInSmallScreen);
        }

        [TestMethod]
        public void Ball_PaddleIsNull()
        {
            Action createBallWithNullPaddle = delegate { new Ball(2, 50, 25, null); };

            Assert.ThrowsException<ArgumentException>(createBallWithNullPaddle);
        }

        [TestMethod]
        public void Ball_PaddleEqualsScreenWidth()
        {
            int screenWidth = 50;
            int screenHeight = 25;
            int ratio = 5;
            Paddle paddle = GetPaddleWithScreenWidthAndHeight(screenWidth, screenHeight, ratio);
            Action createBallWithSmallScreen = delegate { new Ball(1, 50 / ratio, 25, paddle); };

            Assert.ThrowsException<ArgumentException>(createBallWithSmallScreen);
        }

        private Paddle GetPaddleWithScreenWidthAndHeight(int screenWidth, int screenHeight, int ratio)
        {
            return new Paddle(screenWidth / ratio, screenHeight / ratio, screenWidth, screenHeight, ratio);
        }
    }
}
