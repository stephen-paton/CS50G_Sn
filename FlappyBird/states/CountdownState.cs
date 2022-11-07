using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird.states
{
    internal class CountdownState : State
    {
        private const float COUNTDOWN_TIME = 0.75f;

        private int count;
        private float timer;

        public void Enter(params object[] enterParams)
        {
            count = 3;
            timer = 0;
        }

        public void Exit()
        {

        }

        public void Update(float dt)
        {
            timer += dt;

            if (timer > COUNTDOWN_TIME)
            {
                timer = timer % COUNTDOWN_TIME;
                count -= 1;
            }

            if (count == 0)
            {
                Globals.stateMachine.Change("play");
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            string text;
            Vector2 textSize;

            text = count.ToString();
            textSize = Globals.hugeFont.MeasureString(text);
            _spriteBatch.DrawString(Globals.hugeFont, text, new Vector2((Globals.VIRTUAL_WIDTH / 2) - (textSize.X / 2), 120), Color.White);
        }
    }
}
