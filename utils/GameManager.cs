using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.sprite;

namespace Pong.utils;

class GameManager : Component
{
    private Canvas canvas;
    private Paddle paddle1;
    private Paddle paddle2;
    private Net net;
    Ball ball;

    public GameManager()
    {
        Globals.CanvasWidth = 1920;
        Globals.CanvasHeight = 1080;
        Globals.PaddleWidth = 25;
        Globals.PaddleHeight = 150;
        Globals.PaddleMargin = 200;
        Globals.SideNetWidth = Globals.CanvasWidth;
        Globals.SideNetHeight = 25;
        Globals.MiddleNetWidth = 25;
        Globals.MiddleNetHeight = Globals.CanvasHeight;
        Globals.BallWidth = 30;
        Globals.BallHeight = 25;


        canvas = new Canvas(Globals.GraphicsDevice, Globals.CanvasWidth, Globals.CanvasHeight);
        canvas.SetDestinationRectangle(this, System.EventArgs.Empty);

        Globals.Window.ClientSizeChanged += canvas.SetDestinationRectangle;

        net = new Net()
        {
            Size1 = new Point(Globals.SideNetWidth, Globals.SideNetHeight),
            Size2 = new Point(Globals.MiddleNetWidth, Globals.MiddleNetHeight),
            Position1 = Vector2.Zero,
            Position2 = new Vector2(Globals.CanvasWidth / 2 - Globals.MiddleNetWidth / 2, 0),
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
        canvas.Activate();

        spriteBatch.Begin();
        paddle1.Draw(spriteBatch);
        paddle2.Draw(spriteBatch);
        net.Draw(spriteBatch);
        ball.Draw(spriteBatch);
        spriteBatch.End();

        canvas.Draw(spriteBatch);
    }

}