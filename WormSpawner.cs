using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject
{
    internal class WormSpawner
    {
        private int wormSpawnTimer = 200;
        private List<Worm> _worms = new List<Worm>();
        private Nest nest;
        private Random _random = new Random();

        public void Update()
        {
            for (int i = 0; i < _worms.Count; i++)
            {
                _worms[i].Update();
                if (_worms[i].IsCollidingWithOtherCenter(GameSettings.Player))
                {
                    GameSettings.PickupSound.Play(GameSettings._MusicVolume, 0, 0);
                    GameSettings.Player.Worms++;
                    if (GameSettings.Player.Worms > 4) GameSettings._difficulty += 0.1f;
                    _worms.Remove(_worms[i]);
                    i--;
                    if (GameSettings.Player.Worms == 10)
                    {
                        Vector2 pos = GameSettings.Grid.getRandomCell().TopLeftPosition;
                        while (GameSettings.Player.TopLeftPosition.Equals(pos)
                            || isAlreadyOccupied(pos)) pos = GameSettings.Grid.getRandomCell().TopLeftPosition;
                        SpriteSheet sheet = new SpriteSheetAnimation(GameSettings.NestTextureSheet, pos, new Vector2(GameSettings._cellWidth, GameSettings._cellHeight), 1, 4, 0, 0, 5, 0, 3);
                        nest = new Nest(new Vector2(0, 0), sheet);
                    }
                }
            }
            if (nest != null) nest.Update();
            if (GameSettings.IsGamePlaying)
            {
                wormSpawnTimer--;
                if (wormSpawnTimer <= 0 && _worms.Count < 3) spawnWorm();
            }
        }

        public void DrawWorms(SpriteBatch spriteBatch)
        {
            foreach (Worm o in _worms) o.Draw(spriteBatch);
            if (nest != null) nest.Draw(spriteBatch);
        }

        private void spawnWorm()
        {
            Vector2 pos = GameSettings.Grid.getRandomCell().TopLeftPosition;
            while(GameSettings.Player.TopLeftPosition.Equals(pos)
                || isAlreadyOccupied(pos)) pos = GameSettings.Grid.getRandomCell().TopLeftPosition;
            SpriteSheet sheet = new SpriteSheetAnimation(GameSettings.WormTextureSheet, pos, new Vector2(GameSettings._cellWidth, GameSettings._cellHeight), 1, 8, 0, 0, 10, 0, 7);
            _worms.Add(new Worm(new Vector2(0, 0), sheet));
            wormSpawnTimer = 500;
        }

        private bool isAlreadyOccupied(Vector2 pos)
        {
            foreach(Worm w in _worms)
            {
                if (w.TopLeftPosition.Equals(pos)) return true;
            }
            if(nest != null && nest.TopLeftPosition.Equals(pos)) return true;   
            return false;
        }
    }
}
