using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject
{
    internal class Cell : GameObject
    {
        public Cell(Vector2 velocity, SpriteSheet cell) : base(velocity, cell)
        {
        }
    }
}
