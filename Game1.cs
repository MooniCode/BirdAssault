using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using SharpDX.Direct2D1;
using System;
using System.Diagnostics;
using System.Threading;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using System.Linq;

namespace MyFinalProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //ScreenSize
            _graphics.PreferredBackBufferWidth = (int)GameSettings._windowSize.X;
            _graphics.PreferredBackBufferHeight = (int)GameSettings._windowSize.Y;
        }

        protected override void Initialize()
        {
            base.Initialize();
            if (!File.Exists(@"highscore.txt"))
            {
                File.Create(@"highscore.txt").Close();
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Grid
            GameSettings.Cell1Texture = Content.Load<Texture2D>("Images/Tile1");
            GameSettings.Cell2Texture = Content.Load<Texture2D>("Images/Tile2");

            //Screens
            GameSettings.StartScreenTexture = Content.Load<Texture2D>("Images/StartScreen");
            GameSettings.PauseScreenTexture = Content.Load<Texture2D>("Images/PauseScreenTexture");
            GameSettings.GameOverScreenTexture = Content.Load<Texture2D>("Images/GameOverScreen");
            GameSettings.RestartScreenTexture = Content.Load<Texture2D>("Images/RestartScreenTexture");
            GameSettings.VictoryScreenTexture = Content.Load<Texture2D>("Images/VictoryScreenTexture");

            //Overlays
            GameSettings.DarkOverlay = Content.Load<Texture2D>("Images/DarkOverlay");
            GameSettings.GameScreenOverlay = Content.Load<Texture2D>("Images/GameScreenOverlay");

            //Player, Enemies & objects
            GameSettings.PlayerTexture = Content.Load<Texture2D>("Images/BirbSheet2");
            GameSettings.TetrisObstaclePieceTexture = Content.Load<Texture2D>("Images/HappyCloudSheet");
            GameSettings.FalconTextureSheet = Content.Load<Texture2D>("Images/FalconTextureSheet");
            GameSettings.FlyTextureSheet = Content.Load<Texture2D>("Images/FlyTextureSheet");
            GameSettings.FlyImageTextureSheet = Content.Load<Texture2D>("Images/FlyImageTextureSheet2");
            GameSettings.WormTextureSheet = Content.Load<Texture2D>("Images/WormTextureSheet");
            GameSettings.NestTextureSheet = Content.Load<Texture2D>("Images/NestTextureSheet");

            //UI
            GameSettings.wasdKeys = Content.Load<Texture2D>("Images/wasdKeys");
            GameSettings.PButton = Content.Load<Texture2D>("Images/PButton");
            GameSettings.RButton = Content.Load<Texture2D>("Images/RButton");
            GameSettings.SubtractButton = Content.Load<Texture2D>("Images/SubtractButton");
            GameSettings.PlusButton = Content.Load<Texture2D>("Images/PlusButton");

            //SoundEffects
            GameSettings.PickupSound = Content.Load<SoundEffect>("Sound/PickupSound");
            GameSettings.HurtSound = Content.Load<SoundEffect>("Sound/HurtSound");
            GameSettings.BackSound = Content.Load<SoundEffect>("Sound/BackSound");
            GameSettings.FlySound = Content.Load<SoundEffect>("Sound/FlySound");
            GameSettings.SelectSound = Content.Load<SoundEffect>("Sound/SelectSound");
            GameSettings.DeadSound = Content.Load<SoundEffect>("Sound/DeadSound");
            GameSettings.WinnerSound = Content.Load<SoundEffect>("Sound/WinnerSound");

            //Music
            MediaPlayer.Volume = GameSettings._MusicVolume;
            GameSettings.GameMusic = Content.Load<Song>("Sound/GameMusic");

            //Fonts
            GameSettings.DefaultFont = Content.Load<SpriteFont>("Fonts/DefaultFont");
            GameSettings.TitleFont = Content.Load<SpriteFont>("Fonts/TitleFont");
            GameSettings.SmallTitleFont = Content.Load<SpriteFont>("Fonts/SmallTitleFont");
            GameSettings.ClockFont = Content.Load<SpriteFont>("Fonts/ClockFont");

            using (StreamReader sr = new StreamReader(@"highscore.txt"))
            {
                GameSettings.HighScore = Int32.Parse(File.ReadAllText(@"highscore.txt"));
                //GameSettings.HighScore = Int32.Parse(File.ReadLines(@"highscore.txt").Skip(1).Take(1).First());
            }

            GameSettings.gridSheet = new SpriteSheet(GameSettings.GridTexture, new Vector2(GameSettings._windowSize.X / 2 - GameSettings._gridSize.X / 2, GameSettings._windowSize.Y / 2 - GameSettings._gridSize.Y / 2), GameSettings._gridSize, 1, 1, 0, 0);
            GameSettings.Grid = new Grid(Vector2.Zero, GameSettings.gridSheet, 11, 11);
            GameSettings.playerSheet = new SpriteSheetAnimation(GameSettings.PlayerTexture, new Vector2(0, 0), new Vector2(GameSettings._cellWidth, GameSettings._cellHeight), 1, 15, 0, 0, 1, 0, 14);

            GameSettings.CurrentScreen = new StartupScreen();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            GameSettings.CurrentScreen.Update(gameTime);
            UserInput.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);

            _spriteBatch.Begin();
            GameSettings.CurrentScreen.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}