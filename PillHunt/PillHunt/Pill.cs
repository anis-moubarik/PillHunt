using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
