using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PongLibrary
{
    public class Paddle
    {
        private readonly int _speed;
        private int screenWidth;

        private Rectangle boundingBox;
        public Rectangle BoundingBox
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructs a new Paddle object with its width and height,
        /// information about its surrounding screen's width and height
        /// and its speed.
        /// </summary>
        /// 
        /// <param name="paddleWidth">The width of this Paddle's boundingBox</param>
        /// <param name="paddleHeight">The height of this Paddle's boundingBox</param>
        /// <param name="screenWidth">The width of the surrounding screen,
        /// used to place this Paddle in the center of the screen</param>
        /// <param name="screenHeight">The height of the surrounding screen,
        /// used to place this Paddle in the center of the screen</param>
        /// <param name="speed">The number of pixel(s) this Paddle moves at each update</param>
        public Paddle(int paddleWidth, int paddleHeight, int screenWidth, int screenHeight, int speed)
        {
            if(paddleWidth <= 0)
            {
                throw new ArgumentException
                    (string.Format("paddleWidth ({0}) must be greater than 0", paddleWidth));
            }
            else if(paddleHeight <= 0)
            {
                throw new ArgumentException
                    (string.Format("paddleHeight ({0}) must be greater than 0", paddleHeight));
            }
            else if(screenWidth <= paddleWidth)
            {
                throw new ArgumentException
                    (string.Format("screenWidth ({0}) must be greater than paddleWidth ({1})",
                    screenWidth, paddleWidth));
            }
            else if(screenHeight <= paddleHeight)
            {
                throw new ArgumentException
                    (string.Format("screenHeight ({0}) must be greater than paddleHeight ({1})",
                    screenHeight, paddleHeight));
            }
            else if(speed < 0)
            {
                //To modify, may equal anything but 0 (inversed controls)
                throw new ArgumentException(string.Format("speed ({0}) must be greater than or equal to 0", speed));
            }

            //placing the boundingBox at the center of the screen
            Rectangle boundingBox = new Rectangle();
            boundingBox.X = (screenWidth - paddleWidth) / 2;
            boundingBox.Y = -(screenHeight - paddleHeight);
            boundingBox.Width = paddleWidth;
            boundingBox.Height = paddleHeight;

            this.BoundingBox = boundingBox;
            this.screenWidth = screenWidth;
            this._speed = speed;
        }

        /// <summary>
        /// Moves this Paddle object by decrementing its X position by its speed
        /// </summary>
        public void MoveLeft()
        {
            this.Move(false);
        }

        /// <summary>
        /// Moves this Paddle object by incrementing its X position by its speed
        /// </summary>
        public void MoveRight()
        {
            this.Move(true);
        }

        /// <summary>
        /// Moves this Paddle object along the X axis based on its speed
        /// </summary>
        /// <param name="isRight">if true, moves this Paddle object to the right
        /// if false, moves this Paddle object to the left</param>
        private void Move(Boolean isRight)
        {
            //The x Vector
            int xMovement = ((isRight) ? (this._speed) : (-this._speed));
            int xMin = 0;
            int xMax = this.screenWidth - this.boundingBox.Width;
            Rectangle afterMove = this.boundingBox;

            //Move along the x Vector, but not out of screen
            afterMove.X = MathHelper.Clamp(afterMove.X + xMovement, xMin, xMax);

            this.boundingBox = afterMove;
        }
    }
}