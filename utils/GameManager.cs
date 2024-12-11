using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.sprite;

namespace Pong.utils;

class GameManager : Component
{
    private Canvas canvas;
    private MainMenu mainMenu;
    private GameLevel gameLevel;
    private GameOver gameOver;

    public GameManager()
    {
        Globals.state = State.MAIN_MENU;

        Globals.CanvasWidth = 1920;
        Globals.CanvasHeight = 1080;

        canvas = new Canvas(Globals.GraphicsDevice, Globals.CanvasWidth, Globals.CanvasHeight);
        canvas.SetDestinationRectangle(this, System.EventArgs.Empty);

        Globals.Window.ClientSizeChanged += canvas.SetDestinationRectangle;

        mainMenu = new MainMenu();
        gameLevel = new GameLevel();
        gameOver = new GameOver();

        gameLevel.GameOver += GameOverState;
        gameOver.StartGame += gameLevel.Reset;
    }

    public override void Update(GameTime gameTime)
    {
        switch (Globals.state)
        {
            case State.MAIN_MENU:
                mainMenu.Update(gameTime);
                break;
            case State.GAME_LEVEL:
                gameLevel.Update(gameTime);
                break;
            case State.GAME_OVER:
                gameOver.Update(gameTime);
                break;
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        canvas.Activate();
        switch (Globals.state)
        {
            case State.MAIN_MENU:
                mainMenu.Draw(spriteBatch);
                break;
            case State.GAME_LEVEL:
                gameLevel.Draw(spriteBatch);
                break;
            case State.GAME_OVER:
                gameOver.Draw(spriteBatch);
                break;
        }
        canvas.Draw(spriteBatch);
    }

    private void GameOverState(object sender, int Score1, int Score2)
    {
        Globals.state = State.GAME_OVER;
        gameOver.GameOverTimer = 2.0f;
        gameOver.Score1 = Score1;
        gameOver.Score2 = Score2;
        gameOver.SetScorePosition();
    }
}