using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace PillHunt
{
    class PlayGameScreen : MenuScreen
    {
        public PlayGameScreen(bool versusMode)
            : base("PillHunt")
        {
            if (versusMode)
            {
                MenuEntry playerNames = new MenuEntry("Player names");
                MenuEntry playGame = new MenuEntry("Play Game");
                // Hook up menu event handlers.
                playerNames.Selected += playerNamesMenuEntrySelected;
                playGame.Selected += playGameSelected;


                // Add entries to the menu.
                MenuEntries.Add(playerNames);
                MenuEntries.Add(playGame);
            }
            else
            {
                MenuEntry playerNames = new MenuEntry("Player names");

                // Hook up menu event handlers.
                playerNames.Selected += playerNamesMenuEntrySelected;

                // Add entries to the menu.
                MenuEntries.Add(playerNames);
            }
            MenuEntry exitMenuEntry = new MenuEntry("Back to main menu");
            exitMenuEntry.Selected += ConfirmQuitMessageBoxAccepted;
            MenuEntries.Add(exitMenuEntry);
        }

        void playGameSelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new GameplayScreen());
        }

        void playerNamesMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            //MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen();

            //confirmExitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;
        }
        void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            //LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
            //                                               new MainMenuScreen());
            //ScreenManager.AddScreen(new playGameScreen(true), e.PlayerIndex);
            ScreenManager.AddScreen(new MainMenuScreen(), e.PlayerIndex);
        }
    }
}

