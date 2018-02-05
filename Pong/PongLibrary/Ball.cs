using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PongLibrary
{
    public class Ball
    {
        public const float speed = 3; 

        private Vector2 _velocity;
        private Rectangle _screen;
        private Rectangle _boundingBox;
        public Rectangle BoundingBox
        {
            get => _boundingBox;
            private set => _boundingBox = value;
        }
        private Paddle _paddle;

        /// <summary>
        /// Constructs a new Ball object with its diameter,
        /// information about its surronding screen
        /// and its associated Paddle object
        /// </summary>
        /// <param name="ballDiameter">this Ball object's diameter</param>
        /// <param name="screenWidth">the surrounding screen's width</param>
        /// <param name="screenHeight">the surrounding screen's height</param>
        /// <param name="paddle">the associated Paddle object</param>
        public Ball(int ballDiameter, int screenWidth, int screenHeight, Paddle paddle)
        {
            if(ballDiameter <= 0)
            {
                throw new ArgumentException
                    (string.Format("ballDiameter ({0}) must be greater than 0", ballDiameter));
            }
            else if(ballDiameter >= screenWidth)
            {
                throw new ArgumentException
                    (string.Format("ballDiameter ({0}) must be smaller than screenWidth ({1})",
                    ballDiameter, screenWidth));
            }
            else if(paddle == null)
            {
                throw new ArgumentException("paddle cannot be null");
            }
            else if(paddle.BoundingBox.Height + ballDiameter >= screenHeight)
            {
                throw new ArgumentException
                    (string.Format("The sum of ballDiameter and the Height of paddle's BoundingBox " +
                    "({0}) must be smaller than the screenHeight",
                    paddle.BoundingBox.Height + ballDiameter));
            }
            else if(paddle.BoundingBox.Width >= screenWidth)
            {
                throw new ArgumentException
                    (string.Format("The Width of ", paddle.BoundingBox.Width, screenWidth));
            }

            this._paddle = paddle;

            //Setting the surrounding screen
            this._screen = new Rectangle();
            this._screen.Location = new Point(0, 0);
            this._screen.Size = new Point(screenWidth, screenHeight);

            //Setting the Ball's collision Rectangle
            Rectangle boundingBox = new Rectangle();
            boundingBox.Location = new Point((screenWidth - ballDiameter) / 2, _screen.Top);
            boundingBox.Size = new Point(ballDiameter);
            this.BoundingBox = boundingBox;

            this._velocity = getRandomVector();
        }

        //Returns a Vector2 that respects the speed const
        private Vector2 getRandomVector()
        {
            Random rand = new Random();
            float x = (float) rand.NextDouble();
            //pythagorean theorem to get second component
            float y = (float) Math.Sqrt(1 - Math.Pow(x, 2));

            return new Vector2(negativeOrNot(x), negativeOrNot(y));
        }

        //Returns the given float as its negative or positive, randomly
        private float negativeOrNot(float num)
        {
            Random rand = new Random();
            int randomInt = (int)(rand.NextDouble() * 2);

            if(randomInt == 0)
            {
                return -num;
            }
            else
            {
                return num;
            }
        }

        /// <summary>
        /// Moves this Ball object according to its _velocity
        /// </summary>
        public void Move()
        {
            Rectangle newBall = this.BoundingBox;
            for (int i = 0; i < speed; i++)
            {
                OffScreenDirection direction = checkOffScreen();

                if (checkCollidePaddle())
                {
                    BounceOffPaddle();
                }
                else if (direction != OffScreenDirection.NONE)
                {
                    bounceOffScreen(direction);
                }

                newBall.Location = new Point((int)(this.BoundingBox.X + this._velocity.X),
                    (int)(this.BoundingBox.Y + this._velocity.Y));
                this.BoundingBox = newBall;
            }

        }

        private Boolean checkCollidePaddle()
        {
            return true;
        }

        public void BounceOffPaddle()
        {

        }

        private OffScreenDirection checkOffScreen()
        {
            return OffScreenDirection.NONE;
        }

        private void bounceOffScreen(OffScreenDirection side)
        {

        }

        private void bounce(Boolean isVertical)
        {
            Vector2 newVector = this._velocity;
            if(isVertical)
            {
                newVector.X *= -1;
            }
            else
            {
                newVector.Y *= -1;
            }

            this._velocity = newVector;
        }
    }
}