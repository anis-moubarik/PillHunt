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

        private Rectangle position;

        //creates a new pill to given position
        public Pill(Rectangle pos)
        {
        position = pos;
        }

        //returns pill's position
        public Rectangle getPosition()
            {
            return position;
            }

        //draws the pill using the given spritebatch and texture
        public void draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
