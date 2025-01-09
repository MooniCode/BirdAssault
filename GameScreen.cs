using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace MyFinalProject
{
    internal class GameScreen : Screen
    {
        public int FlyTimer = 0;
        private FlyImage Fly = null;
        public GameScreen()
        {
            GameSettings.IsGamePlaying = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            GameSettings.Player.Update();
            GameSettings.ObstacleSpawner.Update();
            GameSettings.WormSpawner.Update();
            MediaPlayer.Volume = GameSettings._MusicVolume;
            if (FlyTimer <= 0) Fly = null;
            if (FlyTimer > 0)
            {
                FlyTimer--;
                if (Fly == null)
                {
                    SpriteSheet flyimagesheet = new SpriteSheetAnimation(GameSettings.FlyImageTextureSheet, new Vector2(GameSettings._gridPaddingX + GameSettings._cellWidth * 5, GameSettings._gridPaddingY + GameSettings._cellHeight * 5), new Vector2(GameSettings._cellWidth, GameSettings._cellHeight), 1, 2, 0, 0, 5, 0, 1);
                    Fly = new FlyImage(new Vector2(0, 0), flyimagesheet);
                } else
                {
                    Fly.Update();
                }
            }
            CheckPlayerBoundaries();
            WriteHighScore();
            UpdateScreen();
            Runtimer();
            BeginMessage();
        }

        private void BeginMessage()
        {
            GameSettings.BeginTimer++;
        }

        private void Runtimer()
        {
            if (GameSettings.IsGamePlaying)
            {
                GameSettings.Stopwatch.Start();
            }
        }

        private void WriteHighScore()
        {
            int highScore = Math.Max(GameSettings.HighScore, GameSettings.Player.Worms);
            using (StreamWriter sw = new StreamWriter(@"highscore.txt"))
            {
                sw.WriteLine(highScore);
            }

            if (GameSettings.Player.Worms > GameSettings.HighScore && GameSettings.NestReached)
            {
                GameSettings.HighScore = GameSettings.Player.Worms;
            }
        }

        internal override void UpdateScreen()
        {
            base.UpdateScreen();
            if (GameSettings.Player.Health < 1)
            {
                GameSettings.DeadSound.Play(GameSettings._MusicVolume, 0, 0);
                MediaPlayer.Stop();
                GameSettings.CurrentScreen = new GameOverScreen();
            }
            else if (UserInput.WasKeyPressed(Keys.R))
            {
                if (GameSettings.RestartGame)
                {
                    GameSettings.BackSound.Play(GameSettings._MusicVolume, 0, 0);
                    GameSettings.RestartGame = false;
                    GameSettings.IsGamePlaying = false;
                }
                else
                {
                    GameSettings.SelectSound.Play(GameSettings._MusicVolume, 0, 0);
                    GameSettings.RestartGame = true;
                    GameSettings.IsGamePlaying = true;
                    GameSettings.PrepareNewGame();
                }
            } else if (UserInput.WasKeyPressed(Keys.Back))
            {
                GameSettings.RestartGame = true;
                GameSettings.IsGamePlaying = true;
            }
            else if (UserInput.WasKeyPressed(Keys.P))
            {
                if (GameSettings.IsGamePaused)
                {
                    GameSettings.BackSound.Play(GameSettings._MusicVolume, 0, 0);
                    GameSettings.IsGamePaused = false;
                    GameSettings.IsGamePlaying = false;
                }
                else
                {
                    GameSettings.SelectSound.Play(GameSettings._MusicVolume, 0, 0);
                    GameSettings.IsGamePaused = true;
                    GameSettings.IsGamePlaying = true;
                }
            }

        }
        private void CheckPlayerBoundaries()
        {
            if (GameSettings.Player.TopLeftPosition.X < GameSettings.Grid.TopLeftPosition.X)
            {
                GameSettings.Player.TopLeftPosition = new Vector2(GameSettings.Grid.TopLeftPosition.X, GameSettings.Player.TopLeftPosition.Y);
            }
            if (GameSettings.Player.TopLeftPosition.Y < GameSettings.Grid.TopLeftPosition.Y)
            {
                GameSettings.Player.TopLeftPosition = new Vector2(GameSettings.Player.TopLeftPosition.X, GameSettings.Grid.TopLeftPosition.Y);
            }
            if (GameSettings.Player.TopLeftPosition.X + GameSettings.Player.Size.X > GameSettings.Grid.TopLeftPosition.X + GameSettings.Grid.Size.X)
            {
                GameSettings.Player.TopLeftPosition = new Vector2(GameSettings.Grid.TopLeftPosition.X + GameSettings.Grid.Size.X - GameSettings.Player.Size.X, GameSettings.Player.TopLeftPosition.Y);
            }
            if (GameSettings.Player.TopLeftPosition.Y + GameSettings.Player.Size.Y > GameSettings.Grid.TopLeftPosition.Y + GameSettings.Grid.Size.Y)
            {
                GameSettings.Player.TopLeftPosition = new Vector2(GameSettings.Player.TopLeftPosition.X, GameSettings.Grid.TopLeftPosition.Y + GameSettings.Grid.Size.Y - GameSettings.Player.Size.Y);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
           
            GameSettings.Grid.DrawGrid(spriteBatch);
            base.Draw(spriteBatch);

            GameSettings.Player.Draw(spriteBatch);


            GameSettings.WormSpawner.DrawWorms(spriteBatch);
            GameSettings.ObstacleSpawner.DrawObstacles(spriteBatch);
            if(Fly != null) Fly.Draw(spriteBatch);

            spriteBatch.Draw(GameSettings.GameScreenOverlay, new Rectangle(0, 0, (int)GameSettings._windowSize.X, (int)GameSettings._windowSize.Y), Color.White);

            //Stats
            spriteBatch.DrawString(GameSettings.SmallTitleFont, $"Stats:", new Vector2(GameSettings._windowSize.X * 0.025f, GameSettings._windowSize.Y * 0.2f), Color.Black);
            spriteBatch.DrawString(GameSettings.DefaultFont, $"Lives: {GameSettings.Player.Health}", new Vector2(GameSettings._windowSize.X * 0.025f, GameSettings._windowSize.Y * 0.28f), Color.DarkSlateGray);
            spriteBatch.DrawString(GameSettings.DefaultFont, $"Worms: {GameSettings.Player.Worms}", new Vector2(GameSettings._windowSize.X * 0.025f, GameSettings._windowSize.Y * 0.33f), Color.DarkSlateGray);
            spriteBatch.DrawString(GameSettings.DefaultFont, $"Highscore: {GameSettings.HighScore}", new Vector2(GameSettings._windowSize.X * 0.025f, GameSettings._windowSize.Y * 0.38f), Color.DarkSlateGray);

            //Settings
            spriteBatch.DrawString(GameSettings.SmallTitleFont, $"Settings:", new Vector2(GameSettings._windowSize.X * 0.025f, GameSettings._windowSize.Y * 0.5f), Color.Black);
            spriteBatch.DrawString(GameSettings.DefaultFont, $"Audio: {Math.Round(GameSettings._MusicVolume * 10)}", new Vector2(GameSettings._windowSize.X * 0.025f, GameSettings._windowSize.Y * 0.58f), Color.DarkSlateGray);
            
            //Time
            string timeString = $"{GameSettings.Stopwatch.Elapsed:mm\\:ss}";
            spriteBatch.DrawString(GameSettings.ClockFont, timeString, new Vector2(GameSettings._windowSize.X * 0.05f, GameSettings._windowSize.Y * 0.080f), Color.White);

            //Controls
            spriteBatch.DrawString(GameSettings.SmallTitleFont, $"Controls:", new Vector2(GameSettings._windowSize.X * 0.81f, GameSettings._windowSize.Y * 0.2f), Color.Black);

            spriteBatch.DrawString(GameSettings.DefaultFont, "Movement:", new Vector2(GameSettings._windowSize.X * 0.81f, GameSettings._windowSize.Y * 0.28f), Color.DarkSlateGray);
            spriteBatch.Draw(GameSettings.wasdKeys, new Rectangle(1550, 350, 350, 110), Color.White);

            spriteBatch.DrawString(GameSettings.DefaultFont, "Pause:", new Vector2(GameSettings._windowSize.X * 0.81f, GameSettings._windowSize.Y * 0.48f), Color.DarkSlateGray);
            spriteBatch.Draw(GameSettings.PButton, new Rectangle(1675, 500, 60, 60), Color.White);

            spriteBatch.DrawString(GameSettings.DefaultFont, "Restart:", new Vector2(GameSettings._windowSize.X * 0.81f, GameSettings._windowSize.Y * 0.56f), Color.DarkSlateGray);
            spriteBatch.Draw(GameSettings.RButton, new Rectangle(1710, 590, 60, 60), Color.White);

            spriteBatch.DrawString(GameSettings.DefaultFont, "Volume up:", new Vector2(GameSettings._windowSize.X * 0.81f, GameSettings._windowSize.Y * 0.64f), Color.DarkSlateGray);
            spriteBatch.Draw(GameSettings.PlusButton, new Rectangle(1750, 675, 60, 60), Color.White);

            spriteBatch.DrawString(GameSettings.DefaultFont, "Volume down:", new Vector2(GameSettings._windowSize.X * 0.81f, GameSettings._windowSize.Y * 0.72f), Color.DarkSlateGray);
            spriteBatch.Draw(GameSettings.SubtractButton, new Rectangle(1790, 765, 60, 60), Color.White);

            if (GameSettings.BeginTimer > 50 && GameSettings.BeginTimer < 250)
            {
                spriteBatch.DrawString(GameSettings.DefaultFont, $"Collect Worms and avoid the Enemies!", new Vector2(GameSettings._windowSize.X * 0.325f, GameSettings._windowSize.X * 0.1f), Color.White);
            }

            if (!GameSettings.IsGamePaused)
            {
                GameSettings.Stopwatch.Stop();
                spriteBatch.Draw(GameSettings.DarkOverlay, new Rectangle(0, 0, (int)GameSettings._windowSize.X, (int)GameSettings._windowSize.Y), Color.White);
                String text1 = $"GAME IS PAUSED";
                String text2 = $"Press 'P' to unpause";
                spriteBatch.DrawString(GameSettings.TitleFont, text1, new Vector2((GameSettings._windowSize.X - GameSettings.TitleFont.MeasureString(text1).X) / 2, (GameSettings._windowSize.Y - GameSettings.TitleFont.MeasureString(text1).Y) / 2 - GameSettings.TitleFont.MeasureString(text1).Y / 2) , Color.White);
                spriteBatch.DrawString(GameSettings.TitleFont, text2, new Vector2((GameSettings._windowSize.X - GameSettings.TitleFont.MeasureString(text2).X) / 2, (GameSettings._windowSize.Y - GameSettings.TitleFont.MeasureString(text2).Y) / 2 + GameSettings.TitleFont.MeasureString(text2).Y / 2) , Color.White);
            }

            if (!GameSettings.RestartGame)
            {
                spriteBatch.Draw(GameSettings.DarkOverlay, new Rectangle(0, 0, (int)GameSettings._windowSize.X, (int)GameSettings._windowSize.Y), Color.White);
                String text1 = $"Restart game?";
                String text2 = $"Press 'R' to restart or 'Back' to cancel";
                spriteBatch.DrawString(GameSettings.TitleFont, text1, new Vector2((GameSettings._windowSize.X - GameSettings.TitleFont.MeasureString(text1).X) / 2, (GameSettings._windowSize.Y - GameSettings.TitleFont.MeasureString(text1).Y) / 2 - GameSettings.TitleFont.MeasureString(text1).Y / 2), Color.White);
                spriteBatch.DrawString(GameSettings.TitleFont, text2, new Vector2((GameSettings._windowSize.X - GameSettings.TitleFont.MeasureString(text2).X) / 2, (GameSettings._windowSize.Y - GameSettings.TitleFont.MeasureString(text2).Y) / 2 + GameSettings.TitleFont.MeasureString(text2).Y / 2), Color.White);
            }
        }
        public bool hasFlyImage()
        {
            return Fly != null;
        }
    }
}
