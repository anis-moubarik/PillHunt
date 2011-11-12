using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PillHunt
{
    class Pill
    {

        public Rectangle pillPos = new Rectangle(0, 0, 0, 0);
        private bool _alive;
        public bool alive
        {
            set { _alive = value; }
            get { return _alive; }
        }

        public Pill()
        {
            alive = true;
        }
        public void Draw(SpriteBatch spriteBatch, Texture2D pill, Rectangle pillPos)
        {
            this.pillPos = pillPos;
            if(alive)
                spriteBatch.Draw(pill, pillPos, Color.White);
        }

       

    }
}
