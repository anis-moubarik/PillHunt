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
        Texture2D dimmer;
        Texture2D gameOver;
        SpriteFont font;
        Rectangle awesomePos = new Rectangle(0, 0, 32, 32);
        int frameCounter;
        int frameTime;
        int currentFrameRate;
        int score = 0;
        Dictionary<Pill, Rectangle> pillerList;
        List<Pill> toBeRemoved;
        double _timer;
        Vector2 _timerVec;
        bool endGame = false;
        int speedX;
        int speedY;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            _timer = 5.0f;
            _timerVec = new Vector2(0, 0);
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
            dimmer = Content.Load<Texture2D>("dimmer");
            gameOver = Content.Load<Texture2D>("gameOver");
            createPills();
        }


        public void createPills()
            {

            Random random = new Random();
            int maxX = graphics.GraphicsDevice.Viewport.Width - pill.Width;
            int maxY = graphics.GraphicsDevice.Viewport.Height - pill.Height;

            pillerList = new Dictionary<Pill, Rectangle>();

            for (int i = 0; i < 10000; i++)
                {
                pillerList.Add(new Pill(), new Rectangle(random.Next(maxX), random.Next(maxY), 32, 32));
                }
            }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (_timer > 0)
            {
                _timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                _timer = 0;
                endGame = true;
            }
            toBeRemoved = new List<Pill>();

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            int MaxX = graphics.GraphicsDevice.Viewport.Width - awesomeFace.Width;
            int MaxY = graphics.GraphicsDevice.Viewport.Height - awesomeFace.Height;
            int MinY = 0, MinX = 0;

            if (!endGame)
            {
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
                if (keyState.GetPressedKeys().Length > 0)
                {
                    if (keyState.IsKeyDown(Keys.W))
                    {
                        speedY -= 2;
                    }
                    if (keyState.IsKeyDown(Keys.S))
                    {
                        speedY += 2;
                    }
                    if (keyState.IsKeyDown(Keys.A))
                    {
                        speedX -= 2;
                    }
                    if (keyState.IsKeyDown(Keys.D))
                    {
                        speedX += 2;
                    }
                }
                else
                {
                    if (speedX > 0)
                    {
                        speedX -= 1;
                    }
                    if (speedX < 0)
                    {
                        speedX += 1;
                    }

                    if (speedY > 0)
                    {
                        speedY -= 1;
                    }
                    if (speedY < 0)
                    {
                        speedY += 1;
                    }

                    if ((awesomePos.X + speedX) < MaxX && (awesomePos.X + speedX) > MinX)
                    {
                        awesomePos.X += speedX;
                    }
                    else
                    {
                        speedX -= speedX * 2;
                    }

                    if ((awesomePos.Y + speedY) < MaxY && (awesomePos.Y + speedY) > MinY)
                    {
                        awesomePos.Y += speedY;
                    }
                    else
                    {
                        speedY -= speedY * 2;
                    }
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
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightBlue);

            Rectangle dim = new Rectangle(0, 0, 800, 600);
            int maxWidth = graphics.GraphicsDevice.Viewport.Width;
            int maxHeight = graphics.GraphicsDevice.Viewport.Height;


            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);


            if (endGame)
            {
                Vector2 mid;

                KeyboardState ks = Keyboard.GetState();
                mid.X = (graphics.GraphicsDevice.Viewport.Width / 2);
                mid.Y = graphics.GraphicsDevice.Viewport.Height / 2;
                spriteBatch.Draw(dimmer, dim, new Color(new Vector4(1f, 1f, 1f, 0.5f)));


                spriteBatch.Draw(gameOver, mid, null, Color.White, 0f, new Vector2((float)gameOver.Width / 2, (float)gameOver.Height / 2), 1f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(font, "Score: " + score, new Vector2(mid.X + 20, mid.Y + 20), Color.White);
                spriteBatch.DrawString(font, "Game Over", new Vector2(mid.X - 50, mid.Y - 50), Color.White);


            }
            if (!endGame)
            {
                foreach (KeyValuePair<Pill, Rectangle> pair in pillerList)
                {
                    pair.Key.draw(spriteBatch, pill, pair.Value);
                }
            }
                spriteBatch.DrawString(font, "FPS: " + currentFrameRate, new Vector2(maxWidth - 60, 0), Color.Black);
                spriteBatch.DrawString(font, "Score: " + score, new Vector2(maxWidth - 80, 20), Color.Black);
                spriteBatch.DrawString(font, "Time: " + Math.Round(_timer), _timerVec, Color.White);
                spriteBatch.Draw(awesomeFace, awesomePos, Color.White);





            spriteBatch.End();
            base.Draw(gameTime);
            
        }
    }
}
