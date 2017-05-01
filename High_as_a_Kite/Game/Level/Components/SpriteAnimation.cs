using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace High_as_a_Kite
{
    public class SpriteAnimation : Sprite
    {
        #region Variables
        private int frames, currentFrame;
        private float timePerFrame, totalElapsed;
        #endregion

        #region Properties
        public int Frames
        {
            get
            { return frames;}
            set
            { frames = value;}
        }

        public int CurrentFrame
        {
            get
            {return currentFrame;}
            set
            {currentFrame = value;}
        }
        #endregion

        #region Constructors
        public SpriteAnimation() : base()
        {
            this.Frames = 0;
            this.currentFrame = 0;
        }

        public SpriteAnimation(float scale, float depth, Vector2 origin, int columns, int rows, int rowIndex, int frames) : base(scale, depth, origin, columns, rows, rowIndex)
        {
            this.Frames = frames;
            this.currentFrame = 0;
        }
        #endregion

        #region Methods
        public void UpdateFrame(float elapsed)
        {
            totalElapsed += elapsed/1000;
            if (totalElapsed > timePerFrame)
            {
                nextFrame();
                totalElapsed -= timePerFrame;
            }
        }

        private void nextFrame()
        {
            if (!NewMotion) currentFrame++;
            currentFrame = currentFrame % frames;
            //if (currentFrame == 0) currentFrame++;
            NewMotion = false;
        }

        public void loadContent(ContentManager content, string assetName, int framePerSec, Vector2 position)
        {
            Position = position;
            timePerFrame = (float)1/framePerSec;
            base.loadContent(content, assetName);
        }

        public void Draw(SpriteBatch batch, bool defaultFrame)
        {
            if (!defaultFrame)
            {
                Rectangle sourcerect = new Rectangle();
                sourcerect = new Rectangle(FrameWidth * CurrentFrame, FrameHeight * RowIndex, FrameWidth, FrameHeight);
                batch.Draw(Texture, Position, sourcerect, Color.White, Rotation, Origin, Scale, SpriteEffects.None, Depth);
            }
            else base.Draw(batch);

        }
        #endregion
    }
}
