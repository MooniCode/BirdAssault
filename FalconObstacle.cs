using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject
{
    internal class FalconObstacle : Obstacle
    {
        private int _maxTimeToLive = 1000;
        private int _timeToLive = 0;
        public FalconObstacle(Vector2 velocity, SpriteSheet spriteSheet) : base(velocity, spriteSheet) 
        {
            SetRotation();
        }
        public override void Update()
        {
            Player player = GameSettings.Player;
            base.Update();
            if (GameSettings.IsGamePlaying)
            {
                SetVelocity();
                SetRotation();
                DisableAfterTimeToLive();
                if (IsCollidingWithOtherCenter(player))
                {
                    GameSettings.HurtSound.Play(GameSettings._MusicVolume, 0, 0);
                    IsActive = false;
                    player.Health--;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

            base.Draw(spriteBatch);
        }

        private void DisableAfterTimeToLive()
        {
            _timeToLive++;
            if (_timeToLive >= _maxTimeToLive)
            {
                IsActive = false;
            }
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
            float speed = 1f;
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
