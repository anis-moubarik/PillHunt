using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PillHunt
{

    public class InputAction
    {
        private readonly Keys[] keys;
        private readonly bool newPressOnly;

        private delegate bool KeyPress(Keys key, PlayerIndex? controllingPlayer, out PlayerIndex player);

        public InputAction(Keys[] keys, bool newPressOnly)
        {
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
