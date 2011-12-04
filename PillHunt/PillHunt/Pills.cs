using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace PillHunt
    {
    class Pills
        {

        private List<Pill> list;

        //creates given amount of new pills to random positions, requires also screen size and pill texture's size
        public Pills(Map map, int numberOfPills, int screenWidth, int screenHeight, int pillSize)
            {

            list = new List<Pill>();
            Random random = new Random();
            int maxWidth = screenWidth - pillSize;
            int maxHeight = screenHeight - pillSize;
            Rectangle position;

            for (int i = 0; i < numberOfPills; i++)
                {
                position = new Rectangle(random.Next(maxWidth), random.Next(maxHeight), pillSize, pillSize);
                while (map.intersectsWithAWall(position, ""))
                    {
                    position = new Rectangle(random.Next(maxWidth), random.Next(maxHeight), pillSize, pillSize);
                    }
                list.Add(new Pill(position));
                }

            }


        //counts and returns how many pills in the list intersect with the given position
        //also removes all the pills that intersect with the given position and plays the nom sound if pills are eaten
        public int countIntersections(Rectangle position, SoundEffect nom)
            {

            int intersections = 0;

            for (int i = 0; i < list.Count; i++)
                {
                if (position.Intersects(list[i].getPosition()))
                    {
                    intersections++;
                    list.RemoveAt(i);
                    nom.Play();
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
