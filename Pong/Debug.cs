using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    internal static class Debug
    {
        private static SpriteFont font;

        public static void Initialize(SpriteFont font)
        {
            Debug.font = font;
        }
        public static void DisplayFPS(SpriteBatch _spriteBatch, float dt)
        {
            float fps = 1.0f / dt;
            _spriteBatch.DrawString(font, $"FPS: {fps:F2}", new Vector2(10, 10), Color.Green);
        }
    }
}
