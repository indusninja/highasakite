using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace High_as_a_Kite
{
    public class ColorMap
    {
        #region Variables
        private Color[,] spriteColorData;
        private int arrayWidth, arrayHeight;
        #endregion

        #region Method
        public ColorMap()
        {
            arrayWidth = 0;
            arrayHeight = 0;
            spriteColorData = new Color[arrayWidth, arrayHeight];
        }

        public ColorMap(Sprite sprite)
        {
            arrayWidth = sprite.Texture.Width;
            arrayHeight = sprite.Texture.Height;
            Color[] colors1D = new Color[arrayWidth * arrayHeight];
            sprite.Texture.GetData(colors1D);

            spriteColorData = new Color[arrayWidth, arrayHeight];
            for (int x = 0; x < arrayWidth; x++)
                for (int y = 0; y < arrayHeight; y++)
                    spriteColorData[x, y] = colors1D[x + y * arrayWidth];
        }

        private Color GetColor(int x, int y)
        {
            x %= arrayWidth;
            y %= arrayHeight;
            if (x < 0)
                x += arrayWidth;
            if (y < 0)
                y += arrayHeight;
            return spriteColorData[x, y];
        }

        public void Update(int x, int y, out string direction, out float multiplier)
        {
            direction = "UP";
            Color clr = GetColor(x, y);
            multiplier = 0.0f;
            /// <summary>
            /// for Red=255, Green=0, Blue=0; Direction is (x, y) = (1, 0) (RIGHT); Magnitude = 2;
            /// </summary>
            if ((clr.R.Equals(255)) && (clr.G.Equals(0)) && (clr.B.Equals(0)))
            {
                direction = "RIGHT";
                multiplier = 2.0f;
            }
            /// <summary>
            /// for Red=255, Green=130, Blue=0; Direction is (x, y) = (1, 0) (RIGHT); Magnitude = 1.6;
            /// </summary>
            else if ((clr.R.Equals(255)) && (clr.G.Equals(130)) && (clr.B.Equals(0)))
            {
                direction = "RIGHT";
                multiplier = 1.6f;
            }
            /// <summary>
            /// for Red=255, Green=190, Blue=0; Direction is (x, y) = (1, 0) (RIGHT); Magnitude = 1.3;
            /// </summary>
            else if ((clr.R.Equals(255)) && (clr.G.Equals(190)) && (clr.B.Equals(0)))
            {
                direction = "RIGHT";
                multiplier = 1.3f;
            }
            /// <summary>
            /// for Red=255, Green=255, Blue=0; Direction is (x, y) = (1, 0) (RIGHT); Magnitude = 1.0;
            /// </summary>
            else if ((clr.R.Equals(255)) && (clr.G.Equals(255)) && (clr.B.Equals(0)))
            {
                direction = "RIGHT";
                multiplier = 1.0f;
            }
            /// <summary>
            /// for Red=0, Green=255, Blue=255; Direction is (x, y) = (-1, 0) (LEFT); Magnitude = 1.0;
            /// </summary>
            else if ((clr.R.Equals(0)) && (clr.G.Equals(255)) && (clr.B.Equals(255)))
            {
                direction = "LEFT";
                multiplier = 1.0f;
            }
            /// <summary>
            /// for Red=0, Green=160, Blue=255; Direction is (x, y) = (-1, 0) (LEFT); Magnitude = 1.3;
            /// </summary>
            else if ((clr.R.Equals(0)) && (clr.G.Equals(160)) && (clr.B.Equals(255)))
            {
                direction = "LEFT";
                multiplier = 1.3f;
            }
            /// <summary>
            /// for Red=0, Green=130, Blue=255; Direction is (x, y) = (-1, 0) (LEFT); Magnitude = 1.6;
            /// </summary>
            else if ((clr.R.Equals(0)) && (clr.G.Equals(130)) && (clr.B.Equals(255)))
            {
                direction = "LEFT";
                multiplier = 1.6f;
            }
            /// <summary>
            /// for Red=0, Green=0, Blue=255; Direction is (x, y) = (-1, 0) (LEFT); Magnitude = 2.0;
            /// </summary>
            else if ((clr.R.Equals(0)) && (clr.G.Equals(0)) && (clr.B.Equals(255)))
            {
                direction = "LEFT";
                multiplier = 2.0f;
            }
            /// <summary>
            /// for Red=0, Green=0, Blue=0; Direction is (x, y) = (0, -1) (UP); Magnitude = 1;
            /// </summary>
            else if ((clr.R.Equals(0)) && (clr.G.Equals(0)) && (clr.B.Equals(0)))
            {
                direction = "UP";
                multiplier = 1.0f;
            }
            /// <summary>
            /// for Red=255, Green=255, Blue=255; Direction is (x, y) = (0, 1) (DOWN); Magnitude = 0.2;
            /// </summary>
            else if ((clr.R.Equals(255)) && (clr.G.Equals(255)) && (clr.B.Equals(255)))
            {
                direction = "DOWN";
                multiplier = 0.2f;
            }
            /*Trace.WriteLine("Color: " + clr.ToString() + 
                "; Direction: " + direction + 
                "; Power: " + multiplier.ToString());*/
        }
        #endregion
    }
}
