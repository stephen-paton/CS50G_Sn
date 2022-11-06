using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Pong
{
    internal class Paddle
    {
        private const int PADDLE_SPEED = 200;
        private Random rnd;
        private float x;
        private float y;
        private int dy;
        private int width;
        private int height;

        public float X
        {
            get => x;
        }
        public float Y
        {
            get => y;
        }
        public int Width
        {
            get => width;
        }
        public int Height
        {
            get => height;
        }

        public Paddle(float x, float y, int width, int height)
        {
            rnd = new Random();
            this.x = x;
            this.y = y;
            dy = 0;
            this.width = width;
            this.height = height;
        }

        public void Update(float dt)
        {
            if (dy < 0) 
            {
                y = Math.Max(0, y + (dy * dt));
            }
            else if (dy > 0)
            {
                y = Math.Min(Globals.VIRTUAL_HEIGHT - height, y + (dy * dt));
            }
        }

        public void Render(SpriteBatch _spriteBatch, Texture2D colorTexture)
        {
            _spriteBatch.Draw(colorTexture, new Rectangle((int)x, (int)y, width, height), Color.White);
        }

        public void MoveUp()
        {
            dy = -PADDLE_SPEED;
        }

        public void MoveDown()
        {
            dy = PADDLE_SPEED;
        }

        public void Stop()
        {
            dy = 0;
        }
    }
}
