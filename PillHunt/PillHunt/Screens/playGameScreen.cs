using System;
using Microsoft.Xna.Framework;

namespace PillHunt
    {
    class PlayGameScreen : MenuScreen

        {

        MenuEntry p1HumanOrAIEntry;
        MenuEntry p2HumanOrAIEntry;
        MenuEntry mapEntry;

        private string[] humanOrAI = { "human player", "novice computer", "dealer computer", "addict computer", "drug lord computer" };
        private int currentP1HumanOrAi = 0;
        private int currentP2HumanOrAi = 0;

        private string[] maps = { "map", "map2", "map3" };
        private int currentMap = 0;

        //creates a new play game screen
        public PlayGameScreen() : base("PillHunt")

            {

            //creating menu entries

            p1HumanOrAIEntry = new MenuEntry(string.Empty);
            p2HumanOrAIEntry = new MenuEntry(string.Empty);
            mapEntry = new MenuEntry(string.Empty);

            updateMenuEntries();

            MenuEntry playGame = new MenuEntry("\n\n\nStart Hunting!");
            MenuEntry exitMenuEntry = new MenuEntry("\n\n\n\n\nBack to Main Menu");

            // Hook up menu event handlers.
            p1HumanOrAIEntry.Selected += p1HumanOrAISelected;
            p2HumanOrAIEntry.Selected += p2HumanOrAISelected;
            mapEntry.Selected += mapSelected;
            
            playGame.Selected += playGameSelected;
            exitMenuEntry.Selected += confirmQuitMessageBoxAccepted;

            // Add entries to the menu.
            MenuEntries.Add(p1HumanOrAIEntry);
            MenuEntries.Add(p2HumanOrAIEntry);
            MenuEntries.Add(mapEntry);

            MenuEntries.Add(playGame);
            MenuEntries.Add(exitMenuEntry);

            }

        //update the menu values:
        void updateMenuEntries()
            {
            p1HumanOrAIEntry.Text = "\nPlayer 1: " + humanOrAI[currentP1HumanOrAi];
            p2HumanOrAIEntry.Text = "\nPlayer 2: " + humanOrAI[currentP2HumanOrAi];
            mapEntry.Text = "\n\nMap: " + maps[currentMap];
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

        void mapSelected(object sender, PlayerIndexEventArgs e)
            {
            currentMap = (currentMap + 1) % maps.Length;
            updateMenuEntries();
            }

        void playGameSelected(object sender, PlayerIndexEventArgs e)
            {

            bool player1IsAI;
            bool player2IsAI;
            string player1Name;
            string player2Name;

            if (currentP1HumanOrAi == 0)
                {
                player1IsAI = false;
                player1Name = "Player 1";
                }
            else
                {
                player1IsAI = true;
                player1Name = "Computer 1";
                }

            if (currentP2HumanOrAi == 0)
                {
                player2IsAI = false;
                player2Name = "Player 2";
                }
            else
                {
                player2IsAI = true;
                player2Name = "Computer 2";
                }

            string map = maps[currentMap] + ".txt";

            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new GameplayScreen(player1Name, player2Name,
                map, player1IsAI, player2IsAI, currentP1HumanOrAi, currentP2HumanOrAi));

            }

        void confirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
            {
            ScreenManager.AddScreen(new MainMenuScreen(), e.PlayerIndex);
            }

        }

    }

