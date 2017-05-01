using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace High_as_a_Kite
{
    public class Sprite
    {
        #region Variables
        private int columns, rows, rowIndex;
        private Texture2D texture;
        private float scale, depth, rotation;
        private Vector2 origin, position;
        private bool newMotion;
        private string motion;
        #endregion

        #region Properties
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public float Scale
        {
            get { return scale; }
            set
            {
                if (value != 0)
                    scale = value;
                else
                    scale = 1.0f;
            }
        }

        public float Depth
        {
            get { return depth; }
            set { depth = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public string Motion
        {
            get { return motion; }
            set
            {
                if (value != motion) 
                    newMotion = true;
                motion = value;
            }
        }

        public bool NewMotion
        {
            get { return newMotion; }
            set { newMotion = value; }
        }

        public int FrameWidth
        {
            get { return texture.Width / columns; }
        }

        public int FrameHeight
        {
            get { return texture.Height / rows; }
        }

        public int RowIndex
        {
            get { return rowIndex; }
            set { rowIndex = value; }
        }

        public float xPosition
        {
            get { return position.X; }
            set { position.X = value; }
        }

        public float yPosition
        {
            get { return position.Y; }
            set { position.Y = value; }
        }
        #endregion

        #region Constructors
        public Sprite()
        {
            this.Motion = string.Empty;
            this.newMotion = false;
            this.Scale = 1.0f;
            this.Depth = 0.0f;
            this.Origin = Vector2.Zero;
            this.columns = 0;
            this.rows = 0;
        }

        public Sprite(float scale, float depth, Vector2 origin)
        {
            this.newMotion = false;
            this.Scale = scale;
            this.Depth = depth;
            this.Origin = origin;
            this.columns = 1;
            this.rows = 1;
            this.Rotation = 0;
            this.position = new Vector2(0, 0);
            this.RowIndex = 0;
        }

        public Sprite(float scale, float depth, Vector2 origin, int columns, int rows, int rowIndex)
        {
            this.newMotion = false;
            this.Scale = scale;
            this.Depth = depth;
            this.Origin = origin;
            this.columns = columns;
            this.rows = rows;
            this.Rotation = 0;
            this.position = new Vector2(0, 0);
            this.RowIndex = rowIndex;
        }
        #endregion

        #region Methods

        public virtual void loadContent(ContentManager content, string assetName)
        {
            Texture = content.Load<Texture2D>(assetName);
        }

        public void Draw(SpriteBatch batch)
        {
            Rectangle sourcerect = new Rectangle();
            sourcerect = new Rectangle(0, FrameHeight * RowIndex, FrameWidth, FrameHeight);
            batch.Draw(Texture, Position, sourcerect, Color.White, Rotation, Origin, Scale, SpriteEffects.None, Depth);
        }

        #endregion
    }
}
