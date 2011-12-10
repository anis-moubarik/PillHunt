using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;

namespace PillHunt
    {
    class Map
        {

        private List<Wall> list;

        public Map(string map, int screenWidth, int screenHeight)
            {
            list = new List<Wall>();
            loadMap(map, screenWidth, screenHeight);
            }

        //draws all the walls using the given spritebatch and texture
        public void draw(SpriteBatch spriteBatch, Texture2D texture, bool gameEnds, bool active, Player player1)
            {
            foreach (Wall wall in list)
                {
                wall.draw(spriteBatch, texture, gameEnds, active, player1);
                }
            }

        //loads a map from a .txt-file
        public void loadMap(string map, int width, int height)
            {

            TextReader tr = new StreamReader(map);
            int lines = int.Parse(tr.ReadLine());
            string[] text;
            Rectangle position;

            for (int i = 0; i < lines; i++)
                {
                text = tr.ReadLine().Split(',');
                position = new Rectangle(int.Parse(text[3]), int.Parse(text[4]), int.Parse(text[5]), int.Parse(text[6]));
                list.Add(new Wall(position, width, height, bool.Parse(text[0]), bool.Parse(text[1]), int.Parse(text[2])));
                }

            tr.Close();

            }

        //returns true if given position intersects with a given edge of a wall
        public bool intersectsWithAWall(Rectangle position, String edge)
            {
            for (int i = 0; i < list.Count; i++)
                {
                if (position.Intersects(list[i].getPosition(edge)))
                    {                    
                    return true;
                    }
                }
            return false;
            }

        }
    }
