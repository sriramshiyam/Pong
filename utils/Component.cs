using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong;

abstract class Component
{
    abstract public void Draw(SpriteBatch spriteBatch);
    abstract public void Update(GameTime gameTime);
}