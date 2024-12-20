using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.utils;

class Globals
{
    public static GraphicsDevice GraphicsDevice;
    public static GameWindow Window;
    public static ContentManager Content;
    public static State state;
    public static int CanvasWidth;
    public static int CanvasHeight;
    public static int PaddleWidth;
    public static int PaddleHeight;
    public static int PaddleMargin;
    public static int SideNetWidth;
    public static int SideNetHeight;
    public static int MiddleNetWidth;
    public static int MiddleNetHeight;
    public static int BallWidth;
    public static int BallHeight;
}

enum State
{
    MAIN_MENU,
    GAME_LEVEL,
    GAME_OVER
}

struct Box
{
    public float OverlapX;
    public float OverlapY;
    public float PreviousOverlapX;
    public float PreviousOverlapY;
    public Vector2 Position;
    public Vector2 Size;
}