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

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D awesomeFace;
        Texture2D pill;
        SpriteFont font;
        Rectangle awesomePos = new Rectangle(0, 0, 32, 32);
        int frameCounter;
        int frameTime;
        int currentFrameRate;
        int score = 0;
        Dictionary<Pill, Rectangle> pillerList;
        List<Pill> toBeRemoved;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 640;
            graphics.PreferredBackBufferWidth = 800;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            awesomeFace = Content.Load<Texture2D>("Awesome");
            pill = Content.Load<Texture2D>("pill");
            font = Content.Load<SpriteFont>("FPS");
            createPills();
        }


        public void createPills()
            {

            Random random = new Random();
            int maxX = graphics.GraphicsDevice.Viewport.Width - pill.Width;
            int maxY = graphics.GraphicsDevice.Viewport.Height - pill.Height;

            pillerList = new Dictionary<Pill, Rectangle>();

            for (int i = 0; i < 20; i++)
                {
                pillerList.Add(new Pill(), new Rectangle(random.Next(maxX), random.Next(maxY), 32, 32));
                }
            }

        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {

            toBeRemoved = new List<Pill>();

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Escape))
                {
                this.Exit();
                }

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


            foreach (KeyValuePair<Pill, Rectangle> pair in pillerList)
                {
                if (awesomePos.Intersects(pair.Value))
                    {
                    score++;
                    toBeRemoved.Add(pair.Key);                
                    }
                }

            foreach (Pill pill in toBeRemoved)
                {

                pillerList.Remove(pill);
                
                }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);


            int maxWidth = graphics.GraphicsDevice.Viewport.Width;
            int maxHeight = graphics.GraphicsDevice.Viewport.Height;

            spriteBatch.Begin();
            
            spriteBatch.DrawString(font, "FPS: " + currentFrameRate, new Vector2(maxWidth-60, 0), Color.Black);
            spriteBatch.DrawString(font, "Score: " + score, new Vector2(maxWidth - 80, 20), Color.Black);
            spriteBatch.Draw(awesomeFace, awesomePos, Color.White);
            foreach (KeyValuePair<Pill, Rectangle> pair in pillerList)
                {
                pair.Key.draw(spriteBatch, pill, pair.Value);
                }


            spriteBatch.End();
            base.Draw(gameTime);
            
        }
    }
}
