using MyFinalProject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFinalProject
{
    internal class TextureScreen : Screen
    {
        public Texture2D Texture { get; set; }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(Texture, new Rectangle(0, 0, (int)GameSettings._windowSize.X, (int)GameSettings._windowSize.Y), Color.White);

        }

        internal override void UpdateScreen()
        {
            base.UpdateScreen();
        }
    }
}