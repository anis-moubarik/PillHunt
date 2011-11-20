using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Linq;

namespace PillHunt
{

    public class ScreenManager : DrawableGameComponent
    {
        //Game should be able to handle crashing game and come to the last screen we were
        //not sure it works though.
        private const string StateFilename = "ScreenManagerState.xml";

        //List of screens we use
        List<GameScreen> screens = new List<GameScreen>();
        List<GameScreen> tempScreensList = new List<GameScreen>();



        SpriteBatch spriteBatch;
        SpriteFont font;
        Texture2D blankTexture;
        InputState input = new InputState();

        bool isInitialized;



        // A default SpriteBatch shared by all the screens. This saves
        // each screen having to bother creating their own local instance.
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        // A default font shared by all the screens. This saves
        // each screen having to bother loading their own local copy.
        public SpriteFont Font
        {
            get { return font; }
        }


        public Texture2D BlankTexture
        {
            get { return blankTexture; }
        }


        public ScreenManager(Game game)
            : base(game)
        {

        }

        public override void Initialize()
        {
            base.Initialize();

            isInitialized = true;
        }


        protected override void LoadContent()
        {
            ContentManager content = Game.Content;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = content.Load<SpriteFont>("menufont");
            blankTexture = content.Load<Texture2D>("blank");

            //As we load we activate our screens
            foreach (GameScreen screen in screens)
            {
                screen.Activate(false);
            }
        }


        protected override void UnloadContent()
        {
            foreach (GameScreen screen in screens)
            {
                screen.Unload();
            }
        }


        public override void Update(GameTime gameTime)
        {

            input.Update();
            tempScreensList.Clear();

            foreach (GameScreen screen in screens)
                tempScreensList.Add(screen);

            bool otherScreenHasFocus = !Game.IsActive;
            bool coveredByOtherScreen = false;

            while (tempScreensList.Count > 0)
            {
                GameScreen screen = tempScreensList[tempScreensList.Count - 1];

                tempScreensList.RemoveAt(tempScreensList.Count - 1);

                // Update the screen.
                screen.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

                if (screen.ScreenState == ScreenState.TransitionOn ||
                    screen.ScreenState == ScreenState.Active)
                {
                    // If this is the first active screen we came across,
                    // give it a chance to handle input.
                    if (!otherScreenHasFocus)
                    {
                        screen.HandleInput(gameTime, input);

                        otherScreenHasFocus = true;
                    }

                    // If this is an active non-popup, inform any subsequent
                    // screens that they are covered by it.
                    if (!screen.IsPopup)
                        coveredByOtherScreen = true;
                }
            }

        }


        public override void Draw(GameTime gameTime)
        {
            foreach (GameScreen screen in screens)
            {
                if (screen.ScreenState == ScreenState.Hidden)
                    continue;

                screen.Draw(gameTime);
            }
        }


        public void AddScreen(GameScreen screen, PlayerIndex? controllingPlayer)
        {
            screen.ControllingPlayer = controllingPlayer;
            screen.ScreenManager = this;
            screen.IsExiting = false;

            // If we have a graphics device, tell the screen to load content.
            if (isInitialized)
            {
                screen.Activate(false);
            }

            screens.Add(screen);
        }


        public void RemoveScreen(GameScreen screen)
        {
            // If we have a graphics device, tell the screen to unload content.
            if (isInitialized)
            {
                screen.Unload();
            }

            screens.Remove(screen);
            tempScreensList.Remove(screen);

        }



        public GameScreen[] GetScreens()
        {
            return screens.ToArray();
        }


        public void FadeBackBufferToBlack(float alpha)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(blankTexture, GraphicsDevice.Viewport.Bounds, Color.Black * alpha);
            spriteBatch.End();
        }

        public void Deactivate()
        {
            return;

        }

        public bool Activate(bool instancePreserved)
        {
            return false;
        }

    }
   }