using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject
{
    internal class ObstacleSpawner
    {
        public List<Obstacle> _obstacles = new List<Obstacle>();
        private int tetrisTimer = 0;
        private int falconTimer = 400;
        private int flyTimer = 400;
        private Random _random = new Random();

        public void Update()
        {
            foreach (Obstacle o in _obstacles)
            {
                o.Update();
            }
            if (GameSettings.IsGamePlaying)
            {
                tetrisTimer--;
                if (GameSettings.Player.Worms >= 2) falconTimer--;
                if (GameSettings.Player.Worms >= 4) flyTimer--;
                if (tetrisTimer <= 0) spawnTetrisObstacle();
                if (falconTimer <= 0) spawnFalconObstacle();
                if (flyTimer <= 0) spawnFlyObstacle();
                for (int i = 0; i < _obstacles.Count; i++)
                {
                    if (!_obstacles[i].IsActive)
                    {
                        _obstacles.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        public void DrawObstacles(SpriteBatch spriteBatch)
        {
            foreach (Obstacle o in _obstacles) o.Draw(spriteBatch);
        }

        private void spawnTetrisObstacle()
        {
            _obstacles.Add(new TetrisObstacle());
            tetrisTimer = 300;
            if(GameSettings.Player.Worms >= 10)
            {
                tetrisTimer = 200;
            }
        }

        private void spawnFalconObstacle()
        {
            SpriteSheet falconsheet = new SpriteSheetAnimation(GameSettings.FalconTextureSheet, GetRandomPosTwoCellOutOfGrid(), new Vector2(GameSettings._cellWidth * 1.1f, GameSettings._cellHeight * 1.2f), 1, 15, 0, 0, 1, 0, 14);
            _obstacles.Add(new FalconObstacle(new Vector2(0, 0.5f) * GameSettings._difficulty, falconsheet));
            falconTimer = 400;
        }

        private void spawnFlyObstacle()
        {
            if (!((GameScreen)GameSettings.CurrentScreen).hasFlyImage())
            {
                SpriteSheet flysheet = new SpriteSheetAnimation(GameSettings.FlyTextureSheet, GetRandomPosTwoCellOutOfGrid(), new Vector2(GameSettings._cellWidth * 0.8f, GameSettings._cellHeight * 0.6f), 1, 8, 0, 0, 1, 0, 7);
                _obstacles.Add(new FlyObstacle(new Vector2(0, 0.5f) * GameSettings._difficulty, flysheet));
                flyTimer = 400;
            }
        }

        private Vector2 GetRandomPosTwoCellOutOfGrid()
        {
            int randchoice = _random.Next(4);
            int randpos = _random.Next(-1, 11);
            switch (randchoice)
            {
                case 0:
                    return new Vector2(GameSettings._gridPaddingX - 2 * GameSettings._cellWidth,
                                       GameSettings._gridPaddingY + randpos * GameSettings._cellHeight);
                case 1:
                    return new Vector2(GameSettings._gridPaddingX + randpos * GameSettings._cellWidth,
                                       GameSettings._gridPaddingY - 2 * GameSettings._cellHeight);
                case 2:
                    return new Vector2(GameSettings._gridPaddingX + 12 * GameSettings._cellWidth,
                                       GameSettings._gridPaddingY + randpos * GameSettings._cellHeight);
                case 3:
                    return new Vector2(GameSettings._gridPaddingX + randpos * GameSettings._cellWidth,
                                       GameSettings._gridPaddingY + 12 * GameSettings._cellHeight);
                default:
                    //never executed
                    return new Vector2();
            }
        }
    }
}
