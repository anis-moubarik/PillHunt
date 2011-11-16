using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PillHunt
    {
    class Pills
        {

        private List<Pill> list;

        //creates given amount of new pills to random positions, requires also screen size and pill texture's size
        public Pills(int numberOfPills, int screenWidth, int screenHeight, int pillSize)
            {

            list = new List<Pill>();
            Random random = new Random();
            int maxWidth = screenWidth - pillSize;
            int maxHeight = screenHeight - pillSize;

            for (int i = 0; i < numberOfPills; i++)
                {
                list.Add(new Pill(new Rectangle(random.Next(maxWidth), random.Next(maxHeight), pillSize, pillSize)));
                }

            }


        //counts and returns how many pills in the list intersect with the given position
        //also removes all the pills that intersect with the given position
        public int countIntersections(Rectangle position)
            {

            int intersections = 0;

            for (int i = 0; i < list.Count; i++)
                {
                if (position.Intersects(list[i].getPosition()))
                    {
                    intersections++;
                    list.RemoveAt(i);
                    }
                }

            return intersections;

            }


        //draws all the pills using the given spritebatch and texture
        public void draw(SpriteBatch spriteBatch, Texture2D texture)
            {
            foreach (Pill pill in list)
                {
                pill.draw(spriteBatch, texture);
                }
            }


        public bool isEmpty()
            {
            return list.Count == 0;
            }
        }
    }
