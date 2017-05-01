using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace High_as_a_Kite
{
    class MenuScreen
    {
        private ScreenType screenFunctionality;
        private int displayTimeDuration;
        private int displayedForMilliSeconds = 0;
        private Texture2D backgroundSprite;
        private int state = 0;

        public ScreenType ScreenType
        {
            get { return screenFunctionality; }
            set { screenFunctionality = value; }
        }

        public int Delay
        {
            get { return displayTimeDuration; }
            set { displayTimeDuration = value; }
        }

        public int Progress
        {
            get { return state; }
        }

        public MenuScreen()
        {
            screenFunctionality = ScreenType.None;
            displayTimeDuration = 0;
        }

        public void LoadContent(ContentManager content, string assetname)
        {
            backgroundSprite = content.Load<Texture2D>(assetname);
        }

        public void Reset()
        {
            displayedForMilliSeconds = 0;
            state = 0;
        }

        public void Update(GameTime gameTime)
        {
            displayedForMilliSeconds += gameTime.ElapsedGameTime.Milliseconds;
            if ((Delay != -1) && (displayedForMilliSeconds >= Delay))
            {
                state = 1;
                return;
            }
            switch (Input.Button)
            {
                case "A":
                    state = 1;
                    break;
                case "HOME":
                    state = -1;
                    break;
                default:
                    state = 0;
                    break;
            }
            /*if (Input.Button == "A")
                state = 1;
            if (Input.Button == "HOME")
                state = -1;
            if (Input.NonZeroingStrength != 0)
                state = 1;*/
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 pos = new Vector2(Game.SCREEN_WIDTH / 2, Game.SCREEN_HEIGHT / 2);
            spriteBatch.Draw(backgroundSprite, 
                pos,
                null, 
                Color.White, 
                0.0f,
                new Vector2(backgroundSprite.Width / 2, backgroundSprite.Height / 2), 
                1.0f, 
                SpriteEffects.None, 
                1.0f);
        }
    }
}
