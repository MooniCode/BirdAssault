using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject
{
    internal class Obstacle : GameObject
    {
        public Obstacle() { }
        public Obstacle(Vector2 velocity, SpriteSheet spriteSheet) : base(velocity, spriteSheet)
        {
        }
    }
}
