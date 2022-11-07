using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird.states
{
    internal interface State
    {
        void Enter(params object[] enterParams);
        void Exit();
        void Update(float dt);
        void Draw(SpriteBatch _spriteBatch);
    }
}
