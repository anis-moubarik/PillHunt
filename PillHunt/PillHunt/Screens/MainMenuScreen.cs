using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace PillHunt
    {
    // First thing that shows up is this.
    class MainMenuScreen : MenuScreen
        {

        ContentManager content;
        Song bgMusic;
        MenuEntry soundEntry;
        private bool soundsOn = false;


        public MainMenuScreen() : base("PillHunt")

            {

            // Create our menu entries.

            soundEntry = new MenuEntry(string.Empty);
            updateMenuEntries();

            MenuEntry playGameMenuEntry = new MenuEntry("\nHunt Pills!");
            MenuEntry helpMenuEntry = new MenuEntry("\n\n\nHelp");
            MenuEntry creditMenuEntry = new MenuEntry("\n\n\n\nCredits");
            MenuEntry exitMenuEntry = new MenuEntry("\n\n\n\n\nExit");

            // Hook up menu event handlers.
            playGameMenuEntry.Selected += playGameMenuEntrySelected;
            soundEntry.Selected += soundOnOrOffSelected;
            helpMenuEntry.Selected += helpMenuEntrySelected;
            creditMenuEntry.Selected += creditMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(soundEntry);
            MenuEntries.Add(helpMenuEntry);
            MenuEntries.Add(creditMenuEntry);
            MenuEntries.Add(exitMenuEntry);

            }

        //updates all the menu entries
        void updateMenuEntries()
            {

            soundEntry.Text = "\n\nSounds: " + (soundsOn ? "on" : "off");

            if (soundsOn)
                {
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(bgMusic);
                MediaPlayer.Volume = 0.085f;
                SoundEffect.MasterVolume = 0.09f;
                }

            else
                {
                MediaPlayer.Volume = 0.000f;
                SoundEffect.MasterVolume = 0.00f;
                }

            }

        //event handlers:

        void playGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
            {
            ScreenManager.AddScreen(new PlayGameScreen(), e.PlayerIndex);
            }

        void soundOnOrOffSelected(object sender, PlayerIndexEventArgs e)
            {
            soundsOn = !soundsOn;
            updateMenuEntries();
            }

        void helpMenuEntrySelected(object sender, PlayerIndexEventArgs e)
            {
            ScreenManager.AddScreen(new HelpMenuScreen(), e.PlayerIndex);
            }

        void creditMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new CreditMenuScreen(), e.PlayerIndex);
        }


        // When the user cancels the main menu, ask if they want to exit.
        protected override void OnCancel(PlayerIndex playerIndex)
            {
            const string message = "Are you sure you want to exit?";

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += confirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
            }


        void confirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
            {
            ScreenManager.Game.Exit();
            }


        //activates the main menu
        public override void Activate(bool instancePreserved)
            {
            if (!instancePreserved)
                {
                if (content == null)
                    {
                    content = new ContentManager(ScreenManager.Game.Services, "Content");
                    }
                bgMusic = content.Load<Song>("daymare");
                }
            }


        public override void Deactivate()
            {
            base.Deactivate();
            }


        public override void Unload()
            {
            content.Unload();
            }

        }

    }
