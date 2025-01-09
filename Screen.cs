using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject
{
    internal abstract class Screen
    {
        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        internal virtual void UpdateScreen()
        {
            if (UserInput.WasKeyPressed(Keys.Add))
            {
                GameSettings._MusicVolume += 0.1f;
                if (GameSettings._MusicVolume > 1f) GameSettings._MusicVolume = 1f;
            }
            else if (UserInput.WasKeyPressed(Keys.Subtract))
            {
                GameSettings._MusicVolume -= 0.1f;
                if (GameSettings._MusicVolume < 0f) GameSettings._MusicVolume = 0f;
            }
        }
    }
}
