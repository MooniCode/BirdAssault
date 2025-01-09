using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject
{
    internal class TetrisObstacle : Obstacle
    {
        public List<TetrisObstaclePiece> _obstaclePieces = new List<TetrisObstaclePiece>();

        private Random _random = new Random();
        private List<int> _usedXPositions = new List<int>();
        private List<int> _usedYPositions = new List<int>();

        public TetrisObstacle()
        {
            int pieceCount = 4;
            int startingLocation = _random.Next(0, 11);
            GenerateObstacle(startingLocation, pieceCount);
            while (IsCollidingWith(GameSettings.ObstacleSpawner._obstacles))
            {
                _obstaclePieces.Clear();
                _usedXPositions.Clear();
                _usedYPositions.Clear();
                startingLocation = _random.Next(0, 11);
                GenerateObstacle(startingLocation, pieceCount);
            }
        }

        public override void Update()
        {
            for (int i = 0; i < _obstaclePieces.Count; i++)
            {
                _obstaclePieces[i].Update();
                if (_obstaclePieces[i].IsCollidingWithOtherCenter(GameSettings.Player))
                {
                    GameSettings.HurtSound.Play(GameSettings._MusicVolume, 0, 0);
                    GameSettings.Player.Health--;
                    _obstaclePieces.Clear();
                    IsActive = false;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (TetrisObstaclePiece p in _obstaclePieces) p.Draw(spriteBatch);
        }

        public void GenerateObstacle(int startingLocation, int pieceCount)
        {
            for (int i = 0; i < pieceCount; i++)
            {
                int x = startingLocation;
                int y = 0;
                if (i != 0) DecideVerticalOrHorizontalShift(ref x, ref y);
                _usedXPositions.Add(x);
                _usedYPositions.Add(y);
                GenerateTetrisObstaclePiece(x, y);
            }
        }

        private void DecideVerticalOrHorizontalShift(ref int x, ref int y)
        {
            int VerticalOrHorizontal = _random.Next(0, 2);
            if (VerticalOrHorizontal == 0)
            {
                x = ShiftHorizontally(x);
                while (_usedXPositions.Contains(x) || x > 10 || x < 0)
                {
                    x = ShiftHorizontally(x);
                }
            }
            else
            {
                y = ShiftVertically(y);
                while (_usedYPositions.Contains(y) || y > 10 || y < 0)
                {
                    y = ShiftVertically(y);
                }
            }
        }

        private int ShiftVertically(int y)
        {
            int upOrDown = _random.Next(0, 2);
            if (upOrDown == 0)
            {
                y -= 1;
            }
            else
            {
                y += 1;
            }

            return y;
        }

        private int ShiftHorizontally(int x)
        {
            int leftOrRight = _random.Next(0, 2);
            if (leftOrRight == 0)
            {
                x -= 1;
            }
            else
            {
                x += 1;
            }

            return x;
        }

        private void GenerateTetrisObstaclePiece(int x, int y)
        {
            SpriteSheet tetrisPieceSheet = new SpriteSheetAnimation(GameSettings.TetrisObstaclePieceTexture, new Vector2(x * GameSettings._cellWidth + GameSettings._gridPaddingX, y * GameSettings._cellHeight - 300), new Vector2(GameSettings._cellWidth, GameSettings._cellHeight), 1, 17, 0, 0, 10, 0, 16);
            TetrisObstaclePiece piece = new TetrisObstaclePiece(new Vector2(0, 0.5f) * GameSettings._difficulty, tetrisPieceSheet);

            _obstaclePieces.Add(piece);
        }

        public bool IsCollidingWith(List<Obstacle> obstacles)
        {
            foreach (Obstacle obstacle in obstacles)
            {
                if (obstacle is TetrisObstacle o)
                {
                    foreach (TetrisObstaclePiece p1 in _obstaclePieces)
                    {
                        foreach (TetrisObstaclePiece p2 in o._obstaclePieces)
                        {
                            if (p1.IsCollidingWith(p2))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}
