using Microsoft.Xna.Framework;

namespace PillHunt
    {
    class PauseMenuScreen : MenuScreen
        {

        string p1name;
        string p2name;
        string mapName;
        bool p1ai;
        bool p2ai;
        int p1aiLevel;
        int p2aiLevel;

        public PauseMenuScreen(string player1Name, string player2Name, string nameOfTheMap,
            bool player1IsAI, bool player2IsAI, int player1AILevel, int player2AILevel)
            : base("Paused")

            {

            p1name = player1Name;
            p2name = player2Name;
            mapName = nameOfTheMap;
            p1ai = player1IsAI;
            p2ai = player2IsAI;
            p1aiLevel = player1AILevel;
            p2aiLevel = player2AILevel;

            // Create our menu entries.
            MenuEntry resumeGameMenuEntry = new MenuEntry("Resume Game");
            MenuEntry restartGameMenuEntry = new MenuEntry("\nRestart Game with same settings");
            MenuEntry restartDifferentGameMenuEntry = new MenuEntry("\nRestart Game with different settings");
            MenuEntry quitGameMenuEntry = new MenuEntry("\n\nExit to Main Menu");

            // Hook up menu event handlers.
            resumeGameMenuEntry.Selected += OnCancel;
            restartGameMenuEntry.Selected += restartGameMenuSelected;
            restartDifferentGameMenuEntry.Selected += restartDifferentGameMenuSelected;
            quitGameMenuEntry.Selected += quitGameMenuEntrySelected;

            // Add entries to the menu.
            MenuEntries.Add(resumeGameMenuEntry);
            MenuEntries.Add(restartGameMenuEntry);
            MenuEntries.Add(restartDifferentGameMenuEntry);
            MenuEntries.Add(quitGameMenuEntry);

            }

        //event handlers:

        void quitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
            {
            const string message = "Are you sure you want to quit?";
            MessageBoxScreen confirmQuitMessageBox = new MessageBoxScreen(message);
            confirmQuitMessageBox.Accepted += confirmQuitMessageBoxAccepted;
            ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
            }

        void restartGameMenuSelected(object sender, PlayerIndexEventArgs e)
            {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new GameplayScreen(p1name, p2name,
                mapName, p1ai, p2ai, p1aiLevel, p2aiLevel));
            }

        void restartDifferentGameMenuSelected(object sender, PlayerIndexEventArgs e)
            {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new GameplayScreen(p1name, p2name,
                mapName, p1ai, p2ai, p1aiLevel, p2aiLevel));
            }

        void confirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
            {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
            }


        }
    }
