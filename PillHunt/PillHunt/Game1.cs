using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PillHunt
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D awesomeFace;
        Texture2D pill;
        SpriteFont font;
        Rectangle awesomePos = new Rectangle(0, 0, 32, 32);
        Rectangle pillPos = new Rectangle(100, 10, 32, 32);
        Vector2 origin = Vector2.Zero;
        int frameCounter;
        int frameTime;
        int currentFrameRate;
        int score = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 640;
            graphics.PreferredBackBufferWidth = 800;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            awesomeFace = Content.Load<Texture2D>("Awesome");
            pill = Content.Load<Texture2D>("pill");
            font = Content.Load<SpriteFont>("FPS");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState keyState = Keyboard.GetState();
            // TODO: Add your update logic here
            int MaxX = graphics.GraphicsDevice.Viewport.Width - awesomeFace.Width;
            int MaxY = graphics.GraphicsDevice.Viewport.Height - awesomeFace.Height;
            int MinY = 0, MinX = 0;

            if (keyState.IsKeyDown(Keys.W) && awesomePos.Y > MinY)
            {
                awesomePos.Y -= 5;
            }
            if (keyState.IsKeyDown(Keys.S) && awesomePos.Y < MaxY)
            {
                awesomePos.Y += 5;
            }
            if (keyState.IsKeyDown(Keys.A) && awesomePos.X > MinX)
            {
                awesomePos.X -= 5;
            }
            if (keyState.IsKeyDown(Keys.D) && awesomePos.X < MaxX)
            {
                awesomePos.X += 5;
            }

            frameCounter++;
            frameTime += gameTime.ElapsedGameTime.Milliseconds;
            if (frameTime >= 1000)
            {
                currentFrameRate = frameCounter;
                frameTime = 0;
                frameCounter = 0;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        Pill piller = new Pill();
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);
            // TODO: Add your drawing code here

            int maxWidth = graphics.GraphicsDevice.Viewport.Width;
            int maxHeight = graphics.GraphicsDevice.Viewport.Height;

            



            spriteBatch.Begin();
            if(piller.alive)
                piller.Draw(spriteBatch, pill, pillPos);
            spriteBatch.DrawString(font, "FPS: " + currentFrameRate, new Vector2(maxWidth-60, 0), Color.Black);
            spriteBatch.DrawString(font, "Score: " + score, new Vector2(maxWidth - 70, 20), Color.Black);
            spriteBatch.Draw(awesomeFace, awesomePos, Color.White);
            if (awesomePos.Intersects(pillPos))
            {
                piller.alive = false;
                score++;
            }
            spriteBatch.End();
            base.Draw(gameTime);
            
        }
    }
}
