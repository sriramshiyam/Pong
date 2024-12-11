using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong.utils;

class MainMenu : Component
{
    SpriteFont TitleFont;
    SpriteFont PlayLabelFont;
    Vector2 TitlePosition;
    Vector2 PlayLabelPosition;

    public MainMenu()
    {
        LoadFonts();
        TitlePosition = new Vector2(Globals.CanvasWidth / 2 - TitleFont.MeasureString("PONG").X / 2, 400);
        PlayLabelPosition = new Vector2(Globals.CanvasWidth / 2 - PlayLabelFont.MeasureString("Press Enter To Play").X / 2, 600);
    }

    private void LoadFonts()
    {
        TitleFont = Globals.Content.Load<SpriteFont>("MenuFont1");
        PlayLabelFont = Globals.Content.Load<SpriteFont>("MenuFont2");
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.DrawString(TitleFont, "PONG", TitlePosition, Color.White);
        spriteBatch.DrawString(PlayLabelFont, "Press Enter To Play", PlayLabelPosition, Color.White);
        spriteBatch.End();
    }

    public override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
        {
            Globals.state = State.GAME_LEVEL;
        }
    }
}