﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace High_as_a_Kite
{
    public class TileMap
    {
        public List<TileLayer> Layers = new List<TileLayer>();

        public int GetWidthInPixels()
        {
            return GetWidth() * TileLayer.TileWidth;
        }

        public int GetHeightInPixels()
        {
            return GetHeight() * TileLayer.TileHeight;
        }

        public int GetWidth()
        {
            int width = 0;

            foreach (TileLayer layer in Layers)
                width = (int)Math.Max(width, layer.Width);

            return width;
        }

        public int GetHeight()
        {
            int height = 0;

            foreach (TileLayer layer in Layers)
                height = (int)Math.Max(height, layer.Height);

            return height;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 pos)
        {
            foreach (TileLayer layer in Layers)
                layer.Draw(spriteBatch, pos);
        }
    }
}
