using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace High_as_a_Kite
{
    class HUD
    {
        private SpriteFont font;
        private Vector2 position;
        private string output;
        private Color fontColor;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public HUD()
        {
            position = Vector2.Zero;
            output = string.Empty;
            fontColor = Color.Red;
        }

        public void LoadFont(ContentManager content, string fontName)
        {
            font = content.Load<SpriteFont>(fontName);
        }

        public void Update(string text)
        {
            output = text;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 FontOrigin = font.MeasureString(output) / 2;
            spriteBatch.DrawString(font, output, position, fontColor, 0, FontOrigin, 1.0f, SpriteEffects.None, 1.0f);
        }
    }
}
