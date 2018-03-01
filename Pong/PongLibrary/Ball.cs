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
        //The number of times the _velocity gets applied
        //to this Ball object everytime it moves
        private Vector2 _velocity;
        private Rectangle _screen;
        public Rectangle BoundingBox
        {
            get;
            private set;
        }
        private Paddle _paddle;

        public static Ball GetBallForTestingPurposes(Rectangle ball, Rectangle screen, Paddle paddle, bool isUp, bool isRight)
        {
            Ball toReturn = new Ball(ball.Width, screen.Width, screen.Height, paddle)
            {
                BoundingBox = ball,
                _velocity = new Vector2(isRight ? 1 : -1, isUp ? 1 : -1)
            };

            return toReturn;
        }

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
                    "({0}) must be smaller than the screenHeight ({1})",
                    paddle.BoundingBox.Height + ballDiameter, screenHeight));
            }
            else if(paddle.BoundingBox.Width >= screenWidth)
            {
                throw new ArgumentException
                    (string.Format("The Width of ", paddle.BoundingBox.Width, screenWidth));
            }

            _paddle = paddle;

            //Setting the surrounding screen
            _screen = new Rectangle
            {
                Location = new Point(0, 0),
                Size = new Point(screenWidth, screenHeight)
            };

            //Setting the Ball's collision Rectangle
            Rectangle initBox = new Rectangle
            {
                Location = new Point((screenWidth - ballDiameter) / 2, _screen.Top),
                Size = new Point(ballDiameter)
            };
            BoundingBox = initBox;

            _velocity = new Vector2(1, 1);
           /* _velocity = getRandomVector();*/
        }

        //Returns a Vector2 that respects the speed const
        private Vector2 getRandomVector()
        {
            return new Vector2(negativeOrNot(1), negativeOrNot(1));
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
        /// while keeping it in bounds
        /// </summary>
        public void Move()
        {
            //this Ball's boundaries to be mutated

            Rectangle newBall = BoundingBox;

            newBall.Location = new Point((int)(BoundingBox.X + _velocity.X),
                (int)(BoundingBox.Y + _velocity.Y));

            this.BoundingBox = newBall;

            if (checkCollidePaddle())
            {
                bounceOffPaddle();
            }
            bounceOffScreen();

            //at this point, this Ball object is surely heading in bound
        }

        //Checks if this Ball object is in contact
        //with its associated Paddle object
        private Boolean checkCollidePaddle()
        {
            return BoundingBox.Bottom >= _paddle.BoundingBox.Top
                && (BoundingBox.Right > _paddle.BoundingBox.Left
                && BoundingBox.Left < _paddle.BoundingBox.Right);
        }

        //Bounces this Ball object on the top of its associated Paddle object
        private void bounceOffPaddle()
        {
            Rectangle newBall = this.BoundingBox;
            newBall.Y = _paddle.BoundingBox.Top - BoundingBox.Height;
            this.BoundingBox = newBall;

            bounceOffDirection(Direction.DOWN);
        }

        //Checks in which direction, if any, this Ball object
        //is heading off screen
        private void bounceOffScreen()
        {
            if(this.BoundingBox.Top < this._screen.Top)
            {
                bounceOffDirection(Direction.UP);
            }
            else if(this.BoundingBox.Right > this._screen.Right)
            {
                bounceOffDirection(Direction.RIGHT);
            }
            else if(this.BoundingBox.Left < this._screen.Left)
            {
                bounceOffDirection(Direction.LEFT);
            }
            else if(this.BoundingBox.Bottom > this._screen.Bottom)
            {
                hitBottom();
            }

        }

        //Bounces this Ball object off the specified screen side
        private void bounceOffDirection(Direction side)
        {
            if (side == Direction.UP ||
                side == Direction.DOWN)
            {
                this._velocity.Y *= -1;
            }
            else if (side == Direction.LEFT ||
                side == Direction.RIGHT)
            {
                this._velocity.X *= -1;
            }

            clampBallInScreen();
        }

        private void clampBallInScreen()
        {
            Rectangle newBall = BoundingBox;

            newBall.X = MathHelper.Clamp
                    (newBall.X,
                    _screen.Left,
                    _screen.Right - newBall.Width);
            newBall.Y = MathHelper.Clamp
                    (newBall.Y,
                    _screen.Top,
                    _screen.Bottom - newBall.Height);

            BoundingBox = newBall;
        }

        //Puts this Ball object exactly on the bottom of the screen
        //with a zero Vector2
        private void hitBottom()
        {
            Rectangle newBall = BoundingBox;

            //Put the ball exactly at level with the screen's bottom
            newBall.Y = _screen.Bottom - BoundingBox.Height;

            BoundingBox = newBall;
            _velocity = Vector2.Zero;
        }
    }
}