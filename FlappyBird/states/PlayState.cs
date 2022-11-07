using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace FlappyBird.states
{
    internal class PlayState : State
    {
        private KeyboardState lastKState;
        private Bird bird;
        private float spawnTimer;
        private float lastY;
        private List<PipePair> pipePairs;
        private int score;

        public void Enter(params object[] enterParams)
        {
            lastKState = Keyboard.GetState();
            bird = new Bird();
            spawnTimer = 0;
            lastY = -Pipe.Height + Globals.random.Next(1, 81) + 20;
            pipePairs = new List<PipePair>();
            score = 0;
        }

        public void Exit()
        {

        }

        public void Update(float dt)
        {
            KeyboardState kState = Keyboard.GetState();

            spawnTimer += dt;

            if (spawnTimer > 2)
            {
                float y = Math.Max(
                    -Pipe.Height + 10,
                    Math.Min(lastY + Globals.random.Next(-20, 21), Globals.VIRTUAL_HEIGHT - 90 - Pipe.Height)
                );
                lastY = y;

                pipePairs.Add(new PipePair(y));

                spawnTimer = 0;
            }

            foreach (PipePair pair in pipePairs)
            {
                if (!pair.HasScored)
                {
                    if (pair.X + Pipe.Width < bird.X)
                    {
                        score += 1;
                        pair.HasScored = true;
                        Globals.scoreSfx.Play();
                    }
                }

                pair.Update(dt);
            }

            bird.Update(dt, kState, lastKState);

            pipePairs.RemoveAll(pair => pair.Remove);

            if (pipePairs.Exists(pair => pair.HasCollided(bird)))
            {
                Globals.explosionSfx.Play();
                Globals.hurtSfx.Play();

                Globals.stateMachine.Change("score", score);
            }

            if (bird.Y > Globals.VIRTUAL_HEIGHT - 15)
                Globals.stateMachine.Change("score", score);

            lastKState = kState;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            foreach (PipePair pair in pipePairs)
            {
                pair.Draw(_spriteBatch);
            }

            string text = $"Score: {score}";
            Vector2 textSize = Globals.flappyFont.MeasureString(text);
            _spriteBatch.DrawString(Globals.flappyFont, text, new Vector2(8, 8), Color.White);

            bird.Draw(_spriteBatch);
        }
    }
}
