using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace FlappyBird.states
{
    internal class TitleScreenState : State
    {
        public void Enter(params object[] enterParams)
        {

        }

        public void Exit()
        {

        }

        public void Update(float dt)
        {
            KeyboardState kState = Keyboard.GetState();

            if (kState.IsKeyDown(Keys.Enter))
            {
                Globals.stateMachine.Change("countdown");
            }

        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            Vector2 textSize;
            String text;

            text = "Fifty Bird";
            textSize = Globals.flappyFont.MeasureString(text);
            _spriteBatch.DrawString(Globals.flappyFont, text, new Vector2((Globals.VIRTUAL_WIDTH / 2) - (textSize.X / 2), 64), Color.White);

            text = "Press Enter";
            textSize = Globals.mediumFont.MeasureString(text);
            _spriteBatch.DrawString(Globals.mediumFont, text, new Vector2((Globals.VIRTUAL_WIDTH / 2) - (textSize.X / 2), 100), Color.White);
        }
    }
}
