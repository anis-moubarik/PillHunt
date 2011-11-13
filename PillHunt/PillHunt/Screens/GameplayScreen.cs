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
        SpriteFont gameFont;

        Texture2D awesomeFace;
        Texture2D pill;
        Texture2D dimmer;
        Texture2D gameOver;
        SpriteFont font;

        Player player1;
        Timer clock;

        int frameCounter;
        int frameTime;
        int currentFrameRate;

        Dictionary<Pill, Rectangle> pillerList;
        List<Pill> toBeRemoved;

        bool endGame = false;
        float pauseAlpha;

        InputAction pauseAction;


        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            pauseAction = new InputAction(
                new Buttons[] { Buttons.Start, Buttons.Back },
                new Keys[] { Keys.Escape },
                true);

            player1 = new Player();
            clock = new Timer();
        }

        public void createPills()
        {

            Random random = new Random();
            int maxX = ScreenManager.GraphicsDevice.Viewport.Width - pill.Width;
            int maxY = ScreenManager.GraphicsDevice.Viewport.Height - pill.Height;

            pillerList = new Dictionary<Pill, Rectangle>();

            for (int i = 0; i < 100; i++)
            {
                pillerList.Add(new Pill(), new Rectangle(random.Next(maxX), random.Next(maxY), 32, 32));
            }
        }


        public override void Activate(bool instancePreserved)
        {
            if (!instancePreserved)
            {
                if (content == null)
                    content = new ContentManager(ScreenManager.Game.Services, "Content");

                gameFont = content.Load<SpriteFont>("gamefont");
                awesomeFace = content.Load<Texture2D>("awesome");
                pill = content.Load<Texture2D>("pill");
                font = content.Load<SpriteFont>("FPS");
                dimmer = content.Load<Texture2D>("dimmer");
                gameOver = content.Load<Texture2D>("gameover");
                createPills();

                // A real game would probably have more content than this sample, so
                // it would take longer to load. We simulate that by delaying for a
                // while, giving you a chance to admire the beautiful loading screen.
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



        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
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

                toBeRemoved = new List<Pill>();

                KeyboardState keyState = Keyboard.GetState();

                if (keyState.IsKeyDown(Keys.Escape))
                {
                    
                }

                if (!endGame)
                {
                    keyState = movement(keyState);


                    frameCounter++;
                    frameTime += gameTime.ElapsedGameTime.Milliseconds;

                    if (frameTime >= 1000)
                    {
                        currentFrameRate = frameCounter;
                        frameTime = 0;
                        frameCounter = 0;
                    }


                    foreach (KeyValuePair<Pill, Rectangle> pair in pillerList)
                    {
                        //awesomePos.Width += 1;
                        //awesomePos.Height += 1;
                        if (player1.getPosition().Intersects(pair.Value))
                        {
                            player1.increaseScore();
                            toBeRemoved.Add(pair.Key);
                        }
                    }

                    foreach (Pill pill in toBeRemoved)
                    {

                        pillerList.Remove(pill);

                    }

                }
            }
        }

        private KeyboardState movement(KeyboardState keyState)
        {

            //liike nopeutuu jos nappia painettu
            if (keyState.GetPressedKeys().Length > 0)
            {
                if (keyState.IsKeyDown(Keys.W))
                {
                    player1.changeSpeedY(-2);
                }
                if (keyState.IsKeyDown(Keys.S))
                {
                    player1.changeSpeedY(2);
                }
                if (keyState.IsKeyDown(Keys.A))
                {
                    player1.changeSpeedX(-2);
                }
                if (keyState.IsKeyDown(Keys.D))
                {
                    player1.changeSpeedX(2);
                }
            } //muuten hidastuu
            else
            {
                player1.slowDown();
            }

            int maxX = ScreenManager.GraphicsDevice.Viewport.Width - awesomeFace.Width;
            int maxY = ScreenManager.GraphicsDevice.Viewport.Height - awesomeFace.Height;
            int minY = 0;
            int minX = 0;

            player1.move(maxX, maxY, minX, minY);

            return keyState;
        }


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

            ScreenManager.GraphicsDevice.Clear(Color.LightBlue);

            Rectangle dim = new Rectangle(0, 0, 800, 600);
            int maxWidth = ScreenManager.GraphicsDevice.Viewport.Width;
            int maxHeight = ScreenManager.GraphicsDevice.Viewport.Height;


            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);


            if (endGame)
            {
                //Vector2 mid;
                //mid.X = ScreenManager.GraphicsDevice.Viewport.Width / 2;
                //mid.Y = ScreenManager.GraphicsDevice.Viewport.Height / 2;
                //spriteBatch.Draw(dimmer, dim, new Color(new Vector4(1f, 1f, 1f, 0.5f)));


                //spriteBatch.Draw(gameOver, mid, null, Color.White, 0f, new Vector2((float)gameOver.Width / 2, (float)gameOver.Height / 2), 1f, SpriteEffects.None, 0f);
                //spriteBatch.DrawString(font, "Score: " + player1.getScore(), new Vector2(mid.X + 20, mid.Y + 20), Color.White);
                //spriteBatch.DrawString(font, "Game Over", new Vector2(mid.X - 50, mid.Y - 50), Color.White);
                Vector2 mid = new Vector2((float)ScreenManager.GraphicsDevice.Viewport.Width / 2, (float)ScreenManager.GraphicsDevice.Viewport.Height / 2);
                Vector2 goPos = new Vector2((float)gameOver.Width / 2, (float)gameOver.Height / 2);

                spriteBatch.Draw(dimmer, dim, new Color(new Vector4(1f, 1f, 1f, 0.5f)));
                spriteBatch.Draw(gameOver, mid, null, Color.White, 0f, goPos, 1f, SpriteEffects.None, 1f);
                spriteBatch.DrawString(font, "Game Over", new Vector2(mid.X - 50, mid.Y - 50), Color.Black);
                spriteBatch.DrawString(font, "Score: " + player1.getScore(), new Vector2(mid.X - 50, mid.Y - 20), Color.Black);


            }
            if (!endGame)
            {
                foreach (KeyValuePair<Pill, Rectangle> pair in pillerList)
                {
                    pair.Key.draw(spriteBatch, pill, pair.Value);
                }
            }

            clock.draw(spriteBatch, font);
            player1.draw(spriteBatch, awesomeFace);

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
