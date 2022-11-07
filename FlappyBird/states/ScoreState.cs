using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlappyBird.states
{
    internal class ScoreState : State
    {
        int score;

        public void Enter(params object[] enterParams)
        {
            score = (int)enterParams[0];
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
            string text;
            Vector2 textSize;

            text = "Oof! You lost!";
            textSize = Globals.flappyFont.MeasureString(text);
            _spriteBatch.DrawString(Globals.flappyFont, text, new Vector2((Globals.VIRTUAL_WIDTH / 2) - (textSize.X / 2), 64), Color.White);

            text = $"Score: {score}";
            textSize = Globals.mediumFont.MeasureString(text);
            _spriteBatch.DrawString(Globals.mediumFont, text, new Vector2((Globals.VIRTUAL_WIDTH / 2) - (textSize.X / 2), 100), Color.White);

            text = "Press Enter to Play Again!";
            textSize = Globals.mediumFont.MeasureString(text);
            _spriteBatch.DrawString(Globals.mediumFont, text, new Vector2((Globals.VIRTUAL_WIDTH / 2) - (textSize.X / 2), 160), Color.White);
        }
    }
}
