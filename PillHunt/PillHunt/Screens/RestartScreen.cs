using Microsoft.Xna.Framework;

namespace PillHunt
{
    class RestartScreen : MenuScreen
    {

        public RestartScreen()
            : base("Game Ended")
        {
            // Create our menu entries.
            MenuEntry resumeGameMenuEntry = new MenuEntry("New Game");
            MenuEntry quitGameMenuEntry = new MenuEntry("Quit Game");

            // Hook up menu event handlers.
            resumeGameMenuEntry.Selected += playGameSelected;
            quitGameMenuEntry.Selected += QuitGameMenuEntrySelected;

            // Add entries to the menu.
            MenuEntries.Add(resumeGameMenuEntry);
            MenuEntries.Add(quitGameMenuEntry);
        }

        void playGameSelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new GameplayScreen());
        }
        void QuitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Are you sure you want to quit?";

            MessageBoxScreen confirmQuitMessageBox = new MessageBoxScreen(message);

            confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
        }


        void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
                                                           new MainMenuScreen());
        }


    }
}
