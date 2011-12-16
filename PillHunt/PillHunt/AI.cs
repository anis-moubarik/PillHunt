using System;
using Microsoft.Xna.Framework;

namespace PillHunt
    {
    class AI
        {

        private int level;
        private Vector2 target;
        private bool targetAcquired;

        //creates a new AI of given level
        public AI(int lvl)
            {
            level = lvl;
            targetAcquired = false;
            }

        //sets a new target for AI if it doesn't already have one or it is reached
        public void moveAIPlayer(Player player)
            {
            if (!targetAcquired)
                {
                player.setDirection(getDirection());
                }
            }

        public Vector2 getDirection()
            {

            if (level == 1)
                {
                target = new Vector2(0, 0);
                targetAcquired = true;
                }

            return target;

            }

        }
    }
