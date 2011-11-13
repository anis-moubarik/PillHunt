using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PillHunt
{
    class Timer
    {

        private double time;
        private Vector2 timeVector; //location of the timer


        public Timer()
        {
            time = 8.0f;
            timeVector = new Vector2(0, 0);
        }


        public double getTime()
        {
            return time;
        }

        public void decreaseTime(double decrease)
        {
            time = time - decrease;
        }

        //resets the timer
        public void zero()
        {
            time = 0.0f;
        }

        //draws the timer to the left corner of the screen
        public void draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, "Time: " + Math.Round(time), timeVector, Color.White);
        }


    }

}
