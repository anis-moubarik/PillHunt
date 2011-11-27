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
            loadMap(map);

        }

        //draws all the walls using the given spritebatch and texture
        public void draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            foreach (Wall wall in list)
            {
                wall.draw(spriteBatch, texture);
            }
        }


        public void loadMap(string map)
        {

            TextReader tr = new StreamReader(map);
            int lines = int.Parse(tr.ReadLine());
            string[] text;

            for (int i = 0; i < lines; i++)
            {
                text = tr.ReadLine().Split(',');
                list.Add(new Wall(new Rectangle(int.Parse(text[0]), int.Parse(text[1]), int.Parse(text[2]), int.Parse(text[3]))));
            }

            tr.Close();

        }

        public bool intersectsWithAWall(Rectangle position)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (position.Intersects(list[i].getPosition()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
