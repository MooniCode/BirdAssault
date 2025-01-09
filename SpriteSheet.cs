using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject
{
    internal class SpriteSheet
    {
        public Texture2D Texture { get; set; }
        public Vector2 TopLeftPosition { get; set; }
        public Vector2 Size { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int CurrentSpriteIndex { get; set; }
        public float Rotation { get; set; }



        public SpriteSheet(Texture2D texture, Vector2 topLeftPosition, Vector2 size, int rows, int columns, int currentSpriteIndex, float rotation)
        {
            Texture = texture;
            TopLeftPosition = topLeftPosition;
            Size = size;
            Rows = rows;
            Columns = columns;
            CurrentSpriteIndex = currentSpriteIndex;
            Rotation = rotation;
        }

        public Rectangle DestinationRectangle
        {
            get
            {
                return new Rectangle((int)TopLeftPosition.X, (int)TopLeftPosition.Y, (int)Size.X, (int)Size.Y);
            }
        }
        public Rectangle Center
        {
            get
            {
                return new Rectangle((int)(TopLeftPosition.X + Size.X / 2), (int)(TopLeftPosition.Y + Size.Y / 2), 1, 1);
            }
        }
        public virtual Rectangle HitBox { get { return DestinationRectangle; } }
        public Rectangle SourceRectangle
        {
            get
            {
                float spriteWidth = Texture.Width / Columns;
                float spriteHeight = Texture.Height / Rows;
                int spriteColumn = CurrentSpriteIndex % Columns;
                int spriteRow = CurrentSpriteIndex / Columns;
                return new Rectangle((int)(spriteColumn * spriteWidth), (int)(spriteRow * spriteHeight), (int)spriteWidth, (int)spriteHeight);
            }
        }
        public virtual void Update()
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Rectangle dr = DestinationRectangle;
            Rectangle drMiddle = new Rectangle(dr.X + dr.Width / 2, dr.Y + dr.Height / 2, dr.Width, dr.Height); // we calculate the middle of the rectangle to make it rotate inside of the destination rectangle.
            Vector2 sourceSize = new Vector2(SourceRectangle.Width, SourceRectangle.Height); // we get the size of the texture.
            Vector2 sourceOrigin = sourceSize / 2; // we calculate the middle of the texture to make it rotate around the middle of the texture.
            spriteBatch.Draw(Texture, drMiddle, null, Color.White, Rotation, sourceOrigin, SpriteEffects.None, 0);
        }
    }
}
