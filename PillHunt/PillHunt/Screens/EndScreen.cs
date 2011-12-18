using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PillHunt
    {
    class EndScreen
        {

        Player winner;
        Player loser;
        bool tie;

        //creates a new endscreen
        public EndScreen(Player player1, Player player2)
            {
            tie = false;
            whoWon(player1, player2);
            }

        //checks who won the game
        private void whoWon(Player player1, Player player2)
            {

            //player1 wins
            if (player1.getScore() > player2.getScore())
                {
                winner = player1;
                loser = player2;
                }

            //player2 wins
            else if (player2.getScore() > player1.getScore())
                {
                winner = player2;
                loser = player1;
                }

            //the game ends in a tie
            else
                {
                tie = true;
                winner = player1;
                }

            }


        //draws the end screen
        public void draw(SpriteBatch spriteBatch, SpriteFont font, Texture2D goTexture, Texture2D dimmerTexture, int screenWidth, int screenHeight)
            {

            Rectangle dim = new Rectangle(0, 0, screenWidth, screenHeight);
            Vector2 mid = new Vector2(screenWidth / 2, screenHeight / 2);
            Vector2 goPos = new Vector2(goTexture.Width / 2, goTexture.Height / 2);

            spriteBatch.Draw(dimmerTexture, dim, new Color(new Vector4(1f, 1f, 1f, 0.5f)));
            spriteBatch.Draw(goTexture, mid, null, Color.White, 0f, goPos, 1f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(font, "Game Over", new Vector2(mid.X - 50, mid.Y - 50), Color.Black);

            if (tie)
                {
                spriteBatch.DrawString(font, "The game ended in a tie!", new Vector2(mid.X - 110, mid.Y - 20), Color.Black);
                spriteBatch.DrawString(font, "Both players ate " + winner.getScore() + " pills.", new Vector2(mid.X - 105, mid.Y), Color.Black);
                }

            else
                {
                spriteBatch.DrawString(font, winner.getName() + " ate " + winner.getScore() + " pills and won!", new Vector2(mid.X - 110, mid.Y - 20), Color.Black);
                spriteBatch.DrawString(font, loser.getName() + " ate only " + loser.getScore() + " pills.", new Vector2(mid.X - 110, mid.Y), Color.Black);
                }

            spriteBatch.DrawString(font, "Press Enter to continue", new Vector2(mid.X - 95, mid.Y + 26), Color.Red);

            }

        }
    }
