using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PillHunt
    {
    class FPS
        {

        private int frameCounter;
        private int frameTime;
        int currentFrameRate;
        private Vector2 vector;

        public FPS(int width)
            {
            frameCounter = 0;
            frameTime = 0;
            vector = new Vector2(width - 80, 0);
            }

        public void increaseCounter()
            {
            frameCounter++;
            }

        public void increaseTime(int increase)
            {
            frameTime = frameTime + increase;
            }

        public int getFrameTime()
            {
            return frameTime;
            }

        //sets the current fps to currentFrameRate and sets both frameTime and frameCounter to 0
        public void setFPS()
            {
            currentFrameRate = frameCounter;
            frameTime = 0;
            frameCounter = 0;
            }

        //draws the FPS to the top right corner using the given spritebatch and font
        public void draw(SpriteBatch spriteBatch, SpriteFont font)
            {
            spriteBatch.DrawString(font, "FPS: " + currentFrameRate, vector, Color.White);
            }

        }
    }
