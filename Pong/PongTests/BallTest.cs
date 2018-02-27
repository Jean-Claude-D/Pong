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
        public void Ball_DiameterGreaterThanScreenWidth()
        {
            Action init = delegate
            {
                new Ball(ballDiameter: 11,
                    screenWidth: 10,
                    screenHeight: 100,

                    paddle: new Paddle(paddleWidth: 5,
                    paddleHeight: 1,
                    screenWidth: 10,
                    screenHeight: 100,
                    speed: 1));
            };

            Assert.ThrowsException<ArgumentException>(init);
        }

        [TestMethod]
        public void Ball_DiameterEqualToScreenWidth()
        {
            Action init = delegate
            {
                new Ball(ballDiameter:10,
                    screenWidth:10,
                    screenHeight:100,

                    paddle:new Paddle(paddleWidth:5,
                    paddleHeight:1,
                    screenWidth:10,
                    screenHeight:100,
                    speed:1));
            };

            Assert.ThrowsException<ArgumentException>(init);
        }

        [TestMethod]
        public void Ball_DiameterSmallerThanScreenWidth()
        {
            Ball ball = new Ball(ballDiameter: 9,
                    screenWidth: 10,
                    screenHeight: 100,

                    paddle: new Paddle(paddleWidth: 5,
                    paddleHeight: 1,
                    screenWidth: 10,
                    screenHeight: 100,
                    speed: 1));

            Assert.IsNotNull(ball);
        }

        [TestMethod]
        public void Ball_DiameterGreaterThanScreenHeightAndPaddleHeight()
        {
            Action init = delegate
            {
                new Ball(ballDiameter: 10,
                    screenWidth: 100,
                    screenHeight: 10,

                    paddle: new Paddle(paddleWidth: 5,
                    paddleHeight: 1,
                    screenWidth: 100,
                    screenHeight: 10,
                    speed: 1));
            };

            Assert.ThrowsException<ArgumentException>(init);
        }

        [TestMethod]
        public void Ball_DiameterEqualToScreenHeightAndPaddleHeight()
        {
            Action init = delegate
            {
                new Ball(ballDiameter: 9,
                    screenWidth: 100,
                    screenHeight: 10,

                    paddle: new Paddle(paddleWidth: 5,
                    paddleHeight: 1,
                    screenWidth: 100,
                    screenHeight: 10,
                    speed: 1));
            };

            Assert.ThrowsException<ArgumentException>(init);
        }

        [TestMethod]
        public void Ball_DiameterSmallerThanScreenHeightAndPaddleHeight()
        {
            Ball ball = new Ball(ballDiameter: 8,
                    screenWidth: 100,
                    screenHeight: 10,

                    paddle: new Paddle(paddleWidth: 5,
                    paddleHeight: 1,
                    screenWidth: 100,
                    screenHeight: 10,
                    speed: 1));

            Assert.IsNotNull(ball);
        }

        [TestMethod]
        public void Ball_PaddleNull()
        {
            Action init = delegate
            {
                new Ball(ballDiameter: 8,
                    screenWidth: 100,
                    screenHeight: 10,
                    paddle: null);
            };

            Assert.ThrowsException<ArgumentException>(init);
        }

        [TestMethod]
        public void Ball_ScreenSizeNotMatchPaddleScreenSize()
        {
            Action init = delegate
            {
                new Ball(ballDiameter: 9,
                    screenWidth: 100,
                    screenHeight: 10,

                    paddle: new Paddle(paddleWidth: 5,
                    paddleHeight: 1,
                    screenWidth: 10,
                    screenHeight: 100,
                    speed: 1));
            };

            Assert.ThrowsException<ArgumentException>(init);
        }

        [TestMethod]
        public void Move_HitsPaddle()
        {
            Rectangle boundingBox = getStdBall();
            Rectangle screen = getStdScreen();
            Paddle paddle = getStdPaddle();
            Ball ball = Ball.GetBallForTestingPurposes(boundingBox, screen, paddle, true, false);

            Console.WriteLine(screen.Contains(ball.BoundingBox));

            ball.Move();

            Console.WriteLine(screen);
            Console.WriteLine(ball.BoundingBox);

            Console.WriteLine(screen.Top + " >= " + ball.BoundingBox.Top);
            Assert.IsTrue(screen.Top >= ball.BoundingBox.Top);
            Console.WriteLine(screen.Right + " >= " + ball.BoundingBox.Right);
            Assert.IsTrue(screen.Right >= ball.BoundingBox.Right);
            Console.WriteLine(screen.Bottom + " <= " + ball.BoundingBox.Bottom);
            Assert.IsTrue(screen.Bottom <= ball.BoundingBox.Bottom);
            Console.WriteLine(screen.Left + " <= " + ball.BoundingBox.Left);
            Assert.IsTrue(screen.Left <= ball.BoundingBox.Left);
        }

        [TestMethod]
        public void Move_InsideGoUp()
        {
            Rectangle boundingBox = getStdBall();
            Rectangle screen = getStdScreen();
            Paddle paddle = getStdPaddle();
            Ball ball = Ball.GetBallForTestingPurposes(boundingBox, screen, paddle, true, true);

            Console.WriteLine(screen.Contains(ball.BoundingBox));

            ball.Move();

            Console.WriteLine(screen);
            Console.WriteLine(ball.BoundingBox);

            Console.WriteLine(screen.Top + " >= " + ball.BoundingBox.Top);
            Assert.IsTrue(screen.Top >= ball.BoundingBox.Top);
            Console.WriteLine(screen.Right + " >= " + ball.BoundingBox.Right);
            Assert.IsTrue(screen.Right >= ball.BoundingBox.Right);
            Console.WriteLine(screen.Bottom + " <= " + ball.BoundingBox.Bottom);
            Assert.IsTrue(screen.Bottom <= ball.BoundingBox.Bottom);
            Console.WriteLine(screen.Left + " <= " + ball.BoundingBox.Left);
            Assert.IsTrue(screen.Left <= ball.BoundingBox.Left);
        }

        [TestMethod]
        public void Move_InsideGoRight()
        {
            Rectangle boundingBox = getStdBall();
            Rectangle screen = getStdScreen();
            Paddle paddle = getStdPaddle();
            Ball ball = Ball.GetBallForTestingPurposes(boundingBox, screen, paddle, false, true);

            Console.WriteLine(screen.Contains(ball.BoundingBox));

            ball.Move();

            Console.WriteLine(screen);
            Console.WriteLine(ball.BoundingBox);

            Console.WriteLine(screen.Top + " >= " + ball.BoundingBox.Top);
            Assert.IsTrue(screen.Top >= ball.BoundingBox.Top);
            Console.WriteLine(screen.Right + " >= " + ball.BoundingBox.Right);
            Assert.IsTrue(screen.Right >= ball.BoundingBox.Right);
            Console.WriteLine(screen.Bottom + " <= " + ball.BoundingBox.Bottom);
            Assert.IsTrue(screen.Bottom <= ball.BoundingBox.Bottom);
            Console.WriteLine(screen.Left + " <= " + ball.BoundingBox.Left);
            Assert.IsTrue(screen.Left <= ball.BoundingBox.Left);
        }

        [TestMethod]
        public void Move_InsideGoDown()
        {
            Rectangle boundingBox = getStdBall();
            Rectangle screen = getStdScreen();
            Paddle paddle = getStdPaddle();
            Ball ball = Ball.GetBallForTestingPurposes(boundingBox, screen, paddle, false, true);

            Console.WriteLine(screen.Contains(ball.BoundingBox));

            ball.Move();

            Console.WriteLine(screen);
            Console.WriteLine(ball.BoundingBox);

            Console.WriteLine(screen.Top + " >= " + ball.BoundingBox.Top);
            Assert.IsTrue(screen.Top >= ball.BoundingBox.Top);
            Console.WriteLine(screen.Right + " >= " + ball.BoundingBox.Right);
            Assert.IsTrue(screen.Right >= ball.BoundingBox.Right);
            Console.WriteLine(screen.Bottom + " <= " + ball.BoundingBox.Bottom);
            Assert.IsTrue(screen.Bottom <= ball.BoundingBox.Bottom);
            Console.WriteLine(screen.Left + " <= " + ball.BoundingBox.Left);
            Assert.IsTrue(screen.Left <= ball.BoundingBox.Left);
        }

        [TestMethod]
        public void Move_InsideGoLeft()
        {
            Rectangle boundingBox = getStdBall();
            Rectangle screen = getStdScreen();
            Paddle paddle = getStdPaddle();
            Ball ball = Ball.GetBallForTestingPurposes(boundingBox, screen, paddle, true, false);

            Console.WriteLine(screen.Contains(ball.BoundingBox));

            ball.Move();

            Console.WriteLine(screen);
            Console.WriteLine(ball.BoundingBox);

            Console.WriteLine(screen.Top + " >= " + ball.BoundingBox.Top);
            Assert.IsTrue(screen.Top >= ball.BoundingBox.Top);
            Console.WriteLine(screen.Right + " >= " + ball.BoundingBox.Right);
            Assert.IsTrue(screen.Right >= ball.BoundingBox.Right);
            Console.WriteLine(screen.Bottom + " <= " + ball.BoundingBox.Bottom);
            Assert.IsTrue(screen.Bottom <= ball.BoundingBox.Bottom);
            Console.WriteLine(screen.Left + " <= " + ball.BoundingBox.Left);
            Assert.IsTrue(screen.Left <= ball.BoundingBox.Left);
        }

        [TestMethod]
        public void Move_OutsideGoUp()
        {
            Rectangle boundingBox = getStdBall();
            Rectangle screen = getStdScreen();
            Paddle paddle = getStdPaddle();
            Ball ball = Ball.GetBallForTestingPurposes(boundingBox, screen, paddle, true, true);

            Console.WriteLine(screen.Contains(ball.BoundingBox));

            ball.Move();
            ball.Move();
            ball.Move();
            ball.Move();

            Console.WriteLine(screen);
            Console.WriteLine(ball.BoundingBox);

            Console.WriteLine(screen.Top + " >= " + ball.BoundingBox.Top);
            Assert.IsTrue(screen.Top >= ball.BoundingBox.Top);
            Console.WriteLine(screen.Right + " >= " + ball.BoundingBox.Right);
            Assert.IsTrue(screen.Right >= ball.BoundingBox.Right);
            Console.WriteLine(screen.Bottom + " <= " + ball.BoundingBox.Bottom);
            Assert.IsTrue(screen.Bottom <= ball.BoundingBox.Bottom);
            Console.WriteLine(screen.Left + " <= " + ball.BoundingBox.Left);
            Assert.IsTrue(screen.Left <= ball.BoundingBox.Left);
        }

        [TestMethod]
        public void Move_OutsideGoRight()
        {
            Rectangle boundingBox = getStdBall();
            Rectangle screen = getStdScreen();
            Paddle paddle = getStdPaddle();
            Ball ball = Ball.GetBallForTestingPurposes(boundingBox, screen, paddle, false, true);

            Console.WriteLine(screen.Contains(ball.BoundingBox));

            ball.Move();
            ball.Move();
            ball.Move();
            ball.Move();

            Console.WriteLine(screen);
            Console.WriteLine(ball.BoundingBox);

            Console.WriteLine(screen.Top + " >= " + ball.BoundingBox.Top);
            Assert.IsTrue(screen.Top >= ball.BoundingBox.Top);
            Console.WriteLine(screen.Right + " >= " + ball.BoundingBox.Right);
            Assert.IsTrue(screen.Right >= ball.BoundingBox.Right);
            Console.WriteLine(screen.Bottom + " <= " + ball.BoundingBox.Bottom);
            Assert.IsTrue(screen.Bottom <= ball.BoundingBox.Bottom);
            Console.WriteLine(screen.Left + " <= " + ball.BoundingBox.Left);
            Assert.IsTrue(screen.Left <= ball.BoundingBox.Left);
        }

        [TestMethod]
        public void Move_OutsideGoDown()
        {
            Rectangle boundingBox = getStdBall();
            Rectangle screen = getStdScreen();
            Paddle paddle = getStdPaddle();
            Ball ball = Ball.GetBallForTestingPurposes(boundingBox, screen, paddle, false, true);

            Console.WriteLine(screen.Contains(ball.BoundingBox));

            ball.Move();
            ball.Move();
            ball.Move();
            ball.Move();

            Console.WriteLine(screen);
            Console.WriteLine(ball.BoundingBox);

            Console.WriteLine(screen.Top + " >= " + ball.BoundingBox.Top);
            Assert.IsTrue(screen.Top >= ball.BoundingBox.Top);
            Console.WriteLine(screen.Right + " >= " + ball.BoundingBox.Right);
            Assert.IsTrue(screen.Right >= ball.BoundingBox.Right);
            Console.WriteLine(screen.Bottom + " <= " + ball.BoundingBox.Bottom);
            Assert.IsTrue(screen.Bottom <= ball.BoundingBox.Bottom);
            Console.WriteLine(screen.Left + " <= " + ball.BoundingBox.Left);
            Assert.IsTrue(screen.Left <= ball.BoundingBox.Left);
        }

        [TestMethod]
        public void Move_OutsideGoLeft()
        {
            Rectangle boundingBox = getStdBall();
            Rectangle screen = getStdScreen();
            Paddle paddle = getStdPaddle();
            Ball ball = Ball.GetBallForTestingPurposes(boundingBox, screen, paddle, true, false);

            Console.WriteLine(screen.Contains(ball.BoundingBox));

            ball.Move();
            ball.Move();
            ball.Move();
            ball.Move();

            Console.WriteLine(screen);
            Console.WriteLine(ball.BoundingBox);

            Console.WriteLine(screen.Top + " >= " + ball.BoundingBox.Top);
            Assert.IsTrue(screen.Top >= ball.BoundingBox.Top);
            Console.WriteLine(screen.Right + " >= " + ball.BoundingBox.Right);
            Assert.IsTrue(screen.Right >= ball.BoundingBox.Right);
            Console.WriteLine(screen.Bottom + " <= " + ball.BoundingBox.Bottom);
            Assert.IsTrue(screen.Bottom <= ball.BoundingBox.Bottom);
            Console.WriteLine(screen.Left + " <= " + ball.BoundingBox.Left);
            Assert.IsTrue(screen.Left <= ball.BoundingBox.Left);
        }

        private Paddle getStdPaddle()
        {
            return new Paddle(10, 1, 12, 6, 1);
        }

        private Rectangle getStdScreen()
        {
            return new Rectangle(location: Point.Zero, size: new Point(12, -6));
        }

        private Rectangle getStdBall()
        {
            return new Rectangle(x: 10, y: -3, width: 1, height: 1);
        }
    }
}
