using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PillHunt
    {
    class Scores

        {

        private Vector2 p1;
        private Vector2 p2;

        public Scores(int screenWidth)
            {
            p1 = new Vector2(screenWidth - 135, 2);
            p2 = new Vector2(screenWidth - 135, 22);
            }

        //draws the scores of given players with given sprite batch and font
        public void draw(SpriteBatch spriteBatch, SpriteFont font, Player player1, Player player2)
            {
            spriteBatch.DrawString(font, player1.getName() + ": " + player1.getScore(), p1, Color.White);
            spriteBatch.DrawString(font, player2.getName() + ": " + player2.getScore(), p2, Color.White);
            }

        }
    }
