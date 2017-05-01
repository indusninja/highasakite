using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace High_as_a_Kite
{
    public class Background
    {
        //Background classes
        #region Variables
        private TileMap map;
        #endregion

        #region Properties
        public Vector2 Size
        {
            get { return new Vector2((float)map.GetWidthInPixels(), (float)map.GetHeightInPixels()); }
        }
        #endregion

        #region Methods
        public Background()
        {
            map = new TileMap();
        }

        public void LoadBackground(ContentManager content, string file)
        {
            /// <summary>
            /// Size of tiling for the background. Suggestion: Increase only if player is getting to the sides :)
            /// </summary>
            int repeats = 30;
            TileLayer layer = TileLayer.LoadHorizontalTiledBackgroundLayer(content, repeats, file);
            map.Layers.Add(layer);
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 pos)
        {
            map.Draw(spriteBatch, pos);
        }
        #endregion
    }
}
