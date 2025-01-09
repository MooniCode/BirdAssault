using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyFinalProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject
{
    internal class SpriteSheetAnimation : SpriteSheet
    {
        private int currentFrame;
        public int AnimationDelay { get; set; }
        public int MinimumSpriteIndex { get; set; }
        public int MaximumSpriteIndex { get; set; } // this is useful if you have multiple animations in one sprite sheet

        public SpriteSheetAnimation(Texture2D texture, Vector2 topLeftPosition, Vector2 size, int rows, int columns, int currentSpriteIndex, float rotation, int animationDelay, int minimumSpriteIndex, int maximumSpriteIndex) : base(texture, topLeftPosition, size, rows, columns, currentSpriteIndex, rotation)
        {
            AnimationDelay = animationDelay;
            MinimumSpriteIndex = minimumSpriteIndex;
            MaximumSpriteIndex = maximumSpriteIndex;
        }
        public override void Update()
        {
            currentFrame++;
            if (currentFrame > AnimationDelay)
            {
                currentFrame = 0;
                CurrentSpriteIndex++;
                if (CurrentSpriteIndex > MaximumSpriteIndex)
                {
                    CurrentSpriteIndex = MinimumSpriteIndex;
                }
            }

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int sourceRow = CurrentSpriteIndex / Columns;
            int sourceColumn = CurrentSpriteIndex % Columns;

            int spriteWidth = Texture.Width / Columns;
            int spriteHeight = Texture.Height / Rows;

            Rectangle sourceRectangle = new Rectangle(spriteWidth * sourceColumn, spriteHeight * sourceRow, spriteWidth, spriteHeight);
            Rectangle dr = DestinationRectangle;
            Rectangle drMiddle = new Rectangle(dr.X + dr.Width / 2, dr.Y + dr.Height / 2, dr.Width, dr.Height);
            Vector2 sourceSize = new Vector2(SourceRectangle.Width, SourceRectangle.Height);
            Vector2 sourceOrigin = sourceSize / 2;
            //spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.Draw(Texture, drMiddle, sourceRectangle, Color.White, Rotation, sourceOrigin, SpriteEffects.None, 0);
        }
    }
}
