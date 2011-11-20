

using Microsoft.Xna.Framework;

namespace PillHunt
{
    // First thing that shows up is this.
    class MainMenuScreen : MenuScreen
    {


        public MainMenuScreen()
            : base("PillHunt")
        {
            // Create our menu entries.
            MenuEntry playGameMenuEntry = new MenuEntry("Single player");
            MenuEntry versusMode = new MenuEntry("Versus");
            MenuEntry helpMenuEntry = new MenuEntry("Help");
            MenuEntry exitMenuEntry = new MenuEntry("Exit");

            // Hook up menu event handlers.
            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            versusMode.Selected += versusModeMenuEntrySelected;
            helpMenuEntry.Selected += HelpMenuEntry;
            exitMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(versusMode);
            MenuEntries.Add(helpMenuEntry);
            MenuEntries.Add(exitMenuEntry);

        }



        void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new PlayGameScreen(true), e.PlayerIndex);
        }

        void versusModeMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new PlayGameScreen(true), e.PlayerIndex);
        }


        // Event handler for when the help menu entry is selected.
        void HelpMenuEntry(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new HelpMenuScreen(), e.PlayerIndex);
        }


        // When the user cancels the main menu, ask if they want to exit.
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            const string message = "Are you sure you want to exit?";

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }


        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }

    }
}
