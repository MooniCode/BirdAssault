using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject
{
    internal class FlyObstacle : Obstacle
    {
        public FlyObstacle(Vector2 velocity, SpriteSheet spriteSheet) : base(velocity, spriteSheet)
        {
            SetRotation();
            SetVelocity();
        }

        public override void Update()
        {
            Player player = GameSettings.Player;
            base.Update();
            if (GameSettings.IsGamePlaying)
            {
                if (IsCollidingWithOtherCenter(player))
                {
                    GameSettings.FlySound.Play(GameSettings._MusicVolume, 0, 0);
                    IsActive = false;
                    ((GameScreen) GameSettings.CurrentScreen).FlyTimer = 200;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

            base.Draw(spriteBatch);
        }
        public void SetVelocity()
        {
            Player player = GameSettings.Player;
            Vector2 targetPosition = player.TopLeftPosition;
            Vector2 toTarget = targetPosition - TopLeftPosition;
            float distance = toTarget.Length();
            if (distance != 0)
            {
                toTarget.Normalize();
            }
            float speed = 5f;
            Vector2 step = toTarget * speed;
            Velocity = step * GameSettings._difficulty;
        }

        public void SetRotation()
        {
            Vector2 targetPosition = GameSettings.Player.TopLeftPosition;
            Vector2 toTarget = targetPosition - TopLeftPosition;
            float rotation = (float)Math.Atan2(toTarget.Y, toTarget.X);
            Rotation = rotation + (float)Math.PI / 2;
        }
    }
}
