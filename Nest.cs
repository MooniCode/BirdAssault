using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject
{
    internal class Nest : GameObject
    {
        public Nest(Vector2 velocity, SpriteSheet spriteSheet) : base(velocity, spriteSheet)
        {
        }

        public override void Update()
        {
            base.Update();
            if (IsCollidingWithOtherCenter(GameSettings.Player))
            {
                MediaPlayer.Stop();
                GameSettings.WinnerSound.Play(GameSettings._MusicVolume, 0, 0);
                GameSettings.NestReached = true;
                GameSettings.CurrentScreen = new VictoryScreen();
            }
        }
    }
}
