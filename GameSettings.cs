using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFinalProject
{
    internal class GameSettings
    {
        //WindowSize
        public static Vector2 _windowSize { get; set; } = new Vector2(1920, 1080);
        public static Vector2 _gridSize { get; set; } = new Vector2(880, 880);

        //Grid
        public static int _cellHeight { get; set; } = 80;
        public static int _cellWidth { get; set; } = 80;
        public static float _gridPaddingX { get; set; } = GameSettings._windowSize.X / 2 - GameSettings._gridSize.X / 2;
        public static float _gridPaddingY { get; set; } = GameSettings._windowSize.Y / 2 - GameSettings._gridSize.Y / 2;
        public static Texture2D Cell1Texture { get; set; }
        public static Texture2D Cell2Texture { get; set; }
        public static Texture2D GridTexture { get; set; }
        public static SpriteSheet gridSheet { get; set; }
        public static Grid Grid { get; set; }

        //Player, Enemies & Objects
        public static Player Player { get; set; }
        public static Texture2D PlayerTexture { get; set; }
        public static SpriteSheet playerSheet { get; set; }
        public static Texture2D FalconTextureSheet { get; set; }
        public static Texture2D FlyTextureSheet { get; set; }
        public static Texture2D FlyImageTextureSheet { get; set; }
        public static Texture2D WormTextureSheet { get; set; }
        public static Texture2D NestTextureSheet { get; set; }
        public static Texture2D TetrisObstaclePieceTexture { get; set; }
        public static ObstacleSpawner ObstacleSpawner { get; set; }
        public static WormSpawner WormSpawner { get; set; }
        public static bool NestReached { get; set; } = false;

        //Screens
        public static Screen CurrentScreen { get; set; }
        public static Texture2D StartScreenTexture { get; set; }
        public static Texture2D PauseScreenTexture { get; set; }
        public static Texture2D GameOverScreenTexture { get; set; }
        public static Texture2D RestartScreenTexture { get; set; }
        public static Texture2D VictoryScreenTexture { get; set; }

        //UI
        public static Texture2D wasdKeys { get; set; }
        public static Texture2D PButton { get; set; }
        public static Texture2D RButton { get; set; }
        public static Texture2D SubtractButton { get; set; }
        public static Texture2D PlusButton { get; set; }

        //SoundEffects
        public static SoundEffect PickupSound { get; set; }
        public static SoundEffect HurtSound { get; set; }
        public static SoundEffect FlySound { get; set; }
        public static SoundEffect SelectSound { get; set; }
        public static SoundEffect BackSound { get; set; }
        public static SoundEffect DeadSound { get; set; }
        public static SoundEffect WinnerSound { get; set; }

        //Music
        public static float _MusicVolume { get; set; } = 0.1f;
        public static Song GameMusic { get; set; }

        //Fonts
        public static SpriteFont SmallTitleFont { get; set; }
        public static SpriteFont TitleFont { get; set; }
        public static SpriteFont DefaultFont { get; set; }
        public static SpriteFont ClockFont { get; set; }

        //Overlays
        public static Texture2D DarkOverlay { get; set; }
        public static Texture2D GameScreenOverlay { get; set; }

        //Score

        public static int HighScore { get; set; }
        public static Stopwatch Stopwatch { get; private set; } = new Stopwatch();
        public static int BeginTimer { get; set; }

        //other
        public static bool IsGamePlaying { get; set; }
        public static bool IsGamePaused { get; set; } = true;
        public static bool RestartGame { get; set; } = true;
        public static float _difficulty { get; set; } = 1f;


        public static void PrepareNewGame()
        {
            MediaPlayer.Stop();
            MediaPlayer.Play(GameSettings.GameMusic);
            Player = new Player(new Vector2(0, 0), playerSheet, 3, new Vector2(_gridPaddingX + _cellWidth * 5, _gridPaddingY + _cellHeight * 5));
            ObstacleSpawner = new ObstacleSpawner();
            WormSpawner = new WormSpawner();
            _difficulty = 1f;
            Stopwatch = Stopwatch.StartNew();
            GameSettings.BeginTimer = 0;




        }
    }
}
