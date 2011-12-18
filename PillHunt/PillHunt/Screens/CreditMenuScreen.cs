using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PillHunt
{

    class CreditMenuScreen : MenuScreen
    {

        static string message;

        public CreditMenuScreen()
            : base("Options")
        {
            // Create our menu entries.
            MenuEntry back = new MenuEntry("Back");

            // Hook up menu event handlers.
            back.Selected += OnCancel;
            
            // Add entries to the menu.
            message = "Music: Mr.Spastic - Daymare\n\n"
            + "Movement, walls and maps: Simo Sahlstedt , Miro Varilo, Anis Moubarik\n\n"
            + "AI: Simo Sahlstedt \n\n"
            + "Menus: Anis Moubarik\n\n"
            + "Help menu: Miro Varilo\n\n"
            + "Documentation: Anis Moubarik\n\n"
            + "Textures and sound effects: Anon, Anis Moubarik, Simo Sahlstedt\n\n"
            + "Game concept: Anis Moubarik";
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