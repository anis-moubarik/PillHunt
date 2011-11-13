using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PillHunt
{

    class HelpMenuScreen : MenuScreen
    {



        static string message;

        public HelpMenuScreen()
            : base("Options")
        {
            // Create our menu entries.
            //languageMenuEntry = new MenuEntry(string.Empty);

            //SetMenuEntryText();

            MenuEntry back = new MenuEntry("Back");


            // Hook up menu event handlers.
            //languageMenuEntry.Selected += LanguageMenuEntrySelected;
            back.Selected += OnCancel;
            
            // Add entries to the menu.
            //MenuEntries.Add(languageMenuEntry);
            message = "PillHunt is a realistic simulation of the hunt\nsome members of the society go through daily.\n\n"
            + "Your objective is to eat as many pills\n as you can under the time limit.\n\nPlayer 1 moves using the WASD keys.\n"
            + "Player 2 moves using the arrow keys.\n\n"
            + "Press ESC to pause the game.";
            MenuEntries.Add(back);
        }


        void SetMenuEntryText()
        {
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            // Darken down any other screens that were drawn beneath the popup.
            ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2 / 3);

            // Center the message text in the viewport.
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Vector2 viewportSize = new Vector2(viewport.Width, viewport.Height);
            Vector2 textSize = font.MeasureString(message);
            Vector2 textPosition = (viewportSize - textSize) / 2;

            // The background includes a border somewhat larger than the text itself.
            const int hPad = 32;
            const int vPad = 16;

            Rectangle backgroundRectangle = new Rectangle((int)textPosition.X - hPad,
                                                          (int)textPosition.Y - vPad,
                                                          (int)textSize.X + hPad * 2,
                                                          (int)textSize.Y + vPad * 2);

            Color color = Color.White * TransitionAlpha;

            spriteBatch.Begin();

            spriteBatch.DrawString(font, message, textPosition, color);

            spriteBatch.End();
        }

    }
}