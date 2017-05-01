using System;
using Microsoft.Xna.Framework;

namespace High_as_a_Kite
{
    public class Physics
    {
        /// <summary>
        /// The Variable that controls the quantity of Gravity (the actual value affecting gravity is the last parameter).
        /// </summary>
        //public static Vector2 Gravity = new Vector2(0.0f, -0.0228f); // final map (hard)
        public static Vector2 Gravity = new Vector2(0.0f, -0.0220f); //just like soren wants it!

        public static Vector2 Displacement(Vector2 velocity, Vector2 acceleration, float time)
        {
            Vector2 displacement = (velocity * time) + ((acceleration - Physics.Gravity) * time * time * 0.5f);
            return displacement;
        }

        public static Vector2 Velocity(Vector2 velocity, Vector2 acceleration, float time)
        {
            Vector2 newVelocity = velocity + (acceleration * time);
            return newVelocity;
        }

        /// <summary>
        /// Return the rotated vector assigned by the float angle in degrees
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Vector2 RotateVectorDegree(Vector2 vector, float angle)
        {
            //Handle rotation here
            /* newx = x*cos(angle) - y*sin(angle)
             * newy = x*sin(angle) + y*cos(angle)
             */
            double angleRadian = (double)angle * (180.0 / Math.PI);
            
            Vector2 rotatedVector = new Vector2((float)(vector.X*Math.Cos(angleRadian) - vector.Y*Math.Sin(angleRadian)) 
                                               ,(float)(vector.X*Math.Sin(angleRadian) + vector.Y*Math.Cos(angleRadian)));
            return rotatedVector;
        }

        /// <summary>
        /// Return the rotated vector assigned by the float angle in radians 
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Vector2 RotateVector(Vector2 vector, float angle)
        {
            //Handle rotation here
            /* newx = x*cos(angle) - y*sin(angle)
             * newy = x*sin(angle) + y*cos(angle)
             */
           
            Vector2 rotatedVector = new Vector2((float)(vector.X * Math.Cos(angle) - vector.Y * Math.Sin(angle))
                                               ,(float)(vector.X * Math.Sin(angle) + vector.Y * Math.Cos(angle)));
            return rotatedVector;
        }

        /// <summary>
        /// Get the angle between an angle and the x-unit vector (1,0)
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static float getAngleFromXUnitVector(Vector2 vector)
        {
            float angle = 0.0f;
            Vector2 normalizedVector = vector;
            Vector2.Normalize(normalizedVector);
            angle = Vector2.Dot(new Vector2(1, 0), normalizedVector);
            return angle;
        }

        public static float getAngleFromYUnitVector(Vector2 vector)
        {
            float angle = 0.0f;
            Vector2 normalizedVector = vector;
            Vector2.Normalize(normalizedVector);
            angle = Vector2.Dot(new Vector2(0, 1), normalizedVector);
            return angle;
        }

        /// <summary>
        /// Method that simulates the force of friction acting on a value.
        /// </summary>
        /// <param name="value">The value to "retard" - (float)</param>
        /// <param name="percent">Percent of the original value to be returned. Must be integer. Note that percent
        /// should be less than 100 for the value to retard. For values higher than 100, the value will actually increase.</param>
        /// <returns>value after a retarding factor has been applied</returns>
        public static float Retard(float value, int percent)
        {
            return (value * (percent / 100));
        }

        /// <summary>
        /// Method that simulates the force of friction acting on a value.
        /// </summary>
        /// <param name="value">The value to "retard" - (Vector2)</param>
        /// <param name="percent">Percent of the original value to be returned. Must be integer. Note that percent
        /// should be less than 100 for the value to retard. For values higher than 100, the value will actually increase.</param>
        /// <returns>value after a retarding factor has been applied</returns>
        public static Vector2 Retard(Vector2 value, int percent)
        {
            return (value * (percent / 100));
        }
    }
}
