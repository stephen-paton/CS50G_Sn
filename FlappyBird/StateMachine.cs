using FlappyBird.states;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace FlappyBird
{
    internal class StateMachine
    {
        private State empty;
        private State current;
        private Dictionary<string, State> states;

        public StateMachine(Dictionary<string, State> states)
        {
            empty = new EmptyState();
            this.states = states == null ? new Dictionary<string, State>() : states;
            current = empty;
        }

        public void Change(string stateName, params object[] enterParams)
        {
            Debug.Assert(states.ContainsKey(stateName), "State must exist!");
            current.Exit();
            current = states[stateName];
            current.Enter(enterParams);
        }

        public void Update(float dt)
        {
            current.Update(dt);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            current.Draw(_spriteBatch);
        }
    }
}
