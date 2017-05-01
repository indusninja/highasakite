using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;

namespace High_as_a_Kite
{
    public class Level
    {
        // Level classes
        private Kite kite;
        private List<Cloud> clouds;
        private ColorMap colorMap;
        private Background background;
        //private ScoreSystem scoreSystem;
        private Camera camera;
        //private HUD windStrength, windDirection;
        private Windsock windSock;
        private Indicator indicator;
        private Cue windSound1, windSound2, paperArifact;
        private float volume1 = -9f, volume2 = -9f, volume3 = -9f;
        private float oldMagnitude;

        public Vector2 KitePosition
        {
            get { return kite.Position; }
            set { kite.Position = value; }
        }

        public Level()
        {
            background = new Background();
            colorMap = new ColorMap();
            camera = new Camera();
            clouds = new List<Cloud>();
            //windDirection = new HUD();
            //windStrength = new HUD();
            windSock = new Windsock();
            indicator = new Indicator();
        }

        public void LoadLevel(ContentManager content, string cKite, string cWindMap, string cBackground)
        {
            /*windDirection.Position = new Vector2((float)Game.SCREEN_WIDTH - 75, 30);
            windStrength.Position = new Vector2(50, 30);
            windStrength.LoadFont(content, "Fonts\\Courier New");
            windDirection.LoadFont(content, "Fonts\\Courier New");*/
            Sprite s = new Sprite();
            s.loadContent(content, cWindMap);
            colorMap = new ColorMap(s);

            background.LoadBackground(content, cBackground);

            kite = new Kite(new Vector2(background.Size.X / 2, (3 * background.Size.Y) / 4));
            kite.loadContent(content, cKite);
            windSock.init(content);
            indicator.init(content);

            InitLevel();
        }

        public void InitLevel()
        {
            /// <summary>
            /// Background size is divided by a number to know what is the starting position of the kite
            /// For Example: (background.Size.X /2, (3*background.Size.Y) / 4) means:
            /// position of kite is in the middle of the background (horizonally), and
            /// position of kite is 3/4th from the top of the background (vertically).
            /// </summary>
            kite.Position = new Vector2(background.Size.X / 2, (3 * background.Size.Y) / 4);
            kite.SetupBounds(background.Size);

            camera.Position = kite.Position;
        }

        private void updateLevelSounds(float newMagnitude, int winOrLoose)
        {
            if (winOrLoose == 0)
            {
                if (newMagnitude != oldMagnitude)
                {
                    try
                    {
                        windSound1.Stop(AudioStopOptions.AsAuthored);
                        windSound2.Stop(AudioStopOptions.AsAuthored);
                    }
                    catch (Exception ex) { }
                    int windState = (int)(newMagnitude * 10);
                    Trace.WriteLine(windState.ToString());
                    switch (windState)
                    {
                        case 2:
                            windSound1 = AudioManager.WindSoundBank.GetCue("WindWeak");
                            windSound1.SetVariable("Volume", volume1);
                            windSound1.Play();
                            windSound2 = AudioManager.WindSoundBank.GetCue("windArtifact1");
                            windSound2.SetVariable("Volume", volume2);
                            windSound2.Play();
                            break;
                        case 10:
                            windSound1 = AudioManager.WindSoundBank.GetCue("WindMedium");
                            windSound1.SetVariable("Volume", volume1);
                            windSound1.Play();
                            windSound2 = AudioManager.WindSoundBank.GetCue("windArtifact1");
                            windSound2.SetVariable("Volume", volume2);
                            windSound2.Play();
                            break;
                        case 12:
                            windSound1 = AudioManager.WindSoundBank.GetCue("WindMedium");
                            windSound1.SetVariable("Volume", volume1);
                            windSound1.Play();
                            windSound2 = AudioManager.WindSoundBank.GetCue("WindArtifact2");
                            windSound2.SetVariable("Volume", volume2);
                            windSound2.Play();
                            break;
                        case 16:
                            windSound1 = AudioManager.WindSoundBank.GetCue("WindStrong");
                            windSound1.SetVariable("Volume", volume1);
                            windSound1.Play();
                            windSound2 = AudioManager.WindSoundBank.GetCue("WindArtifact2");
                            windSound2.SetVariable("Volume", volume2);
                            windSound2.Play();
                            break;
                        case 20:
                            windSound1 = AudioManager.WindSoundBank.GetCue("WindStrong");
                            windSound1.SetVariable("Volume", volume1);
                            windSound1.Play();
                            windSound2 = AudioManager.WindSoundBank.GetCue("windArtifact1");
                            windSound2.SetVariable("Volume", volume2);
                            windSound2.Play();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    if (!windSound1.IsPlaying)
                    {
                        windSound1 = AudioManager.WindSoundBank.GetCue(windSound1.Name);
                        windSound1.SetVariable("Volume", volume1);
                        windSound1.Play();
                    }
                    if (!windSound2.IsPlaying)
                    {
                        windSound2 = AudioManager.WindSoundBank.GetCue(windSound2.Name);
                        windSound2.SetVariable("Volume", volume2);
                        windSound2.Play();
                    }
                }
            }
            else
            {
                try
                {
                    windSound1.Stop(AudioStopOptions.AsAuthored);
                    windSound2.Stop(AudioStopOptions.AsAuthored);
                }
                catch (Exception ex) { }
            }

            oldMagnitude = newMagnitude;
            /*if (kite.DeltaAcceleration > 0f)
            {
                if (paperArifact.IsPlaying)
                {
                    paperArifact.Stop(AudioStopOptions.Immediate);
                    //volume3 = (kite.DeltaAcceleration / 16f) * 100; volume3 -= 100;
                    paperArifact = AudioManager.PaperSoundBank.GetCue("paperArtifact1");
                    paperArifact.SetVariable("Volume", volume3);
                    paperArifact.Play();
                }
                else
                {
                    //volume3 = (kite.DeltaAcceleration / 16f) * 100; volume3 -= 100;
                    paperArifact = AudioManager.PaperSoundBank.GetCue("paperArtifact1");
                    paperArifact.SetVariable("Volume", volume3);
                    paperArifact.Play();
                }
            }*/
        }

        public int Update(GameTime gameTime)
        {
            //Trace.WriteLine("Level.Update - " + gameTime.TotalGameTime.ToString());
            string dir = "UP";
            float multiplier = 0.0f;
            colorMap.Update((int)kite.Position.X, (int)kite.Position.Y, out dir, out multiplier);
            /*windStrength.Update(multiplier.ToString());
            windDirection.Update(dir);*/
            windSock.update(gameTime, dir, multiplier);
            indicator.update();
            int winOrLoose = kite.Update(gameTime, dir, multiplier);
            updateLevelSounds(multiplier, winOrLoose);
            camera.Update(kite);
            return winOrLoose;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch, camera.Position);
            foreach (Cloud cloud in clouds)
                cloud.Draw(spriteBatch);
            kite.Draw(spriteBatch);
            /*windDirection.Draw(spriteBatch);
            windStrength.Draw(spriteBatch);*/
            windSock.Draw(spriteBatch);
            indicator.Draw(spriteBatch, false);
        }
    }
}
