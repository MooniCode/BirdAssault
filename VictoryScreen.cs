using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject
{
    internal class VictoryScreen : TextureScreen
    {
        public VictoryScreen()
        {
            Texture = GameSettings.VictoryScreenTexture;
            GameSettings.IsGamePlaying = false;
        }

        internal override void UpdateScreen()
        {
            base.UpdateScreen();
            if (UserInput.WasKeyPressed(Keys.Enter))
            {
                GameSettings.SelectSound.Play(GameSettings._MusicVolume, 0, 0);
                GameSettings.CurrentScreen = new StartupScreen();
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
            spriteBatch.DrawString(GameSettings.SmallTitleFont, $"Score: {GameSettings.Player.Worms}", new Vector2(GameSettings._windowSize.X * 0.445f, GameSettings._windowSize.Y * 0.40f), Color.White);
            spriteBatch.DrawString(GameSettings.SmallTitleFont, $"Highscore: {GameSettings.HighScore}", new Vector2(GameSettings._windowSize.X * 0.395f, GameSettings._windowSize.Y * 0.50f), Color.White);
        }
    }
}
