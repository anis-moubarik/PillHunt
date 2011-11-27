using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PillHunt
{
    class Timer
    {

        private double time;
        private Vector2 position;

        //creates a new timer
        public Timer(double t)
        {
            time = t;
            position = new Vector2(0, 0);
        }

        public double getTime()
        {
            return time;
        }

        public void decreaseTime(double decrease)
        {
            time = time - decrease;
        }

        //draws the timer
        public void draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, "Time: " + Math.Round(time), position, Color.White);
        }

    }

}
