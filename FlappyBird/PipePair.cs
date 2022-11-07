using Microsoft.Xna.Framework.Graphics;
using System;

namespace FlappyBird
{
    internal class PipePair
    {
        private const int PIPE_SCROLL = -60;

        private float x;
        private float y;
        private Pipe[] pipes;

        public Pipe[] Pipes { get => pipes; }
        public float X { get => x; }
        public bool HasScored { get; set; }
        public bool Remove { get; set; }

        public PipePair(float y)
        {
            const int GAP_HEIGHT = 90;

            pipes = new Pipe[2];
            pipes[0] = new Pipe("top", y, PIPE_SCROLL);
            pipes[1] = new Pipe("bottom", y + Pipe.Height + GAP_HEIGHT, PIPE_SCROLL);
            x = pipes[0].X;
            this.y = y;
        }

        public void Update(float dt)
        {
            foreach (Pipe pipe in pipes)
                pipe.Update(dt);

            x = pipes[0].X;

            if (x < -Pipe.Width)
                Remove = true;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            foreach (Pipe pipe in pipes)
                pipe.Draw(_spriteBatch);
        }

        public bool HasCollided(Bird bird)
        {
            return Array.Exists(pipes, pipe => pipe.HasCollided(bird));
        }
    }
}
