using Microsoft.Xna.Framework;

namespace PillHunt
    {
    class RestartScreen : MenuScreen
        {

        string p1name;
        string p2name;
        string mapName;
        bool p1ai;
        bool p2ai;
        int p1aiLevel;
        int p2aiLevel;

        public RestartScreen(string player1Name, string player2Name, string nameOfTheMap,
            bool player1IsAI, bool player2IsAI, int player1AILevel, int player2AILevel)
            : base("Game Ended")

            {

            p1name = player1Name;
            p2name = player2Name;
            mapName = nameOfTheMap;
            p1ai = player1IsAI;
            p2ai = player2IsAI;
            p1aiLevel = player1AILevel;
            p2aiLevel = player2AILevel;

            // Create our menu entries.
            MenuEntry restartGameMenuEntry = new MenuEntry("New Game with same settings");
            MenuEntry restartDifferentGameMenuEntry = new MenuEntry("New Game with different settings");
            MenuEntry quitGameMenuEntry = new MenuEntry("\nExit to Main Menu");

            // Hook up menu event handlers.
            restartGameMenuEntry.Selected += restartGameMenuSelected;
            restartDifferentGameMenuEntry.Selected += restartDifferentGameMenuSelected;
            quitGameMenuEntry.Selected += quitGameMenuEntrySelected;

            // Add entries to the menu.
            MenuEntries.Add(restartGameMenuEntry);
            MenuEntries.Add(restartDifferentGameMenuEntry);
            MenuEntries.Add(quitGameMenuEntry);

            }

        //event handlers:

        void restartGameMenuSelected(object sender, PlayerIndexEventArgs e)
            {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new GameplayScreen(p1name, p2name,
                mapName, p1ai, p2ai, p1aiLevel, p2aiLevel));
            }

        void restartDifferentGameMenuSelected(object sender, PlayerIndexEventArgs e)
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
