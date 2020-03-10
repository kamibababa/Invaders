﻿using Invaders.GameStates;
using Microsoft.Xna.Framework;
using System.Linq;

namespace Invaders.Components
{
    class Shot : Sprite
    {
        public int Age;
        int counter;
        Sprite sprite;

        public Shot(string animKey, Vector2 position, PlayingState playingState)
        : base(animKey, 0x10000, position, playingState)
        {
            counter = 3;
        }

        protected override void Update(GameTime gametime)
        {
            if (counter-- > 0)
                return;
            counter = 3;
            Age++;
            Position.Y += 4;
            base.Update(gametime);
            if ((sprite = ((PlayingState)GameState).Barricades.FirstOrDefault(o => IntersectPixels(o))) != null)
            {
                OnExplode(sprite);
                OnRemove();
            }
            else
            if ((sprite = ((PlayingState)GameState).Bullets.FirstOrDefault(o => IntersectPixels(o))) != null)
            {
                OnExplode(sprite);
                OnRemove();
            }
            else
            if ((sprite = ((PlayingState)GameState).Explosions.FirstOrDefault(o => o.Name != "shotexplosion" && IntersectPixels(o))) != null)
            {
                OnExplode(sprite);
                OnRemove();
            }
            else
            if (IntersectPixels(((PlayingState)GameState).Player))
            {
                ((PlayingState)GameState).Player.Explode(this);
                OnRemove(); ;
            }
            if (Position.Y >= MainGame.LineY - 8)
            {
                OnExplode(((PlayingState)GameState).Line);
                OnRemove();
            }
        }
    }
}
