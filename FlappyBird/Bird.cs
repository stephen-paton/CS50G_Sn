using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlappyBird
{
    internal class Bird
    {
        private static Texture2D image;

        private const int GRAVITY = 20;
        private const int ANTI_GRAVITY = -5;

        private int width;
        private int height;
        private float x;
        private float y;
        private float dy;

        public float X { get => x; }
        public float Y { get => y; }
        public int Width { get => width; }
        public int Height { get => height; }

        public static void Init(Texture2D image)
        {
            Bird.image = image;
        }

        public Bird()
        {
            width = Bird.image.Width;
            height = Bird.image.Height;
            x = (Globals.VIRTUAL_WIDTH / 2) - (width / 2);
            y = (Globals.VIRTUAL_HEIGHT / 2) - (height / 2);
            dy = 0;
        }

        public void Update(float dt, KeyboardState kState, KeyboardState lastKState)
        {
            dy += GRAVITY * dt;

            if (kState.IsKeyDown(Keys.Space) && !lastKState.IsKeyDown(Keys.Space))
            {
                dy = ANTI_GRAVITY;
                Globals.jumpSfx.Play();
            }

            y += dy;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(Bird.image, new Vector2(x, y), Color.White);
        }

        public void Reset()
        {
            y = (Globals.VIRTUAL_HEIGHT / 2) - (height / 2);
            dy = 0;
        }
    }
}
