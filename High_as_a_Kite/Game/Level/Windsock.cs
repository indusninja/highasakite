using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace High_as_a_Kite
{
    class Windsock : SpriteAnimation
    {
        #region Variables
        private string windDirection;
        #endregion


        #region Properties
        public string WindDir
        {
            set{
                if (value == "LEFT" || value == "UP") windDirection = "LEFT";
                else windDirection = "RIGHT";
            }
        }

        public float WindStrength{
            set
            {
                if (value == 1.0f) RowIndex = 4;
                else if (value == 1.3f) RowIndex = 3;
                else if (value == 1.6f) RowIndex = 2;
                else if (value == 2.0f) RowIndex = 1;
                else RowIndex = 5;
            }            
        }
        #endregion

        #region Constructor
        public Windsock() : base(1.0f, 0.5f, Vector2.Zero, 2, 6, 0, 2)
        {
            this.windDirection = "NONE";
        }
        #endregion

        #region Method
        public void init(ContentManager content)
        {
            loadContent(content, "wind_sock2", 4, new Vector2(0, 2*Game.SCREEN_HEIGHT/3));
        }

        public void update(GameTime gameTime, string dir, float multiply)
        {
            WindDir = dir;
            if (dir == "UP") RowIndex = 0;
            else WindStrength = multiply;
            base.UpdateFrame(gameTime.ElapsedGameTime.Milliseconds);
        }

        public new void Draw(SpriteBatch batch)
        {
            SpriteEffects flip = SpriteEffects.None;
            if (windDirection == "RIGHT") flip = SpriteEffects.FlipHorizontally;
            Rectangle sourcerect = new Rectangle();
            sourcerect = new Rectangle(FrameWidth * CurrentFrame, FrameHeight * RowIndex, FrameWidth, FrameHeight);
            batch.Draw(Texture, Position, sourcerect, Color.White, Rotation, Origin, Scale, flip, Depth);
        }
        #endregion

    }
}
