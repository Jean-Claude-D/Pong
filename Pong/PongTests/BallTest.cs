using System;
using Microsoft.Xna.Framework;
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

        [TestMethod]
        public void Move_HitsPaddle()
        {
            Rectangle boundingBox = GetStdBall();
            Rectangle screen = GetStdScreen();
            Paddle paddle = GetStdPaddle();
            Ball ball = Ball.GetBallForTestingPurposes(boundingBox, screen, paddle, new Vector2(-1), 3);
            Point expectedPosition = new Point(7, -2);

            ball.Move();

            Assert.AreEqual(expectedPosition, ball.BoundingBox.Location);
        }

        [TestMethod]
        public void Move_InsideGoUp()
        {

        }

        [TestMethod]
        public void Move_InsideGoRight()
        {

        }

        [TestMethod]
        public void Move_InsideGoDown()
        {

        }

        [TestMethod]
        public void Move_InsideGoLeft()
        {

        }

        [TestMethod]
        public void Move_OutsideGoUp()
        {

        }

        [TestMethod]
        public void Move_OutsideGoRight()
        {

        }

        [TestMethod]
        public void Move_OutsideGoDown()
        {

        }

        [TestMethod]
        public void Move_OutsideGoLeft()
        {

        }

        private Paddle GetStdPaddle()
        {
            return new Paddle(10, 1, 12, 6, 1);
        }

        private Rectangle GetStdScreen()
        {
            return new Rectangle(location: Point.Zero, size: new Point(12, -6));
        }

        private Rectangle GetStdBall()
        {
            return new Rectangle(x: 10, y: -3, width: 1, height: 1);
        }
    }
}
