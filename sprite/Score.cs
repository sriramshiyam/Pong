using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.utils;

namespace Pong.sprite;

class Score : Component
{
    private SpriteFont ScoreFont;
    private Vector2 Score1Position;
    private Vector2 Score2Position;
    public int Score1;
    public int Score2;
    public delegate void GameFinished(object sender, int Score1, int Score2);
    public event GameFinished GameOver;

    public Score()
    {
        ScoreFont = Globals.Content.Load<SpriteFont>("ScoreFont");
        Score1Position = new Vector2(0, 50);
        Score2Position = new Vector2(Globals.CanvasWidth / 2 + 100, 50);
        SetScore1Position();
    }

    public void SetScore1Position()
    {
        Score1Position.X = (Globals.CanvasWidth / 2) - (Score1 < 10 ? ScoreFont.MeasureString("0").X : ScoreFont.MeasureString("10").X) - 100;
    }

    public void UpdateScore(object sender)
    {
        Ball ball = sender as Ball;

        if (ball.Position.X >= Globals.CanvasWidth)
        {
            Score1++;
            SetScore1Position();
        }
        else
        {
            Score2++;
        }

        if (Score1 == 11 || Score2 == 11)
        {
            GameOver?.Invoke(this, Score1, Score2);
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(ScoreFont, Score1.ToString(), Score1Position, Color.White);
        spriteBatch.DrawString(ScoreFont, Score2.ToString(), Score2Position, Color.White);
    }

    public override void Update(GameTime gameTime)
    {
        throw new System.NotImplementedException();
    }
}