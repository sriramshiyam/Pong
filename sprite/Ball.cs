using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.utils;

namespace Pong.sprite;

class Ball : Component
{
    public Vector2 Position;
    public Vector2 Direction;
    public float Speed;
    Texture2D Texture;

    public Ball(int Width, int Height)
    {
        var random = new Random();
        Speed = 300;
        Position = new Vector2(Globals.PaddleMargin + Globals.PaddleWidth, Globals.CanvasHeight / 2 - Height / 2);

        float DirY = random.NextSingle() * (1.0f - (-1.0f)) + (-1.0f);

        while (DirY >= -0.3 && DirY <= 0.3)
        {
            DirY = random.NextSingle() * (1.0f - (-1.0f)) + (-1.0f);
        }

        Direction = new Vector2(1.0f, DirY);
        Console.WriteLine(Direction);
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
        Position += Direction * Speed * Delta;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Position, Color.White);
    }


    public void PaddleBoxCollision(ref Box box)
    {
        Vector2 half1 = new Vector2(Texture.Width, Texture.Height) / 2.0f;
        Vector2 center1 = Position + half1;

        Vector2 half2 = new Vector2(box.Size.X, box.Size.Y) / 2.0f;
        Vector2 center2 = box.Position + half2;

        Vector2 delta = new Vector2(Math.Abs(center1.X - center2.X), Math.Abs(center1.Y - center2.Y));

        box.OverlapX = half1.X + half2.X - delta.X;
        box.OverlapY = half1.Y + half2.Y - delta.Y;

        if (box.OverlapX > 0 && box.OverlapY > 0)
        {
            if (box.PreviousOverlapY > 0)
            {
                Position.X += center1.X < center2.X ? -box.OverlapX : box.OverlapX;
            }
            else if (box.PreviousOverlapX > 0)
            {
                Position.X += center1.X < center2.X ? -box.OverlapX : box.OverlapX;
            }
            else
            {
                Position.X += center1.X < center2.X ? -box.OverlapX : box.OverlapX;
                Position.Y += center1.Y < center2.Y ? -box.OverlapY : box.OverlapY;
            }

            Direction.X *= -1;
            Speed += 50;
        }
        else
        {
            box.PreviousOverlapX = box.OverlapX;
            box.PreviousOverlapY = box.OverlapY;
        }
    }
    public void SideNetBoxCollision(ref Box box)
    {
        Vector2 half1 = new Vector2(Texture.Width, Texture.Height) / 2.0f;
        Vector2 center1 = Position + half1;

        Vector2 half2 = new Vector2(box.Size.X, box.Size.Y) / 2.0f;
        Vector2 center2 = box.Position + half2;

        Vector2 delta = new Vector2(Math.Abs(center1.X - center2.X), Math.Abs(center1.Y - center2.Y));

        box.OverlapX = half1.X + half2.X - delta.X;
        box.OverlapY = half1.Y + half2.Y - delta.Y;

        if (box.OverlapX > 0 && box.OverlapY > 0)
        {
            if (box.PreviousOverlapX > 0)
            {
                Position.Y += center1.Y < center2.Y ? -box.OverlapY : box.OverlapY;
            }
            Direction.Y *= -1;
        }
        else
        {
            box.PreviousOverlapX = box.OverlapX;
        }
    }
}