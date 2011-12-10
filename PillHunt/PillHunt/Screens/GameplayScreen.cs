using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace PillHunt
    {

    class GameplayScreen : GameScreen
        {

        ContentManager content;
        InputAction pauseAction;
        InputAction endGameAction;

        //sounds
        SoundEffect nom;
        Song bgMusic;

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
        PlayerControls controls;
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

            //sets the escape-key as the pause button and enter as the restart game button
            pauseAction = new InputAction(new Keys[] { Keys.Escape }, true);
            endGameAction = new InputAction(new Keys[] { Keys.Enter }, true);


            // --- n‰‰ vois jotenkin antaa jatkossa parametreina:
            screenWidth = 1024;
            screenHeight = 768;
            string p1name = "Player1";
            string p2name = "Player2";
            string mapName = "map.txt";
            // ---

            clock = new Timer(20.0f);
            fps = new FPS(screenWidth);
            map = new Map(mapName, screenWidth, screenHeight);
            player1 = new Player(0, 0, screenWidth, screenHeight, p1name, map);
            player2 = new Player(screenWidth - 32, screenHeight - 32, screenWidth, screenHeight, p2name, map);
            pills = new Pills(map, 100, screenWidth, screenHeight, 32);
            scores = new Scores(screenWidth);
            controls = new PlayerControls();
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
                nom = content.Load<SoundEffect>("nom");
                bgMusic = content.Load<Song>("daymare");
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(bgMusic);
                MediaPlayer.Volume = 0.000f; //0.085f;
                SoundEffect.MasterVolume = 0.00f; //0.09f;
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


        //input handling
        public override void HandleInput(GameTime gameTime, InputState input)
            {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];

            PlayerIndex player;
            if (pauseAction.Evaluate(input, ControllingPlayer, out player) && !gameEnds)
                {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
                }
            if (endGameAction.Evaluate(input, ControllingPlayer, out player) && gameEnds)
                {
                ScreenManager.AddScreen(new RestartScreen(), ControllingPlayer);
                }
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
                    fps.calculateFPS(gameTime);
                    controls.checkKeyboardStatus(Keyboard.GetState(), player1, player2);
                    player1.moveTowardsDirection();
                    player1.increaseScore(pills.countIntersections(player1.getPosition(), nom));
                    player2.increaseScore(pills.countIntersections(player2.getPosition(), nom));
                    }

                }
            }


        public override void Draw(GameTime gameTime)
            {

            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            ScreenManager.GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            Rectangle fullscreen = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Draw(bgTexture, fullscreen, Color.White);

            map.draw(spriteBatch, wallTexture, gameEnds, IsActive, player1);
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
