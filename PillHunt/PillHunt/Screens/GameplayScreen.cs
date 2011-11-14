using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace PillHunt
{

    class GameplayScreen : GameScreen
    {

        ContentManager content;
        InputAction pauseAction;

        SpriteFont gameFont;
        SpriteFont font;

        Texture2D awesomeFace;
        Texture2D pill;
        Texture2D dimmer;
        Texture2D gameOver;
        Texture2D bg;

        Player player1;
        Player player2;
        Timer clock;
        FPS fps;

        Dictionary<Pill, Rectangle> pills;
        List<Pill> toBeRemoved;

        bool endGame;
        float pauseAlpha;


        public GameplayScreen()
        {

            //times for the transition
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            //sets the escape-key as the pause-button
            pauseAction = new InputAction(new Keys[] { Keys.Escape }, true);

            player1 = new Player(0, 0);
            player2 = new Player(300, 300);
            clock = new Timer();
            fps = new FPS(800);

            endGame = false;

        }

        //creates all the pills to the game and adds them and their (random) locations to a dictionary
        public void createPills()
        {

            Random random = new Random();
            int maxX = ScreenManager.GraphicsDevice.Viewport.Width - pill.Width;
            int maxY = ScreenManager.GraphicsDevice.Viewport.Height - pill.Height;
            pills = new Dictionary<Pill, Rectangle>();

            for (int i = 0; i < 100; i++)
            {
                pills.Add(new Pill(), new Rectangle(random.Next(maxX), random.Next(maxY), 32, 32));
            }
        }

        //activates the game by loading all the needed images etc.
        public override void Activate(bool instancePreserved)
            {

            if (!instancePreserved)
                {

                if (content == null)
                    {
                    content = new ContentManager(ScreenManager.Game.Services, "Content");
                    }
                
                gameFont = content.Load<SpriteFont>("gamefont");
                awesomeFace = content.Load<Texture2D>("awesome");
                pill = content.Load<Texture2D>("pill");
                font = content.Load<SpriteFont>("FPS");
                dimmer = content.Load<Texture2D>("dimmer");
                gameOver = content.Load<Texture2D>("gameover");
                bg = content.Load<Texture2D>("bg");
                createPills();

                Thread.Sleep(1000);

                ScreenManager.Game.ResetElapsedTime();

                }

            }


        public override void Deactivate()
        {
            base.Deactivate();
        }


        public override void Unload()
        {
            content.Unload();
        }


        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                if (clock.getTime() > 0)
                {
                    clock.decreaseTime(gameTime.ElapsedGameTime.TotalSeconds);
                }

                else
                {
                    clock.zero();
                    endGame = true;
                }

                if (!endGame)
                {

                    toBeRemoved = new List<Pill>();
                    KeyboardState keyState = Keyboard.GetState();

                    movement(keyState);
                    fps.calculateFPS(gameTime);

                    //checks if either of the players intersects with a piller
                    //and if they do then the player gets a score and the piller is removed
                    foreach (KeyValuePair<Pill, Rectangle> pair in pills)
                    {
                        if (player1.getPosition().Intersects(pair.Value))
                        {
                            player1.increaseScore();
                            toBeRemoved.Add(pair.Key);
                        }

                        if (player2.getPosition().Intersects(pair.Value))
                        {
                            player2.increaseScore();
                            toBeRemoved.Add(pair.Key);
                        }
                    }

                    //removes the eaten pillers
                    foreach (Pill pill in toBeRemoved)
                    {
                        pills.Remove(pill);
                    }

                }
            }
        }

        private void movement(KeyboardState keyState)
        {

        //players move faster if keys are pressed, otherwise they slow down

        Boolean playerOne = false;
        Boolean playerTwo = false;

        //player 1 movement
        if (keyState.IsKeyDown(Keys.W))
            {
            player1.changeSpeedY(-2);
            playerOne = true;
            }
        if (keyState.IsKeyDown(Keys.S))
            {
            player1.changeSpeedY(2);
            playerOne = true;
            }
        if (keyState.IsKeyDown(Keys.A))
            {
            player1.changeSpeedX(-2);
            playerOne = true;
            }
        if (keyState.IsKeyDown(Keys.D))
            {
            player1.changeSpeedX(2);
            playerOne = true;
            }

        //player 2 movement
        if (keyState.IsKeyDown(Keys.Up))
            {
            player2.changeSpeedY(-2);
            playerTwo = true;
            }
        if (keyState.IsKeyDown(Keys.Down))
            {
            player2.changeSpeedY(2);
            playerTwo = true;
            }
        if (keyState.IsKeyDown(Keys.Left))
            {
            player2.changeSpeedX(-2);
            playerTwo = true;
            }
        if (keyState.IsKeyDown(Keys.Right))
            {
            player2.changeSpeedX(2);
            playerTwo = true;
            }

        //possible slow downs
        if (!playerOne)
            {
            player1.slowDown();
            }
        if (!playerTwo)
            {
            player2.slowDown();
            }

            int maxX = ScreenManager.GraphicsDevice.Viewport.Width - awesomeFace.Width;
            int maxY = ScreenManager.GraphicsDevice.Viewport.Height - awesomeFace.Height;
            int minY = 0;
            int minX = 0;

            //if players intersect with each other, they bounce away
            if (player1.getPosition().Intersects(player2.getPosition()))
            {
                player1.bounceX();
                player1.bounceY();
                player2.bounceX();
                player2.bounceY();
            }

            //the actual moving happens here
            player1.move(maxX, maxY, minX, minY);
            player2.move(maxX, maxY, minX, minY);

        }

        //input handling
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];

            PlayerIndex player;
            if (pauseAction.Evaluate(input, ControllingPlayer, out player))
            {

                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
        }



        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            ScreenManager.GraphicsDevice.Clear(Color.White);

            Rectangle dim = new Rectangle(0, 0, 800, 600);
            int maxWidth = ScreenManager.GraphicsDevice.Viewport.Width;
            int maxHeight = ScreenManager.GraphicsDevice.Viewport.Height;

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            spriteBatch.Draw(bg, new Vector2(0, 0), Color.White);

            if (!endGame)
            {
                foreach (KeyValuePair<Pill, Rectangle> pair in pills)
                {
                    pair.Key.draw(spriteBatch, pill, pair.Value);
                }
            }

            
            clock.draw(spriteBatch, font);
            player1.draw(spriteBatch, awesomeFace, Color.Yellow);
            player2.draw(spriteBatch, awesomeFace, Color.Red);
            fps.draw(spriteBatch, font);


            if (endGame)
            {
                Vector2 mid = new Vector2((float)ScreenManager.GraphicsDevice.Viewport.Width / 2, (float)ScreenManager.GraphicsDevice.Viewport.Height / 2);
                Vector2 goPos = new Vector2((float)gameOver.Width / 2, (float)gameOver.Height / 2);
                spriteBatch.Draw(dimmer, dim, new Color(new Vector4(1f, 1f, 1f, 0.5f)));
                spriteBatch.Draw(gameOver, mid, null, Color.White, 0f, goPos, 1f, SpriteEffects.None, 1f);
                spriteBatch.DrawString(font, "Game Over", new Vector2(mid.X - 50, mid.Y - 50), Color.Black);
                spriteBatch.DrawString(font, "Player 1 score: " + player1.getScore(), new Vector2(mid.X - 50, mid.Y - 20), Color.Black);
                spriteBatch.DrawString(font, "Player 2 score: " + player2.getScore(), new Vector2(mid.X - 50, mid.Y - 0), Color.Black);

            }


            spriteBatch.DrawString(font, "Player 1: " + player1.getScore(), new Vector2(maxWidth - 100, 20), Color.White);
            spriteBatch.DrawString(font, "Player 2: " + player2.getScore(), new Vector2(maxWidth - 100, 40), Color.White);


            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }

        

    }
}
