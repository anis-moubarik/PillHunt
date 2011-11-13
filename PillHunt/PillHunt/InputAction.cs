using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PillHunt
{

    public class InputAction
    {
        private readonly Buttons[] buttons;
        private readonly Keys[] keys;
        private readonly bool newPressOnly;

        private delegate bool ButtonPress(Buttons button, PlayerIndex? controllingPlayer, out PlayerIndex player);
        private delegate bool KeyPress(Keys key, PlayerIndex? controllingPlayer, out PlayerIndex player);

        public InputAction(Buttons[] buttons, Keys[] keys, bool newPressOnly)
        {
            this.buttons = buttons != null ? buttons.Clone() as Buttons[] : new Buttons[0];
            this.keys = keys != null ? keys.Clone() as Keys[] : new Keys[0];

            this.newPressOnly = newPressOnly;
        }


        public bool Evaluate(InputState state, PlayerIndex? controllingPlayer, out PlayerIndex player)
        {
            KeyPress keyTest;
            if (newPressOnly)
            {
                keyTest = state.IsNewKeyPress;
            }
            else
            {
                keyTest = state.IsKeyPressed;
            }

            foreach (Keys key in keys)
            {
                if (keyTest(key, controllingPlayer, out player))
                    return true;
            }

            player = PlayerIndex.One;
            return false;
        }
    }
}
