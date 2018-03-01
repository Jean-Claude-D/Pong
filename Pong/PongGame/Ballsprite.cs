using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PongLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongGame
{
    public class BallSprite : DrawableGameComponent
    {
        private const int _speed = 5;

        private PaddleSprite _paddle;

        private Ball _ball;
        public Ball Ball
        {
            get => _ball;
        }

        private SpriteBatch _spriteBatch;
        private Texture2D _imgBall;
        private GameOfPong _game;

        private int _counter;
        private int _threshold;

        public BallSprite(GameOfPong game) : base(game)
        {
            _game = game;
        }

        public override void Initialize()
        {
            _threshold = 1;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _imgBall = _game.Content.Load<Texture2D>("ball");
            _paddle = _game.Paddle;
            _ball = new Ball(_imgBall.Width,
                GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height,
                _paddle.Paddle);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            updatePosition();
            base.Update(gameTime);
        }

        private void updatePosition()
        {
            _counter++;
            if (_counter > _threshold)
            {
                _ball.Move();
                _ball.Move();
                _counter = 0;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_imgBall,
                new Vector2(_ball.BoundingBox.X, _ball.BoundingBox.Y),
                Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
