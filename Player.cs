using Microsoft.Xna.Framework;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace MyFinalProject
{
    internal class Player : GameObject
    {
        public int Health { get; set; }
        public int Worms { get; set; }
        public Player(Vector2 velocity, SpriteSheet playerSpriteSheet, int health, Vector2 startPosition) : base(velocity, playerSpriteSheet)
        {
            Health = health;
            TopLeftPosition = startPosition;
            Worms = 0;
        }
        public override void Update()
        {
            base.Update();
            if (GameSettings.IsGamePlaying) UpdatePosition();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        public void UpdatePosition()
        {
            if (UserInput.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.S) || (UserInput.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down)))
            {
                Velocity = new Vector2(0, GameSettings._cellHeight);
                Rotation = (float)Math.PI;
                //SlideToPosition(new Vector2(TopLeftPosition.X, TopLeftPosition.Y + GameSettings._cellHeight), spriteBatch);
            }
            else if (UserInput.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.Z) || (UserInput.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up)))
            {
                Velocity = new Vector2(0, -GameSettings._cellHeight);
                Rotation = 0;
            }
            else if (UserInput.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.Q) || (UserInput.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left)))
            {
                Velocity = new Vector2(-GameSettings._cellWidth, 0);
                double rotation = Math.PI * 1.5;
                Rotation = (float)rotation;
            }
            else if (UserInput.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.D) || (UserInput.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right)))
            {
                Velocity = new Vector2(GameSettings._cellWidth, 0);
                double rotation = Math.PI * 0.5;
                Rotation = (float)rotation;
            }
            else
            {
                Velocity = Vector2.Zero;
            }
        }

    }
}
