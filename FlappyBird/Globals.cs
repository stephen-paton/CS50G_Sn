using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;

namespace FlappyBird
{
    internal static class Globals
    {
        public const int VIRTUAL_WIDTH = 512;
        public const int VIRTUAL_HEIGHT = 288;

        public static StateMachine stateMachine;

        public static Random random;

        public static SpriteFont smallFont;
        public static SpriteFont mediumFont;
        public static SpriteFont flappyFont;
        public static SpriteFont hugeFont;

        public static SoundEffect jumpSfx;
        public static SoundEffect explosionSfx;
        public static SoundEffect hurtSfx;
        public static SoundEffect scoreSfx;

        public static Song musicSong;
    }
}
