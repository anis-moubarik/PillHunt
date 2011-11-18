using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PillHunt
{
    class Wall
    {
        private Rectangle position;

        //creates a new wall to given position
        public Wall(Rectangle pos)
        {
        position = pos;
        }

        //returns wall's position
        public Rectangle getPosition()
            {
            return position;
            }

        //draws the wall using the given spritebatch and texture
        public void draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
