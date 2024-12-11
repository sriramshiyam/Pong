using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.utils;

namespace Pong.sprite;

class Paddle : Component
{
    public Box box;
    public Keys UpKey;
    public Keys DownKey;

    public Vector2 Position;
    public float Speed;
    Texture2D Texture;
    public Paddle(int Width, int Height, Vector2 Position)
    {
        this.Position = Position;
        LoadTexture(Width, Height);
    }

    private void LoadTexture(int Width, int Height)
    {
        Texture = new Texture2D(Globals.GraphicsDevice, Width, Height);
        Color[] colors = new Color[Texture.Width * Texture.Height];

        for (int i = 0; i < Texture.Width * Texture.Height; i++)
        {
            colors[i] = Color.White;
        }

        Texture.SetData(colors);
    }

    public override void Update(GameTime gameTime)
    {
        float Delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (Keyboard.GetState().IsKeyDown(UpKey))
        {
            Position.Y -= Speed * Delta;
        }
        else if (Keyboard.GetState().IsKeyDown(DownKey))
        {
            Position.Y += Speed * Delta;
        }

        Position.Y = MathHelper.Clamp(Position.Y,
                                      Globals.SideNetHeight,
                                      Globals.CanvasHeight - Globals.SideNetHeight - Globals.PaddleHeight);

        box.Position = Position;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Position, Color.White);
    }

    public void LoadBox()
    {
        box = new Box();
        box.PreviousOverlapX = -1;
        box.PreviousOverlapY = -1;
        box.Position = Position;
        box.Size = new Vector2(Texture.Width, Texture.Height);
    }

    public void ResetPosition(object sender)
    {
        Position.Y = Globals.CanvasHeight / 2 - Globals.PaddleHeight / 2;
    }
}