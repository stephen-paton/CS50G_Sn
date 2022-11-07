using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FlappyBird
{
    internal class Pipe
    {
        // Static
        private static Texture2D image;
        private static List<Pipe> pipes;

        public static float Height { get => image.Height; }
        public static float Width { get => image.Width; }

        public static void Init(Texture2D image)
        {
            Pipe.image = image;
        }

        // Instance
        private float x;
        private float y;
        private string orientation;
        private float scrollSpeed;

        public float X { get => x; }
        public float Y { get => y; }

        public Pipe(string orientation, float y, float scrollSpeed)
        {
            x = Globals.VIRTUAL_WIDTH + 32;
            this.y = y;
            this.orientation = orientation;
            this.scrollSpeed = scrollSpeed;
        }

        public bool HasCollided(Bird bird)
        {
            bool hasCollided = false;
            if (
                (x < (bird.X + bird.Width)) &&
                (bird.X < (x + Width)) &&
                (y < (bird.Y + bird.Height)) &&
                (bird.Y < (y + Height))
            )
            {
                hasCollided = true;
            }

            return hasCollided;
        }

        public void Update(float dt)
        {
            x += scrollSpeed * dt;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            if (orientation == "top")
            {
                _spriteBatch.Draw(image, new Vector2(x, y + Height), null, Color.White, 0, new Vector2(0, Height), 1, SpriteEffects.FlipVertically, 1);
            }
            else if (orientation == "bottom")
            {
                _spriteBatch.Draw(image, new Vector2(x, y), Color.White);
            }
        }
    }
}
