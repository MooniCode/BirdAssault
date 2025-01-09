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
    internal class GameObject
    {
        public SpriteSheet SpriteSheet { get; set; }
        public Vector2 TopLeftPosition
        {
            get { return SpriteSheet.TopLeftPosition; }
            set { SpriteSheet.TopLeftPosition = value; }

        }
        public Vector2 Velocity
        {
            get;
            set;
        }
        public float Rotation
        {
            get { return SpriteSheet.Rotation; }
            set { SpriteSheet.Rotation = value; }

        }
        public Vector2 Size
        {
            get { return SpriteSheet.Size; }
            private set { SpriteSheet.Size = value; }

        }
        public Texture2D Texture
        {
            get { return SpriteSheet.Texture; }
            private set { SpriteSheet.Texture = value; }

        }

        public Rectangle DestinationRectangle
        {
            get
            {
                return SpriteSheet.DestinationRectangle;
            }
        }

        public Rectangle HitBox
        {
            get
            {
                return SpriteSheet.HitBox;
            }
        }

        public Rectangle Center
        {
            get { return SpriteSheet.Center; }
        }

        public bool IsActive { get; set; } = true;

        public bool IsOutOfBounds
        {
            get
            {
                return TopLeftPosition.X < 0 || TopLeftPosition.X > GameSettings._windowSize.X || TopLeftPosition.Y + Size.Y < -600 || TopLeftPosition.Y > GameSettings._windowSize.Y;
            }
        }

        public GameObject(Vector2 velocity, SpriteSheet spriteSheet)
        {
            SpriteSheet = spriteSheet;
            Velocity = velocity;
        }

        public GameObject()
        {
        }

        public virtual void Update()
        {
            if(GameSettings.IsGamePlaying)
            {
                TopLeftPosition += Velocity;
                if (IsOutOfBounds)
                {
                    IsActive = false;
                }
            }
            SpriteSheet.Update();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                SpriteSheet.Draw(spriteBatch);
            }
        }
        public bool IsCollidingWith(GameObject otherGameObject)
        {
            if (!IsActive || !otherGameObject.IsActive)
            {
                return false;
            }
            return HitBox.Intersects(otherGameObject.HitBox);
        }

        public bool IsCollidingWithOtherCenter(GameObject otherGameObject)
        {
            if (!IsActive || !otherGameObject.IsActive)
            {
                return false;
            }
            return Center.Intersects(otherGameObject.HitBox);
        }

    }
}
