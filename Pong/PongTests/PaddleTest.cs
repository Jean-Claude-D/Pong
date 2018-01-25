using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PongLibrary;

namespace PongTests
{
    [TestClass]
    public class PaddleTest
    {
        [TestMethod]
        public void MoveLeft_AlreadyOnBound()
        {
            Paddle paddleOnBound = GetPaddleOnBound(false);
            int initialX = paddleOnBound.BoundingBox.X;

            paddleOnBound.MoveLeft();
            int afterMoveX = paddleOnBound.BoundingBox.X;

            Assert.AreEqual(initialX, afterMoveX);
        }

        [TestMethod]
        public void MoveLeft_GoingOutOfBound()
        {
            int space = 2;
            Paddle paddle = GetPaddleOneStepFromOutOfBound(space);
            int limit = paddle.BoundingBox.X - space;

            paddle.MoveLeft();

            Assert.Equals(limit, paddle.BoundingBox.X);
        }

        [TestMethod]
        public void MoveLeft_GoingOnBound()
        {
            int space = 2;
            Paddle paddle = GetPaddleOneStepFromBound(space);
            int limit = paddle.BoundingBox.X - space;

            paddle.MoveLeft();

            Assert.Equals(limit, paddle.BoundingBox.X);
        }

        [TestMethod]
        public void MoveLeft_GoingInBound()
        {
            int space = 5;
            Paddle paddle = GetPaddeOneStepFromInbound(space);
            int expectedX = paddle.BoundingBox.X - space;

            paddle.MoveLeft();

            Assert.Equals(expectedX, paddle.BoundingBox);
        }

        [TestMethod]
        public void MoveRight_AlreadyOnBound()
        {
            Paddle paddleOnBound = GetPaddleOnBound(true);
            int initialX = paddleOnBound.BoundingBox.X;

            paddleOnBound.MoveRight();
            int afterMoveX = paddleOnBound.BoundingBox.X;

            Assert.AreEqual(initialX, afterMoveX);
        }

        [TestMethod]
        public void MoveRight_GoingOutOfBound()
        {
        }

        [TestMethod]
        public void MoveRight_GoingOnBound()
        {
        }

        [TestMethod]
        public void MoveRight_GoingInBound()
        {
        }

        [TestMethod]
        public void Paddle_paddleWidthEquals0()
        {

        }

        [TestMethod]
        public void Paddle_paddleWidthIsGreaterThan0()
        {
            
        }

        [TestMethod]
        public void Paddle_paddleHeightEquals0()
        {

        }

        [TestMethod]
        public void Paddle_paddleHeightIsGreaterThan0()
        {

        }

        [TestMethod]
        public void Paddle_screenWidthEqualsPaddleWidth()
        {

        }

        [TestMethod]
        public void Paddle_screenWidthIsGreaterThanPaddleWidth()
        {

        }

        [TestMethod]
        public void Paddle_screenHeightEqualsPaddleHeight()
        {

        }

        [TestMethod]
        public void Paddle_screenHeightIsGreaterThanPaddleHeight()
        {

        }

        [TestMethod]
        public void Paddle_speedEquals0()
        {

        }

        [TestMethod]
        public void Paddle_speedEqualsNot0()
        {

        }

        private Paddle GetPaddleOnBound(Boolean isRightBound)
        {
            Paddle paddleOnBound = new Paddle(18, 2, 20, 15, 1);

            if (isRightBound)
            {
                paddleOnBound.MoveRight();
            }
            else
            {
                paddleOnBound.MoveLeft();
            }

            return paddleOnBound;
        }

        private Paddle GetPaddleOneStepFromOutOfBound(int pixels)
        {
            return new Paddle(20, 2, 20 + (pixels * 2), 15, (pixels + 5));
        }

        private Paddle GetPaddleOneStepFromBound(int pixels)
        {
            return new Paddle(20, 2, 20 + (pixels * 2), 15, pixels);
        }

        private Paddle GetPaddeOneStepFromInbound(int speed)
        {
            return new Paddle(20, 2, 20 + (speed * 4), 15, speed);
        }
    }
}
