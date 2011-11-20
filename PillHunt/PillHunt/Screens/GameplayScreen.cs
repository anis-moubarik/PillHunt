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

        //fonts
        SpriteFont gameFont;
        SpriteFont font;

        //textures
        Texture2D awesomeTexture;
        Texture2D pillTexture;
        Texture2D dimmerTexture;
        Texture2D goTexture;
        Texture2D bgTexture;
        Texture2D wallTexture;


        //our own classes
        Player player1;
        Player player2;
        Pills pills;
        Timer clock;
        FPS fps;
        Scores scores;
        PlayerMovement movement;
        Map map;


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

            // --- n‰‰ 7 vois jotenkin antaa jatkossa parametreina (texture sizet vois ehk‰ poistaa):
            screenWidth = 1024;
            screenHeight = 768;
            string p1name = "Player1";
            string p2name = "Player2";
            int playerTextureSize = 32;
            int pillTextureSize = 32;
            string mapName = "map.txt";

            player1 = new Player(0, 0, p1name);
            player2 = new Player(screenWidth - playerTextureSize, screenHeight - playerTextureSize, p2name);
            clock = new Timer();
            fps = new FPS(screenWidth);
            map = new Map(mapName, screenWidth, screenHeight);
            pills = new Pills(map, 100, screenWidth, screenHeight, pillTextureSize);
            scores = new Scores(screenWidth);
            movement = new PlayerMovement(screenWidth, screenHeight, playerTextureSize, playerTextureSize);
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
                wallTexture = content.Load<Texture2D>("wall");

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
            if (coveredByOtherScreen) { pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1); }
            else { pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0); }


            if (IsActive)

            {

                //game ends if the time runs out or all the pills are eaten
                if (clock.getTime() > 0 && !pills.isEmpty())
                {
                    clock.decreaseTime(gameTime.ElapsedGameTime.TotalSeconds);
                }
                
                else { gameEnds = true; }

                if (!gameEnds)

                {
                    movement.moveBothPlayers(Keyboard.GetState(), player1, player2, map);
                    fps.calculateFPS(gameTime);
                    player1.increaseScore(pills.countIntersections(player1.getPosition()));
                    player2.increaseScore(pills.countIntersections(player2.getPosition()));
                }

            }
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

            Rectangle fullscreen = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Draw(bgTexture, fullscreen, Color.White);

            map.draw(spriteBatch, wallTexture);
            pills.draw(spriteBatch, pillTexture);        
            clock.draw(spriteBatch, font);
            player1.draw(spriteBatch, awesomeTexture, Color.Yellow);
            player2.draw(spriteBatch, awesomeTexture, Color.Red);
            fps.draw(spriteBatch, font);
            scores.draw(spriteBatch, font, player1, player2);

            if (gameEnds)
            {
                EndScreen endscreen = new EndScreen(player1, player2);
                endscreen.draw(spriteBatch, font, goTexture, dimmerTexture, screenWidth, screenHeight);
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
