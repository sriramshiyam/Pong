using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.sprite;

namespace Pong.utils;

class GameLevel : Component
{
    private Paddle paddle1;
    private Paddle paddle2;
    private Net net;
    private Score score;
    Ball ball;
    public delegate void GameFinished(object sender, int Score1, int Score2);
    public event GameFinished GameOver;

    public GameLevel()
    {
        Globals.PaddleWidth = 25;
        Globals.PaddleHeight = 150;
        Globals.PaddleMargin = 200;
        Globals.SideNetWidth = Globals.CanvasWidth;
        Globals.SideNetHeight = 25;
        Globals.MiddleNetWidth = 25;
        Globals.MiddleNetHeight = Globals.CanvasHeight - Globals.SideNetHeight * 2;
        Globals.BallWidth = 30;
        Globals.BallHeight = 25;

        net = new Net()
        {
            Size1 = new Point(Globals.SideNetWidth, Globals.SideNetHeight),
            Size2 = new Point(Globals.MiddleNetWidth, Globals.MiddleNetHeight),
            Position1 = Vector2.Zero,
            Position2 = new Vector2(Globals.CanvasWidth / 2 - Globals.MiddleNetWidth / 2, Globals.SideNetHeight),
            Position3 = new Vector2(0, Globals.CanvasHeight - Globals.SideNetHeight)
        };
        net.LoadTextures();
        net.LoadBoxes();

        paddle1 = new Paddle(Globals.PaddleWidth, Globals.PaddleHeight,
                            new Vector2(Globals.PaddleMargin, Globals.CanvasHeight / 2 - Globals.PaddleHeight / 2))
        {
            UpKey = Keys.W,
            DownKey = Keys.S,
            Speed = 500
        };

        paddle1.LoadBox();

        paddle2 = new Paddle(Globals.PaddleWidth, Globals.PaddleHeight,
                    new Vector2(Globals.CanvasWidth - Globals.PaddleMargin, Globals.CanvasHeight / 2 - Globals.PaddleHeight / 2))
        {
            UpKey = Keys.Up,
            DownKey = Keys.Down,
            Speed = 500
        };

        paddle2.LoadBox();

        ball = new Ball(Globals.BallWidth, Globals.BallHeight);

        ball.BallOutOfBounds += paddle1.ResetPosition;
        ball.BallOutOfBounds += paddle2.ResetPosition;

        score = new Score();
        score.Score1 = 0;
        score.Score2 = 0;
        score.GameOver += GameOverState;

        ball.BallOutOfBounds += score.UpdateScore;
    }
    public override void Update(GameTime gameTime)
    {
        paddle1.Update(gameTime);
        paddle2.Update(gameTime);
        ball.Update(gameTime);

        ball.PaddleBoxCollision(ref paddle1.box);
        ball.PaddleBoxCollision(ref paddle2.box);
        ball.SideNetBoxCollision(ref net.box1);
        ball.SideNetBoxCollision(ref net.box2);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        paddle1.Draw(spriteBatch);
        paddle2.Draw(spriteBatch);
        net.Draw(spriteBatch);
        ball.Draw(spriteBatch);
        score.Draw(spriteBatch);
        spriteBatch.End();
    }

    private void GameOverState(object sender, int Score1, int Score2)
    {
        GameOver?.Invoke(this, Score1, Score2);
    }

    public void Reset(object sender)
    {
        paddle1.ResetPosition(this);
        paddle2.ResetPosition(this);
        ball.ResetPosition();
        score.Score1 = 0;
        score.Score2 = 0;
        score.SetScore1Position();
    }
}