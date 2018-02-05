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
        private float speed; 

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
        /// Constructs a new fully customizable Ball object,
        /// to be used only for testing purposes
        /// </summary>
        /// <param name="ballDiameter">this Ball object's diameter</param>
        /// <param name="screen">the surrounding screen</param>
        /// <param name="paddle">the associated Paddle object</param>
        /// <param name="velocity">this Ball's vector of movement</param>
        /// <returns></returns>
        public static Ball GetBallForTestingPurposes(Rectangle boundingBox, Rectangle screen, Paddle paddle, Vector2 velocity, int speed)
        {
            Ball testingBall = new Ball(boundingBox.Width, screen.Width, screen.Height, paddle);
            testingBall._velocity = velocity;
            testingBall.BoundingBox = boundingBox;
            testingBall.speed = speed;

            return testingBall;
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
            else if(paddle.BoundingBox.Height + ballDiameter <= screenHeight)
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
            this.speed = 3;
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
        /// while keeping it in bounds
        /// </summary>
        public void Move()
        {
            //this Ball's boundaries to be mutated
            Rectangle newBall = this.BoundingBox;
            for (int i = 0; i < speed; i++)
            {
                Console.WriteLine("Ball is at ({0}, {1})", this.BoundingBox.X, this.BoundingBox.Y);
                Direction direction = checkOffScreen();

                if (checkCollidePaddle())
                {
                    Console.WriteLine("Bouoncing on paddle " + this._paddle.BoundingBox.ToString());
                    BounceOffPaddle();
                }
                else if (direction != Direction.NONE)
                {
                    bounceOffScreen(direction);
                }

                //at this point, this Ball object is surely heading in bound
                newBall.Location = new Point((int)(this.BoundingBox.X + this._velocity.X),
                    (int)(this.BoundingBox.Y + this._velocity.Y));
                this.BoundingBox = newBall;
            }

        }

        //Checks if this Ball object is in contact
        //with its associated Paddle object
        private Boolean checkCollidePaddle()
        {
            return this.BoundingBox.Intersects(this._paddle.BoundingBox) ||
                (this.BoundingBox.Bottom == this._paddle.BoundingBox.Top &&
                this.BoundingBox.Left <= this._paddle.BoundingBox.Right &&
                this.BoundingBox.Right >= this._paddle.BoundingBox.Left);
        }

        /// <summary>
        /// Bounces this Ball object on the top of its associated
        /// Paddle object
        /// </summary>
        public void BounceOffPaddle()
        {
            Rectangle newBall = this.BoundingBox;
            newBall.Location = new Point(this.BoundingBox.X,
                this._paddle.BoundingBox.Top + this.BoundingBox.Height * 2);
            this.BoundingBox = newBall;

            bounce(Direction.DOWN);
        }

        //Checks in which direction, if any, this Ball object
        //is heading off screen
        private Direction checkOffScreen()
        {
            if(this.BoundingBox.Top + this._velocity.Y > this._screen.Top)
            {
                Console.WriteLine("Up");
                return Direction.UP;
            }
            else if(this.BoundingBox.Right + this._velocity.X > this._screen.Right)
            {
                Console.WriteLine("Right");
                return Direction.RIGHT;
            }
            else if(this.BoundingBox.Left + this._velocity.X < this._screen.Left)
            {
                Console.WriteLine("Left");
                return Direction.LEFT;
            }
            else if(this.BoundingBox.Bottom + this._velocity.Y < this._screen.Bottom)
            {
                Console.WriteLine("Down");
                return Direction.DOWN;
            }
            else
            {
                Console.WriteLine("No");
                return Direction.NONE;
            }
        }

        //Bounces this Ball object off the specified screen side
        private void bounceOffScreen(Direction side)
        {
            if(side == Direction.DOWN)
            {
                hitBottom();
            }
            else
            {
                //A Direction.NONE would be ignored
                bounce(side);
            }
        }

        //Puts this Ball object exactly on the bottom of the screen
        //with a zero Vector2
        private void hitBottom()
        {
            this._velocity = Vector2.Zero;
            //Put the ball exactly at level with the screen's bottom
            Rectangle newBall = this.BoundingBox;
            newBall.Location = new Point(this.BoundingBox.X, this._screen.Bottom + this.BoundingBox.Height);
            this.BoundingBox = newBall;
        }

        //Inverses the _velocity vector to mimic a bounce
        //on the side specified by a Direction
        private void bounce(Direction side)
        {
            if(side == Direction.UP ||
                side == Direction.DOWN)
            {
                this._velocity.Y *= -1;
            }
            else if(side == Direction.LEFT ||
                side == Direction.RIGHT)
            {
                this._velocity.X *= -1;
            }
        }
    }
}