using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject
{
    internal class FlyImage : GameObject
    {
        public FlyImage(Vector2 velocity, SpriteSheet spriteSheet) : base(velocity, spriteSheet)
        {
        }

        public override void Update()
        {
            base.Update();
            if (Size.X <= 600) SpriteSheet.Size = Size * 1.2f;
            TopLeftPosition = new Vector2((GameSettings._windowSize.X - SpriteSheet.Size.X) / 2, (GameSettings._windowSize.Y - SpriteSheet.Size.Y) / 2);
        }
    }
}
