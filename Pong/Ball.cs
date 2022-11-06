using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    internal class Ball
    {
        private Random rnd;
        private float x;
        private float y;
        private float dx;
        private float dy;
        private int width;
        private int height;

        public float X
        {
            get => x;
            set => x = value;
        }
        public float Y
        {
            get => y;
            set => y = value;
        }
        public float Dx
        {
            get => dx;
            set => dx = value;
        }
        public float Dy
        {
            get => dy;
            set => dy = value;
        }
        public int Width
        {
            get => width;
        }
        public int Height
        {
            get => height;
        }

        public Ball(int x, int y, int width, int height, int servingPlayer)
        {
            rnd = new Random();
            this.x = x;
            this.y = y;
            dx = servingPlayer == 1 ? 100 : -100;
            dy = rnd.Next(-50, 50);
            this.width = width;
            this.height = height;

        }

        public void Reset(int servingPlayer)
        {
            x = (Globals.VIRTUAL_WIDTH / 2) - 2;
            y = (Globals.VIRTUAL_HEIGHT / 2) - 2;
            dx = servingPlayer == 1 ? 100 : -100;
            dy = rnd.Next(-50, 50);
        }

        public bool Collides(Paddle paddle)
        {
            bool collides = false;
            
            if (
                (x < (paddle.X + paddle.Width)) &&
                (paddle.X < (x + width)) &&
                (y < (paddle.Y + paddle.Height)) &&
                (paddle.Y < (y + height))
            )
            {
                collides = true;
            }

            return collides;
        }

        public void Update(float dt)
        {
            x += dx * dt;
            y += dy * dt;
        }

        public void Render(SpriteBatch _spriteBatch, Texture2D colorTexture)
        {
            _spriteBatch.Draw(colorTexture, new Rectangle((int)x, (int)y, width, height), Color.White);
        }
    }
}