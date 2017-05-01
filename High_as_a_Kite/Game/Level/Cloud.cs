using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace High_as_a_Kite
{
    class Cloud : SpriteAnimation
    {
        #region Variables
        private int direction;
        private float size;
        #endregion

        #region Constructor
        public Cloud() : base()
        {
            this.size = 0;
            this.direction = 0;
        }

        // Summary:
        //     Creates a cloud with size 1 to 10, direction is between + and -
        public Cloud(float size, Vector2 position, int direction) : base(0.4f*size, 0.5f, Vector2.Zero, 15, 1, 0, 15)
        {
            if (size > 5) this.size = 5.0f;
            else if (size <= 0) this.size =0.2f;
            else this.size = size;
            if (direction < 0) this.direction = -1;
            else if (direction > 0) this.direction = +1;
            else this.direction = 0;
            Position = position;
        }
        #endregion

        #region Methods
        public void init(ContentManager content)
        {
            loadContent(content, "clouds3", 8 / (int)size, Position);
        }

        public new void UpdateFrame(float elapsed)
        {
            Move(elapsed);
            base.UpdateFrame(elapsed);
        }

        private void Move(float elapsed)
        {
            xPosition += (1.0f / ((float)size * 4)) * (float)direction * (elapsed * 400.0f);
        }
        #endregion
    }
}
