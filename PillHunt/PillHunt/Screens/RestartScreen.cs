using Microsoft.Xna.Framework;

namespace PillHunt
{
    class RestartScreen : MenuScreen
    {

        public RestartScreen() : base("Game Ended")

        {

            // Create our menu entries.
            MenuEntry restartGameMenuEntry = new MenuEntry("New Game");
            MenuEntry quitGameMenuEntry = new MenuEntry("Exit to Main Menu");

            // Hook up menu event handlers.
            restartGameMenuEntry.Selected += playGameSelected;
            quitGameMenuEntry.Selected += quitGameMenuEntrySelected;

            // Add entries to the menu.
            MenuEntries.Add(restartGameMenuEntry);
            MenuEntries.Add(quitGameMenuEntry);

        }

        void playGameSelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new PlayGameScreen());
        }
        void quitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Are you sure you want to quit?";
            MessageBoxScreen confirmQuitMessageBox = new MessageBoxScreen(message);
            confirmQuitMessageBox.Accepted += confirmQuitMessageBoxAccepted;
            ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
        }


        void confirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
        }

    }

}
