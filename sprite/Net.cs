using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.utils;

namespace Pong.sprite;

class Net : Component
{

    public Box box1;
    public Box box2;
    Texture2D Texture1;
    Texture2D Texture2;
    public Point Size1;
    public Point Size2;

    public Vector2 Position1;
    public Vector2 Position2;
    public Vector2 Position3;

    public Net()
    {
    }

    public void LoadBoxes()
    {
        box1 = new Box();
        box1.PreviousOverlapX = -1;
        box1.PreviousOverlapY = -1;
        box1.Position = Position1;
        box1.Size = new Vector2(Size1.X, Size1.Y);

        box2 = new Box();
        box2.PreviousOverlapX = -1;
        box2.PreviousOverlapY = -1;
        box2.Position = Position3;
        box2.Size = new Vector2(Size1.X, Size1.Y);
    }

    public void LoadTextures()
    {
        Texture1 = new Texture2D(Globals.GraphicsDevice, Size1.X, Size1.Y);
        Texture2 = new Texture2D(Globals.GraphicsDevice, Size2.X, Size2.Y);

        Color[] colors = new Color[Texture1.Width * Texture1.Height];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.White;
        }

        Texture1.SetData(colors);

        colors = new Color[Texture2.Width * Texture2.Height];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.White;
        }

        Texture2.SetData(colors);
    }

    public override void Update(GameTime gameTime)
    {
        throw new System.NotImplementedException();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture1, Position1, Color.White);
        spriteBatch.Draw(Texture2, Position2, Color.White);
        spriteBatch.Draw(Texture1, Position3, Color.White);
    }

}