using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MyFinalProject;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject
{
    internal class StartupScreen : TextureScreen
    {
        public StartupScreen()
        {
            Texture = GameSettings.StartScreenTexture;
            GameSettings.IsGamePlaying = false;
            GameSettings.PrepareNewGame();
            MediaPlayer.Stop();
            GameSettings.Stopwatch.Stop();
        }

        internal override void UpdateScreen()
        {
            base.UpdateScreen();
            if (UserInput.WasKeyPressed(Keys.Enter))
            {
                GameSettings.SelectSound.Play(GameSettings._MusicVolume, 0, 0);
                GameSettings.CurrentScreen = new GameScreen();
                MediaPlayer.Play(GameSettings.GameMusic);
                GameSettings.Stopwatch.Start();
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            UpdateScreen();
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
