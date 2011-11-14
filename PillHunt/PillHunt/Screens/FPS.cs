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
        private int currentFrameRate;
        private Vector2 vector;

        //creates a new FPS object, requires width of the game window
        public FPS(int width)
            {
            frameCounter = 0;
            frameTime = 0;
            vector = new Vector2(width - 80, 0);
            }

        //calculates current frames per second
        public void calculateFPS(GameTime gameTime)
            {

            frameCounter++;
            frameTime = frameTime + gameTime.ElapsedGameTime.Milliseconds;

            if (frameTime >= 1000)
                {
                currentFrameRate = frameCounter;
                frameTime = 0;
                frameCounter = 0;
                }
            }

        //draws the FPS to the top right corner using the given spritebatch and font
        public void draw(SpriteBatch spriteBatch, SpriteFont font)
            {
            spriteBatch.DrawString(font, "FPS: " + currentFrameRate, vector, Color.White);
            }

        }

    }
