using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;
using System.Reflection;

namespace PillHunt
    {
    class Map
        {

        private List<Wall> list;

        public Map(string map)
            {
            list = new List<Wall>();
            loadMap(map);
            }

        //draws all the walls using the given spritebatch and texture
        public void draw(SpriteBatch spriteBatch, Texture2D texture, bool gameEnds, bool active, Player player1, Player player2)
            {
            foreach (Wall wall in list)
                {
                wall.draw(spriteBatch, texture, gameEnds, active, player1, player2);
                }
            }

        //loads a map from a .txt-file
        public void loadMap(string map)
            {

            TextReader tr = new StreamReader(map);
            int numberOfLines = int.Parse(tr.ReadLine());
            string[] line;

            Color color;
            PropertyInfo colorProperty;

            bool isMoving, isVertical, isInflating, partTimeVisible;
            int speed, x, y, width, height, movingLimit, inflateLimit, blinkRate;

            Rectangle position;
            Rectangle limitPosition;

            for (int i = 0; i < numberOfLines; i++)
                {

                line = tr.ReadLine().Split(',');

                colorProperty = typeof(Color).GetProperty(line[0]);
                color = (Color)colorProperty.GetValue(null, null);

                isMoving = bool.Parse(line[1]);
                isVertical = bool.Parse(line[2]);
                isInflating = bool.Parse(line[3]);
                partTimeVisible = bool.Parse(line[4]);

                speed = int.Parse(line[5]);
                x = int.Parse(line[6]);
                y = int.Parse(line[7]);
                width = int.Parse(line[8]);
                height = int.Parse(line[9]);
                movingLimit = int.Parse(line[10]);
                inflateLimit = int.Parse(line[11]);
                blinkRate = int.Parse(line[12]);

                position = new Rectangle(x, y, width, height);

                if (isVertical)
                    {
                    limitPosition = new Rectangle(x + movingLimit, y, width, height);
                    }
                else
                    {
                    limitPosition = new Rectangle(x, y + movingLimit, width, height);
                    }

                list.Add(new Wall(color, position, limitPosition, isMoving,
                    isVertical, isInflating, partTimeVisible, blinkRate, inflateLimit, speed));

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
