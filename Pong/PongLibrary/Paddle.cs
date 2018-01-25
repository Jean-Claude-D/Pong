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
    }
}