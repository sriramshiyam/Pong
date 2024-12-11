using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong.utils;

class GameOver : Component
{

    private SpriteFont Font;
    public int Score1;
    public int Score2;
    private Vector2 GameOverPosition;
    private Vector2 ScorePosition;
    public float GameOverTimer;
    public delegate void GameState(object sender);
    public event GameState StartGame;

    public GameOver()
    {
        Font = Globals.Content.Load<SpriteFont>("MenuFont1");

        GameOverPosition = new Vector2(Globals.CanvasWidth / 2 - Font.MeasureString("GAME OVER").X / 2, 400);
        ScorePosition = new Vector2(0, 600);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.DrawString(Font, "GAME OVER", GameOverPosition, Color.White);
        spriteBatch.DrawString(Font, Score1.ToString() + "-" + Score2.ToString(), ScorePosition, Color.White);
        spriteBatch.End();
    }

    public override void Update(GameTime gameTime)
    {
        GameOverTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (GameOverTimer <= 0.0f)
        {
            StartGame?.Invoke(this);
            Globals.state = State.MAIN_MENU;
        }
    }

    public void SetScorePosition()
    {
        ScorePosition.X = Globals.CanvasWidth / 2 - Font.MeasureString(Score1.ToString() + "-" + Score2.ToString()).X / 2;
    }
}