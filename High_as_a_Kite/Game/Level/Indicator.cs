using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace High_as_a_Kite
{
    class Indicator : SpriteAnimation
    {

        #region Constructors
        public Indicator(): base(0.3f, 0.5f, Vector2.Zero, 5, 1, 0, 5)
        {

        }
        #endregion

        #region Methods
        public void init(ContentManager content)
        {
            //loadContent(content, "wiimote sprite", 0, new Vector2(252, 16 * Game.SCREEN_HEIGHT / 20));
            loadContent(content, "wiimote_rotating_sprite", 0, new Vector2(252, 16 * Game.SCREEN_HEIGHT / 20));
            
        }

        public void update()
        {
            if (Input.mTimeleft == 0) CurrentFrame = 4;
            else if (Input.mTimeleft > (Input.mDelayTime / 4) * 3) CurrentFrame = 3;
            else if (Input.mTimeleft > (Input.mDelayTime / 4) * 2) CurrentFrame = 2;
            else if (Input.mTimeleft > (Input.mDelayTime / 4)) CurrentFrame = 1;
            else if (Input.mTimeleft > 0)
            {
                CurrentFrame = 0;
            }
        }

        #endregion
    }
}
