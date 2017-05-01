using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace High_as_a_Kite
{
    public class Kite : SpriteAnimation
    {

        #region Variables
        private Vector2 direction, randomDirection;
        private string desiredDirection, prevDir;
        private int yLowerBound, yUpperBound, xLowerBound, xUpperBound, counter;
        private Vector2 acceleration, velocity, previousAcceleration;
        private Random randomGen;
        #endregion

        #region Properties
        public string DesiredDirection
        {
            get
            { return desiredDirection; }
            set
            { desiredDirection = value; }
        }

        public float DeltaAcceleration
        {
            get
            { return (acceleration - previousAcceleration).LengthSquared(); }
        }
        #endregion

        #region Constructors
        public Kite() : base(1.0f, 1.0f, Vector2.Zero, 1, 1, 0, 1)
        {
            this.direction = Vector2.Zero;
            this.desiredDirection = "UP";
            this.acceleration = Vector2.Zero;
            this.velocity = Vector2.Zero;
        }

        public Kite(Vector2 position) : base(0.8f, 1.0f, Vector2.Zero, 1, 1, 0, 1)
        {
            this.Position = position;
            this.direction = new Vector2(0,1);
            this.randomDirection = direction;
            this.desiredDirection = "UP";
            this.prevDir = desiredDirection;
            this.randomGen = new Random(31);
            this.acceleration = Vector2.Zero;
            this.velocity = Vector2.Zero;
        }
        #endregion

        #region Methods
        public void init(ContentManager content, string kiteAsset)
        {
            loadContent(content, kiteAsset);
            randomGen.Next();
        }

        public void SetupBounds(Vector2 size)
        {
            yLowerBound = 0;
            yUpperBound = (int)size.Y - Game.SCREEN_HEIGHT;
            xLowerBound = 0;
            xUpperBound = (int)size.X - Game.SCREEN_WIDTH;
        }

        public new void loadContent(ContentManager content, string assetName)
        {
            base.loadContent(content, assetName);
        }

        /// <summary>
        /// Moves the kite in the direction og the Vector2 "direction"
        /// </summary>
        /// <param name="elapsedMilliseconds">Milliseconds since last move call</param>
        private void Move(int elapsedMilliseconds)
        {
            Position += Physics.Displacement(direction/3, acceleration, (float)elapsedMilliseconds);

            // Move Kite - Update Position (Takes Gravity into account)
            Position += Physics.Displacement(velocity, acceleration, (float)elapsedMilliseconds);

            // Update Velocity
            velocity = Physics.Velocity(velocity, acceleration, (float)elapsedMilliseconds);

            /// <summary>
            /// Friction is the first parameter. Decrease the value for higher friction.
            /// </summary>
            velocity *= 50f * (float)elapsedMilliseconds * 0.001f;

            /// <summary>
            /// Friction is the first parameter. Decrease the value for higher friction.
            /// </summary>
            acceleration *= 50f * (float)elapsedMilliseconds * 0.001f;
        }

        public void debugMove(int elapsedMilliseconds)
        {
            /// <summary>
            /// Movement speed when the arrow keys are pressed. FOR TEST ONLY!
            /// </summary>
            float debugSpeed = 10f;
            if (Input.Button == "left") xPosition -= debugSpeed;
            else if (Input.Button == "right") xPosition += debugSpeed;
            else if (Input.Button == "up") yPosition -= debugSpeed;
            else if (Input.Button == "down")  yPosition += debugSpeed;
        }

        private int CheckBounds()
        {
            int returnValue = 0;
            if (Position.Y < yLowerBound)
            {
                Position = new Vector2(Position.X, (float)yLowerBound);
                // Loose Game Condition
                returnValue = 1;
            }
            if (Position.Y > yUpperBound)
            {
                Position = new Vector2(Position.X, (float)yUpperBound);
                // Win Game Condition
                returnValue = -1;
            }
            if (Position.X < xLowerBound)
                Position = new Vector2((float)xLowerBound, Position.Y);
            if (Position.X > xUpperBound)
                Position = new Vector2((float)xUpperBound, Position.Y);
            return returnValue;
        }

        public int Update(GameTime gameTime, string dir, float multiplier)
        {
            DesiredDirection = dir;
            /// <summary>
            /// The last factor multipied is so as to "smooth" movement. It basically scales down the acceleration.
            /// </summary>
            previousAcceleration = acceleration;
            acceleration = (direction * Input.Strength * multiplier * 0.04f);
            Move(gameTime.ElapsedGameTime.Milliseconds);
            debugMove(gameTime.ElapsedGameTime.Milliseconds);
            handleRotation();
            base.UpdateFrame(gameTime.ElapsedGameTime.Milliseconds);
            return CheckBounds();
        }

        public void handleRotation()
        {
            /// <summary>
            /// Speed of rotation of the Kite.
            /// </summary>
            float randAngle = Physics.getAngleFromXUnitVector(randomDirection);
            float diff = Math.Abs(Rotation - randAngle);

            float rotSpeed = 0.04f;
            switch (desiredDirection)
            {
                case "UP":
                default:
                    if (Rotation < -0.53f)
                        Rotation += rotSpeed;
                    else if (Rotation > 0.53f)
                        Rotation -= rotSpeed;
                    else if ((Rotation > -0.53f) && (Rotation < 0.53f))
                    {
                        handleRandomRotation(-0.53, 0.53, desiredDirection);
                    }
                    break;
                case "LEFT":
                    if (Rotation < -1.90f)
                        Rotation += rotSpeed;
                    else if (Rotation > -0.53f)
                        Rotation -= rotSpeed;
                    else if ((Rotation > -1.90f) && (Rotation < -0.53f))
                    {
                        handleRandomRotation(-1.90, -0.53, desiredDirection);
                    }
                    break;
                case "DOWN":
                    if (Rotation <= 3.14f && Rotation >= 0f)
                        Rotation += rotSpeed;
                    else if (Rotation >= -3.14f && Rotation <= 0f)
                        Rotation -= rotSpeed;
                    break;
                case "RIGHT":
                    if (Rotation < 0.53f)
                        Rotation += rotSpeed;
                    else if (Rotation > 1.90f)
                        Rotation -= rotSpeed;
                    else if ((Rotation > 0.53f) && (Rotation < 1.90f))
                    {
                        handleRandomRotation(0.53, 1.90, desiredDirection);
                    }
                    break;
            }
            direction = Physics.RotateVector(new Vector2(0, -1), Rotation);
            direction.Normalize();
        }

        private void handleRandomRotation(double min, double max, string dir)
        {
            float randAngle = Physics.getAngleFromXUnitVector(randomDirection);
            if (randAngle > max || randAngle < min)
            {
                randomDirection = Physics.RotateVector(new Vector2(0, -1), Rotation);
            }
            randAngle = Physics.getAngleFromXUnitVector(randomDirection);
            float diff = Math.Abs(Rotation - randAngle);
            float rotSpeed = 0.04f;

            if (diff < 0.05)
            {
                counter--;
                if (counter < 0) { randomDirection = Physics.RotateVector(new Vector2(0, -1), rand(min, max)); counter = 14; }
            }
            else
            {
                if (Rotation > randAngle) Rotation -= rotSpeed / 3;
                else Rotation += rotSpeed / 3;
            }
        }

        public new void Draw(SpriteBatch batch)
        {
            Rectangle sourcerect = new Rectangle();
            sourcerect = new Rectangle(0, FrameHeight * RowIndex, FrameWidth, FrameHeight);
            Vector2 pos = new Vector2(Game.SCREEN_WIDTH/2, Game.SCREEN_HEIGHT/2);
            batch.Draw(Texture, pos, sourcerect, Color.White, Rotation, new Vector2(FrameWidth / 2, FrameHeight / 2), Scale, SpriteEffects.None, Depth);
        }

        /// <summary>
        /// Random Number generator.
        /// Only works if abs(min) == abs(max) for any values lower than 0!
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public float rand(double min, double max)
        {
            float rn = 0;
            if (min > max) return rn;
            if (min < 0)
            {
                rn = (float)randomGen.Next(0, Math.Abs((int)Math.Ceiling(max * 1000.0)));
                if (max < 0) rn = (float)randomGen.Next(Math.Abs((int)Math.Ceiling(max * 1000.0)), Math.Abs((int)Math.Ceiling(min * 1000.0))) * -1;
                else if (max > 0) if (randomGen.NextDouble() > 0.5) rn = rn * -1;
            }
            else rn = (float)randomGen.Next((int)Math.Abs(Math.Ceiling(min * 1000.0)), Math.Abs((int)Math.Ceiling(max * 1000.0)));
            rn = rn / 1000f;
            return rn;
        }
        #endregion
    }
}
