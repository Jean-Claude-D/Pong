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
            int space = 2;
            Paddle paddle = GetPaddleOneStepFromOutOfBound(space);
            int limit = paddle.BoundingBox.X + space;

            paddle.MoveRight();

            Assert.Equals(limit, paddle.BoundingBox.X);
        }

        [TestMethod]
        public void MoveRight_GoingOnBound()
        {
            int space = 2;
            Paddle paddle = GetPaddleOneStepFromBound(space);
            int limit = paddle.BoundingBox.X + space;

            paddle.MoveRight();

            Assert.Equals(limit, paddle.BoundingBox.X);
        }

        [TestMethod]
        public void MoveRight_GoingInBound()
        {
            int space = 5;
            Paddle paddle = GetPaddeOneStepFromInbound(space);
            int expectedX = paddle.BoundingBox.X + space;

            paddle.MoveRight();

            Assert.Equals(expectedX, paddle.BoundingBox);
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

        [TestMethod]
        public void Paddle_paddleWidthEquals0()
        {
            Action instantiatePaddle = delegate () { GetPaddleWithWidth(0); };

            Assert.ThrowsException<ArgumentException>(instantiatePaddle);
        }

        [TestMethod]
        public void Paddle_paddleWidthIsGreaterThan0()
        {
            Paddle paddleValidWidth = null;

            try
            {
                paddleValidWidth = GetPaddleWithWidth(2);
            }
            catch (ArgumentException e)
            {
                Assert.Fail();
            }

            Assert.IsNotNull(paddleValidWidth);
        }

        [TestMethod]
        public void Paddle_paddleHeightEquals0()
        {
            Action instantiatePaddle = delegate () { GetPaddleWithHeight(0); };

            Assert.ThrowsException<ArgumentException>(instantiatePaddle);
        }

        [TestMethod]
        public void Paddle_paddleHeightIsGreaterThan0()
        {
            Paddle paddleValidHeight = null;

            try
            {
                paddleValidHeight = GetPaddleWithHeight(2);
            }
            catch(ArgumentException e)
            {
                Assert.Fail();
            }

            Assert.IsNotNull(paddleValidHeight);
        }

        [TestMethod]
        public void Paddle_screenWidthEqualsPaddleWidth()
        {
            Action instantiatePaddle = delegate () { GetPaddleScreenWidth(3, 3); };

            Assert.ThrowsException<ArgumentException>(instantiatePaddle);
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

        private Paddle GetPaddleWithWidth(int paddleWidth)
        {
            return new Paddle(paddleWidth, 2, paddleWidth * 2, 4, 2);
        }

        private Paddle GetPaddleWithHeight(int paddleHeight)
        {
            return new Paddle(15, paddleHeight, 30, paddleHeight * 2, 2);
        }

        private Paddle GetPaddleScreenWidth(int paddleWidth, int screenWidth)
        {
            return new Paddle(paddleWidth, 2, screenWidth, 4, 3);
        }
    }
}
