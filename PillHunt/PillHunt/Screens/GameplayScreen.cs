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

        Texture2D awesomeTexture;
        Texture2D pillTexture;
        Texture2D dimmerTexture;
        Texture2D goTexture;
        Texture2D bgTexture;

        Player player1;
        Player player2;
        Pills pills;
        Timer clock;
        FPS fps;

        bool gameEnds;
        float pauseAlpha;

        int screenWidth;
        int screenHeight;


        public GameplayScreen()
        {

            //times for the transition
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            //sets the escape-key as the pause-button
            pauseAction = new InputAction(new Keys[] { Keys.Escape }, true);

            screenWidth = 800;
            screenHeight = 600;

            player1 = new Player(0, 0);
            player2 = new Player(screenWidth - 32, screenHeight - 32);
            clock = new Timer();
            fps = new FPS(screenWidth);
            pills = new Pills(100, screenWidth, screenHeight, 32);

            gameEnds = false;

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
                awesomeTexture = content.Load<Texture2D>("awesome");
                pillTexture = content.Load<Texture2D>("pill");
                font = content.Load<SpriteFont>("FPS");
                dimmerTexture = content.Load<Texture2D>("dimmer");
                goTexture = content.Load<Texture2D>("gameover");
                bgTexture = content.Load<Texture2D>("bg");

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

                //game ends if the time runs out or all the pills are eaten

                if (clock.getTime() > 0 && !pills.isEmpty())
                {
                    clock.decreaseTime(gameTime.ElapsedGameTime.TotalSeconds);
                }
                
                else
                {
                    gameEnds = true;
                }


                if (!gameEnds)

                {

                    KeyboardState keyState = Keyboard.GetState();

                    movement(keyState);
                    fps.calculateFPS(gameTime);

                    //counts new scores for both players and removes all the eaten pills
                    player1.increaseScore(pills.countIntersections(player1.getPosition()));
                    player2.increaseScore(pills.countIntersections(player2.getPosition()));

                }

            }
        }

        // --- movementille oma luokka ---
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

            int maxX = ScreenManager.GraphicsDevice.Viewport.Width - awesomeTexture.Width;
            int maxY = ScreenManager.GraphicsDevice.Viewport.Height - awesomeTexture.Height;
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

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            spriteBatch.Draw(bgTexture, new Vector2(0, 0), Color.White);
                
            pills.draw(spriteBatch, pillTexture);        
            clock.draw(spriteBatch, font);
            player1.draw(spriteBatch, awesomeTexture, Color.Yellow);
            player2.draw(spriteBatch, awesomeTexture, Color.Red);
            fps.draw(spriteBatch, font);

            spriteBatch.DrawString(font, "Player 1: " + player1.getScore(), new Vector2(screenWidth - 100, 20), Color.White);
            spriteBatch.DrawString(font, "Player 2: " + player2.getScore(), new Vector2(screenWidth - 100, 40), Color.White);


            if (gameEnds)
            {
                Rectangle dim = new Rectangle(0, 0, screenWidth, screenHeight);
                Vector2 mid = new Vector2((float)ScreenManager.GraphicsDevice.Viewport.Width / 2, (float)ScreenManager.GraphicsDevice.Viewport.Height / 2);
                Vector2 goPos = new Vector2((float)goTexture.Width / 2, (float)goTexture.Height / 2);
                spriteBatch.Draw(dimmerTexture, dim, new Color(new Vector4(1f, 1f, 1f, 0.5f)));
                spriteBatch.Draw(goTexture, mid, null, Color.White, 0f, goPos, 1f, SpriteEffects.None, 1f);
                spriteBatch.DrawString(font, "Game Over", new Vector2(mid.X - 50, mid.Y - 50), Color.Black);
                spriteBatch.DrawString(font, "Player 1 score: " + player1.getScore(), new Vector2(mid.X - 50, mid.Y - 20), Color.Black);
                spriteBatch.DrawString(font, "Player 2 score: " + player2.getScore(), new Vector2(mid.X - 50, mid.Y - 0), Color.Black);

            }

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
