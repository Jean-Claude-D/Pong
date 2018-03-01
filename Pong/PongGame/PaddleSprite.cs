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
    public class PaddleSprite : DrawableGameComponent
    {
        private const int _speed = 5;
        private const Keys _left = Keys.A;
        private const Keys _right = Keys.D;

        private Paddle _paddle;
        public Paddle Paddle
        {
            get => _paddle;
        }

        private SpriteBatch _spriteBatch;
        private Texture2D _imgPaddle;
        private GameOfPong _game;

        private KeyboardState _oldState;
        private int _counter;
        private int _threshold;

        public PaddleSprite(GameOfPong game) : base(game)
        {
            _game = game;
        }

        public override void Initialize()
        {
            _oldState = Keyboard.GetState();
            _threshold = 2;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _imgPaddle = _game.Content.Load<Texture2D>("paddle");
            _paddle = new Paddle(_imgPaddle.Width, _imgPaddle.Height,
                GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, _speed);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            checkInput();
            base.Update(gameTime);
        }

        private void checkInput()
        {
            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(_left))
            {
                if (!_oldState.IsKeyDown(_left))
                {
                    _paddle.MoveLeft();
                    _counter = 0;
                }
                else
                {
                    _counter++;
                    if (_counter > _threshold)
                    {
                        _paddle.MoveLeft();
                    }
                }
            }
            else if (newState.IsKeyDown(_right))
            {
                if (!_oldState.IsKeyDown(_right))
                {
                    _paddle.MoveRight();
                    _counter = 0;
                }
                else
                {
                    _counter++;
                    if (_counter > _threshold)
                    {
                        _paddle.MoveRight();
                    }
                }
            }
            _oldState = newState;
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_imgPaddle,
                new Vector2(_paddle.BoundingBox.X, _paddle.BoundingBox.Y),
                Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
