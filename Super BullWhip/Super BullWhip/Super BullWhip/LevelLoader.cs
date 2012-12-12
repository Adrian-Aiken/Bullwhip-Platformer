using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Starbound.RealmFactoryCore;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Super_BullWhip
{
    public static class LevelLoader
    {
        static LevelSet level;
        static Level currentLevel;
        public static void Load(string name)
        {
            level = Game1.Instance.Content.Load<LevelSet>("prototype_level");
            currentLevel = level.GetLevel(name).CreateInstance();
            float mult = currentLevel.Rows * currentLevel.CellSize.Y;
            for (int i = 0; i < currentLevel.Rows; i++)
            {
                for (int o = 0; o < currentLevel.Columns; o++)
                {
                    Tile tile = (Tile)currentLevel.GetTile(i, o);
                    if (tile != null)
                    {
                        Console.WriteLine("Tile: " + o + ", " + i);
                        Console.WriteLine("CellWidth: " + currentLevel.CellSize.X);

                        Obj obj = new Obj(Game1.Instance, o * currentLevel.CellSize.X, (i * currentLevel.CellSize.Y) - mult, 0, tile.Texture);
                        obj.Scale.X = (float)currentLevel.CellSize.X / (float)tile.Texture.Width;
                        obj.Scale.Y = (float)currentLevel.CellSize.Y / (float)tile.Texture.Height;
                    }

                }
            }
        }
    }
}
