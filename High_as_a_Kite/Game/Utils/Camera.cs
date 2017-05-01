using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace High_as_a_Kite
{
    public class Camera
    {
        #region Variables
        private Vector2 position;
        #endregion

        #region Properties
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        #endregion

        #region Constructors
        public Camera()
        {
            position = new Vector2();
        }
        #endregion

        #region Methods
        public void Update(Kite kite)
        {
            position = kite.Position;
        }
        #endregion
    }
}
