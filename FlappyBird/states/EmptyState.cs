using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird.states
{
    internal class EmptyState : State
    {
        public void Enter(params object[] enterParams) { }
        public void Exit() { }
        public void Update(float dt) { }
        public void Draw(SpriteBatch _spriteBatch) { }
    }
}
