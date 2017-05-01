using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace High_as_a_Kite
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        //Variables
        public static int SCREEN_WIDTH, SCREEN_HEIGHT;

        //System Classes
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //FileStream Log = new FileStream("Kite_Debug_Log.txt", FileMode.Create);

        //Game Classes
        Level level;
        Input input;
        MenuSystem menu;
        Cue backgroundMusic;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            /// <summary>
            /// This is where the input is setup.
            /// First parameter is the cooldown time (in milliseconds -> 1000 milSec = 1 Sec)
            /// Second parameter is the time over which the input is averaged (in milliseconds)
            /// Third parameter is the threshold for the strength, only values above this are accepted.
            /// </summary>
            input = new Input(750, 250, 1.5f);
            level = new Level();
            menu = new MenuSystem();
            //Trace.Listeners.Add(new TextWriterTraceListener(Log));
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            /// <summary>
            /// PreferredBackBufferWidth is the width of the game window that opens.
            /// PreferredBackBufferHeight is the height of the game window that opens.
            /// </summary>
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            /// <summary>
            /// Uncomment the next line to activate fullscreen mode.
            /// </summary>
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            SCREEN_WIDTH = graphics.GraphicsDevice.Viewport.Width;
            SCREEN_HEIGHT = graphics.GraphicsDevice.Viewport.Height;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            AudioManager.LoadAudio();
            /// <summary>
            /// This is where the sprites for the various objects in the game is loaded.
            /// second parameter is the sprite for KITE.
            /// third parameter is the sprite for the COLORMAP.
            /// fourth parameter is the background texture list for the BACKGROUND.
            /// </summary>
            level.LoadLevel(Content, "WaterColorKite", "ColorMaps\\windmap2dana", "Content\\Background\\Level1\\level1.background");
            menu.LoadFromFile(Content, "Content\\SplashScreens\\Kite.menu");
            // Uncomment and use this next line for a wiimote-Less game experience
            // Remember to comment out the line above.
            //menu.LoadFromFile(Content, "Content\\SplashScreens\\Kite_wiimoteLess.menu");


            // TODO: this needs to be fixed
            //backgroundMusic = AudioManager.BackgroundSoundBank.GetCue("Comptine");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            AudioManager.AudioEngine.Update();
            input.Update(gameTime);

            menu.Update(gameTime);
            switch (menu.DrawScreen)
            {
                case ScreenType.Game:
                    switch (level.Update(gameTime))
                    {
                        case -1:
                            menu.DrawScreen = ScreenType.LooseScreen;
                            break;
                        case 1:
                            menu.DrawScreen = ScreenType.WinScreen;
                            break;
                        default:
                            break;
                    }
                    break;
                case ScreenType.Exit:
                    Exit();
                    break;
                case ScreenType.ControlsScreen:
                    level.InitLevel();
                    break;
                default:
                    break;
            }

            // Allows the game to exit
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) ||
                (Keyboard.GetState().IsKeyDown(Keys.Escape)))
                this.Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (!backgroundMusic.IsPlaying)
            {
                backgroundMusic = AudioManager.BackgroundSoundBank.GetCue(backgroundMusic.Name);
                backgroundMusic.SetVariable("Volume", 4.0f);
                backgroundMusic.Play();
            }

            spriteBatch.Begin();
            switch (menu.DrawScreen)
            {
                case ScreenType.Game:
                    level.Draw(spriteBatch);
                    break;
                default:
                    menu.Draw(spriteBatch);
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            //Trace.Flush();
            //Log.Close();
            base.OnExiting(sender, args);
        }
    }
}
