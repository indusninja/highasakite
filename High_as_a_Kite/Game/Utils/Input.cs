using System;
using WiimoteLib;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace High_as_a_Kite
{
    class Input
    {
        #region Variables
        private Wiimote mWiimote;
        private int mAverageTime, mTicker, mAverageCount;
        private float mThreshold, mAverage = 0;
        private string mState;
        public static int mTimeleft, mDelayTime;
        private static float mStrength = 0;
        //private static bool left = false, right = false, up = false, down = false;
        private static int buttonA = 0, buttonHome = 0;
        #endregion

        #region Properties
        public WiimoteState CurrentWiimoteState
        {
            get { return mWiimote.WiimoteState; }
        }

        public static int CooldownLeft
        {
            get
            { return mTimeleft; }
        }

        public static int Cooldown
        {
            get
            { return mDelayTime; }
        }
        
        public static float Strength
        {
            get 
            {
                float temp = mStrength;
                mStrength = 0;
                return temp;
            }
        }

        public static float NonZeroingStrength
        {
            get { return mStrength; }
        }

        //For debugging purpose
        public static string Button
        {
            get
            {
                /*if (left) return "left";
                else if (right) return "right";
                else if (up) return "up";
                else if (down) return "down";
                else*/ if (buttonA == 1) { buttonA = 0; return "A"; }
                else if (buttonHome == 1) { buttonHome = 0; return "HOME"; }
                else return "none";
            }
        }
        #endregion

        #region Constructors
        public Input()
        {
            InitializeInputReporter(1000, 300, 3);
        }

        public Input(int delayTime, int averageTime, float threshold)
        {
            InitializeInputReporter(delayTime, averageTime, threshold);
        }
        #endregion

        #region Methods
        private void InitializeInputReporter(int delayTime, int averageTime, float threshold)
        {
            mWiimote = new Wiimote();
            mWiimote.WiimoteExtensionChanged += wm_WiimoteExtensionChanged;
            try
            {
                mWiimote.Connect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            mWiimote.SetReportType(InputReport.IRAccel, true);
            mTicker = 0;
            mTimeleft = 0;
            mAverageCount = 1;
            mDelayTime = delayTime;
            mAverageTime = averageTime;
            mThreshold = threshold;
            mState = "READY";
        }

        internal void wm_WiimoteExtensionChanged(object sender, WiimoteExtensionChangedEventArgs args)
        {
            if (args.Inserted)
                mWiimote.SetReportType(InputReport.IRExtensionAccel, true);    // return extension data
            else
                mWiimote.SetReportType(InputReport.IRAccel, true);             // back to original mode
        }

        public void Update(GameTime gameTime)
        {
            Vector3 Values = new Vector3(CurrentWiimoteState.AccelState.Values.X, 
                CurrentWiimoteState.AccelState.Values.Y, 
                CurrentWiimoteState.AccelState.Values.Z);
            float magnitude = Values.Length();
            /*float keyBoardSpaceBar = (Keyboard.GetState().IsKeyDown(Keys.Space)) ? mThreshold : 0.0f;

            left = Keyboard.GetState().IsKeyDown(Keys.Left);
            right = Keyboard.GetState().IsKeyDown(Keys.Right);
            up = Keyboard.GetState().IsKeyDown(Keys.Up);
            down = Keyboard.GetState().IsKeyDown(Keys.Down);*/
            if ((buttonA == 0) && (CurrentWiimoteState.ButtonState.A))
                buttonA = -1;
            else if ((buttonA == -1) && (!CurrentWiimoteState.ButtonState.A))
                buttonA = 1;
            if ((buttonHome == 0) && (CurrentWiimoteState.ButtonState.Home))
                buttonHome = -1;
            else if ((buttonHome == -1) && (!CurrentWiimoteState.ButtonState.Home))
                buttonHome = 1;
            //Trace.WriteLine("Wiimote Button A: " + buttonA.ToString() + "; Button Home: " + buttonHome.ToString());

            /*if (keyBoardSpaceBar > 0)
            {
                mStrength = keyBoardSpaceBar;
                return;
            }*/
            switch (mState)
            {
                case "READY":
                    if (magnitude >= mThreshold)
                    {
                        mState = "AVERAGING";
                        mAverage = magnitude;
                        mAverageCount = 1;
                        mTicker = gameTime.ElapsedGameTime.Milliseconds;
                    }
                    break;
                case "AVERAGING":
                    if (mTicker > mAverageTime)
                    {
                        mStrength = mAverage / mAverageCount;
                        mTicker = 0;
                        mState = "COOLDOWN";
                    }
                    else
                    {
                        mAverage += magnitude;
                        mAverageCount++;
                        mTicker += gameTime.ElapsedGameTime.Milliseconds;
                    }
                    break;
                case "COOLDOWN":
                    if (mTicker > mDelayTime)
                    {
                        mState = "READY";
                        mTicker = 0;
                        mTimeleft = mTicker;
                    }
                    else
                    {
                        mTicker += gameTime.ElapsedGameTime.Milliseconds;
                        mTimeleft = mTicker;
                    }
                    break;
            }
        }
        #endregion
    }
}