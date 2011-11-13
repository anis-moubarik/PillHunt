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

        Player player1;
        Timer clock;
        FPS fps;

        Dictionary<Pill, Rectangle> pillerList;
        List<Pill> toBeRemoved;

        bool endGame = false;
        int speedX;
        int speedY;

        public Game1()
            {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;

            player1 = new Player();
            clock = new Timer();

            fps = new FPS(800);

            }

        protected override void Initialize()
            {
            base.Initialize();
            }

        protected override void LoadContent()
            {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            awesomeFace = Content.Load<Texture2D>("awesome");
            pill = Content.Load<Texture2D>("pill");
            font = Content.Load<SpriteFont>("FPS");
            dimmer = Content.Load<Texture2D>("dimmer");
            gameOver = Content.Load<Texture2D>("gameover");
            createPills();

            }


        public void createPills()
            {

            Random random = new Random();
            int maxX = graphics.GraphicsDevice.Viewport.Width - pill.Width;
            int maxY = graphics.GraphicsDevice.Viewport.Height - pill.Height;

            pillerList = new Dictionary<Pill, Rectangle>();

            for (int i = 0; i < 100; i++)
                {
                pillerList.Add(new Pill(), new Rectangle(random.Next(maxX), random.Next(maxY), 32, 32));
                }
            }

        protected override void UnloadContent()
            {
            }

        protected override void Update(GameTime gameTime)
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
                this.Exit();
                }

            if (!endGame)
            {
                keyState = movement(keyState);


                fps.increaseCounter();
                fps.increaseTime(gameTime.ElapsedGameTime.Milliseconds);

                if (fps.getFrameTime() >= 1000)
                    {
                    fps.setFPS();
                    }


                foreach (KeyValuePair<Pill, Rectangle> pair in pillerList)
                    {
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


                base.Update(gameTime);
                }
            }

        private KeyboardState movement(KeyboardState keyState)
        {

            //liike nopeutuu jos nappia painettu
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
            } //muuten hidastuu
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
            }

            int maxX = graphics.GraphicsDevice.Viewport.Width - awesomeFace.Width;
            int maxY = graphics.GraphicsDevice.Viewport.Height - awesomeFace.Height;
            int minY = 0;
            int minX = 0;

            //estää pelaajan menon ulos kentältä
            if ((player1.getX() + speedX) < maxX && ((player1.getX() + speedX) > minX))
            {
                player1.setX(player1.getX() + speedX);
            } //bounce
            else 
            {
                speedX -= speedX * 2;
            }

            if ((player1.getY() + speedY) < maxY && (player1.getY() + speedY) > minY)
            {
                player1.setY(player1.getY() + speedY);
            }
            else
            {
                speedY -= speedY * 2;
            }
            return keyState;
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
                mid.X = graphics.GraphicsDevice.Viewport.Width / 2;
                mid.Y = graphics.GraphicsDevice.Viewport.Height / 2;
                spriteBatch.Draw(dimmer, dim, new Color(new Vector4(1f, 1f, 1f, 0.5f)));


                spriteBatch.Draw(gameOver, mid, null, Color.White, 0f, new Vector2((float)gameOver.Width / 2, (float)gameOver.Height / 2), 1f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(font, "Score: " + player1.getScore(), new Vector2(mid.X + 20, mid.Y + 20), Color.White);
                spriteBatch.DrawString(font, "Game Over", new Vector2(mid.X - 50, mid.Y - 50), Color.White);


                }
            if (!endGame)
                {
                foreach (KeyValuePair<Pill, Rectangle> pair in pillerList)
                    {
                    pair.Key.draw(spriteBatch, pill, pair.Value);
                    }
                }
            
            spriteBatch.DrawString(font, "Score: " + player1.getScore(), new Vector2(maxWidth - 80, 20), Color.Black);

            fps.draw(spriteBatch, font);
            clock.draw(spriteBatch, font);
            player1.draw(spriteBatch, awesomeFace);

            spriteBatch.End();
            base.Draw(gameTime);

            }
        }
    }
