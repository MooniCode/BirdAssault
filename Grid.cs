using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace MyFinalProject
{
    internal class Grid : GameObject
    {
        private List<Cell> _cells = new List<Cell>();
        public int Rows { get; set; }
        public int Columns { get; set; }
        private bool _isCell2 = false;
        private bool _isFinished = false;
        public Grid(Vector2 velocity, SpriteSheet grid, int rows, int columns) : base(velocity, grid)
        {
            Rows = rows;
            Columns = columns;
        }

        public void DrawGrid(SpriteBatch spriteBatch)
        {
            if (!_isFinished)
            {
                for (int row = 0; row < Rows; row++)
                {
                    for (int column = 0; column < Columns; column++)
                    {
                        if (_isCell2)
                        {
                            Cell cell = new Cell(Vector2.Zero, new SpriteSheet(GameSettings.Cell1Texture, new Vector2(column * GameSettings._cellWidth + GameSettings._gridPaddingX, row * GameSettings._cellHeight + GameSettings._gridPaddingY), new Vector2(GameSettings._cellWidth, GameSettings._cellHeight), 1, 1, 0, 0));
                            _cells.Add(cell);                                                                                  
                            _isCell2 = false;
                        }
                        else if (!_isCell2)
                        {
                            Cell cell = new Cell(Vector2.Zero, new SpriteSheet(GameSettings.Cell2Texture, new Vector2(column * GameSettings._cellWidth + GameSettings._gridPaddingX, row * GameSettings._cellHeight + GameSettings._gridPaddingY), new Vector2(GameSettings._cellWidth, GameSettings._cellHeight), 1, 1, 0, 0));
                            _cells.Add(cell);
                            _isCell2 = true;
                        }
                    }
                }
            }
            _isFinished = true;
            foreach (Cell cell in _cells)
            {
                cell.Draw(spriteBatch);
            }
        }

        public Cell getRandomCell()
        {
            return _cells[new Random().Next(0, _cells.Count)];
        }
    }
}
