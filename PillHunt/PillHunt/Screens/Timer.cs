﻿using System;
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
        private Vector2 timeVector;


        public Timer()
        {
            time = 15.0f;
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

        public void zero()
        {
            time = 0.0f;
        }

        public void draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, "Time: " + Math.Round(time), timeVector, Color.Black);
        }


    }

}