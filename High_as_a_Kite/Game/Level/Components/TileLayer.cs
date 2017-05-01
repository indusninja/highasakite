using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace High_as_a_Kite
{
    public class TileLayer
    {
        static int tileWidth = 400;
        static int tileHeight = 500;

        public static int TileWidth
        {
            get { return tileWidth; }
            set { tileWidth = value; }
        }

        public static int TileHeight
        {
            get { return tileHeight; }
            set { tileHeight = value; }
        }

        List<Texture2D> tileTextures = new List<Texture2D>();
        int[,] map;

        public int WidthInPixels
        {
            get { return Width * tileWidth; }
        }

        public int HeightInPixels
        {
            get { return Height * tileHeight; }
        }

        public int Width
        {
            get { return map.GetLength(1); }
        }

        public int Height
        {
            get { return map.GetLength(0); }
        }

        public TileLayer(int width, int height)
        {
            map = new int[height, width];

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    map[y, x] = -1;
        }

        public static TileLayer LoadHorizontalTiledBackgroundLayer(ContentManager content, int repeatCount, string fileName)
        {
            TileLayer tileLayer;
            bool readingTextures = false;
            bool readingLayout = false;
            List<string> textureNames = new List<string>();
            List<List<int>> tempLayout = new List<List<int>>();

            using (StreamReader reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine().Trim();

                    if (string.IsNullOrEmpty(line))
                        continue;

                    if (line.Contains("[Textures]"))
                    {
                        readingTextures = true;
                        readingLayout = false;
                    }
                    else if (line.Contains("[Layout]"))
                    {
                        readingTextures = false;
                        readingLayout = true;
                    }
                    else if (readingTextures)
                    {
                        textureNames.Add(line);
                    }
                    else if (readingLayout)
                    {
                        List<int> row = new List<int>();

                        string[] cells = line.Split(' ');

                        foreach (string c in cells)
                        {
                            if (!string.IsNullOrEmpty(c))
                                row.Add(int.Parse(c));
                        }

                        tempLayout.Add(row);
                    }
                }
            }

            int width = tempLayout[0].Count * repeatCount;
            int height = tempLayout.Count;

            tileLayer = new TileLayer(width, height);

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    tileLayer.SetCellIndex(x, y, tempLayout[y][x % tempLayout[0].Count]);

            tileLayer.LoadTileTextures(content, textureNames.ToArray());

            return tileLayer;
        }

        public void LoadTileTextures(ContentManager content, params string[] textureNames)
        {
            Texture2D texture;

            foreach (string textureName in textureNames)
            {
                texture = content.Load<Texture2D>(textureName);
                tileWidth = Math.Max(tileWidth, texture.Width);
                tileHeight = Math.Max(tileHeight, texture.Height);
                tileTextures.Add(texture);
            }
        }

        public void AddTexture(Texture2D texture)
        {
            tileTextures.Add(texture);
        }

        public void SetCellIndex(int x, int y, int cellIndex)
        {
            map[y, x] = cellIndex;
        }

        public int GetCellIndex(int x, int y)
        {
            return map[y, x];
        }

        public void Draw(SpriteBatch batch, Vector2 pos)
        {
            int tileMapWidth = map.GetLength(1);
            int tileMapHeight = map.GetLength(0);

            for (int x = 0; x < tileMapWidth; x++)
            {
                for (int y = 0; y < tileMapHeight; y++)
                {
                    int textureIndex = map[y, x];

                    if (textureIndex == -1)
                        continue;

                    Texture2D texture = tileTextures[textureIndex];

                    batch.Draw(
                        texture,
                        new Rectangle(
                            x * tileWidth - (int)pos.X,
                            y * tileHeight - (int)pos.Y,
                            tileWidth,
                            tileHeight),
                        null,
                        Color.White,
                        0.0f, 
                        Vector2.Zero, 
                        SpriteEffects.None, 
                        1.0f);
                }
            }
        }
    }
}
