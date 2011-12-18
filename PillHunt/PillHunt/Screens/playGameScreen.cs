using System;
using Microsoft.Xna.Framework;

namespace PillHunt
    {
    class PlayGameScreen : MenuScreen

        {

        MenuEntry p1HumanOrAIEntry;
        MenuEntry p2HumanOrAIEntry;
        MenuEntry soundEntry;

        private string[] humanOrAI = { " human", " easy", " medium", " hard", " very hard" };
        private int currentP1HumanOrAi = 0;
        private int currentP2HumanOrAi = 0;
        private bool soundsOn = true;

        public PlayGameScreen() : base("PillHunt")

            {

            // Create our menu entries.

            p1HumanOrAIEntry = new MenuEntry(string.Empty);
            p2HumanOrAIEntry = new MenuEntry(string.Empty);
            soundEntry = new MenuEntry(string.Empty);

            updateMenuEntries();

            MenuEntry playGame = new MenuEntry("Play Game");
            MenuEntry exitMenuEntry = new MenuEntry("Back to main menu");

            // Hook up menu event handlers.
            p1HumanOrAIEntry.Selected += p1HumanOrAISelected;
            p2HumanOrAIEntry.Selected += p2HumanOrAISelected;
            soundEntry.Selected += soundOnOrOffSelected;

            playGame.Selected += playGameSelected;
            exitMenuEntry.Selected += confirmQuitMessageBoxAccepted;

            // Add entries to the menu.
            MenuEntries.Add(p1HumanOrAIEntry);
            MenuEntries.Add(p2HumanOrAIEntry);
            MenuEntries.Add(soundEntry);

            MenuEntries.Add(playGame);
            MenuEntries.Add(exitMenuEntry);

            }

        //update the menu values:
        void updateMenuEntries()
            {
            p1HumanOrAIEntry.Text = "Player 1: " + humanOrAI[currentP1HumanOrAi];
            p2HumanOrAIEntry.Text = "Player 2: " + humanOrAI[currentP2HumanOrAi];
            soundEntry.Text = "Sounds: " + (soundsOn ? " on" : " off");
            }


        //event handlers:

        void p1HumanOrAISelected(object sender, PlayerIndexEventArgs e)
            {
            currentP1HumanOrAi = (currentP1HumanOrAi + 1) % humanOrAI.Length;
            updateMenuEntries();
            }

        void p2HumanOrAISelected(object sender, PlayerIndexEventArgs e)
            {
            currentP2HumanOrAi = (currentP2HumanOrAi + 1) % humanOrAI.Length;
            updateMenuEntries();
            }

        void soundOnOrOffSelected(object sender, PlayerIndexEventArgs e)
            {
            soundsOn = !soundsOn;
            updateMenuEntries();
            }

        void playGameSelected(object sender, PlayerIndexEventArgs e)
            {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new GameplayScreen());
            }

        void confirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
            {
            ScreenManager.AddScreen(new MainMenuScreen(), e.PlayerIndex);
            }

        }

    }

