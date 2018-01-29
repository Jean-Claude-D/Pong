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
        public const float speed = 5; 

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
            boundingBox.Location = new Point((screenWidth - ballDiameter) / 2, 0);
            boundingBox.Size = new Point(ballDiameter);
            this.BoundingBox = boundingBox;

            this._velocity = GetRandomVector();
        }

        //Returns a Vector2 that respects the speed const
        private Vector2 GetRandomVector()
        {
            Random rand = new Random();
            float x = (float) rand.NextDouble() * speed;
            //pythagorean theorem to get second component
            float y = (float) Math.Sqrt(Math.Pow(speed, 2) - Math.Pow(x, 2));

            return new Vector2(NegativeOrNot(x), NegativeOrNot(y));
        }

        //Returns the given float as its negative or positive, randomly
        private float NegativeOrNot(float num)
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
    }
}
