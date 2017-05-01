using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework.Content;

namespace High_as_a_Kite
{
    public enum ScreenType
    {
        SplashScreen,
        ControlsScreen,
        WinScreen,
        LooseScreen,
        Game,
        None,
        Exit
    }

    class MenuSystem
    {
        private List<MenuScreen> menus = new List<MenuScreen>();
        public bool isGameRunning = false;
        private ScreenType currentMenuScreen = ScreenType.SplashScreen;

        public ScreenType DrawScreen
        {
            get { return currentMenuScreen; }
            set { currentMenuScreen = value; }
        }

        public void LoadFromFile(ContentManager content, string menusDefinitionFile)
        {
            using (StreamReader reader = new StreamReader(menusDefinitionFile))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine().Trim();

                    if (string.IsNullOrEmpty(line))
                        continue;

                    if (line.Contains("[SplashScreen]"))
                    {
                        MenuScreen splash = new MenuScreen();
                        string textureName = reader.ReadLine().Trim();
                        splash.LoadContent(content, textureName);
                        string delay = reader.ReadLine().Trim();
                        if (!string.IsNullOrEmpty(delay))
                            splash.Delay = (int.Parse(delay) > 0) ? (int.Parse(delay)) : -1;
                        splash.ScreenType = ScreenType.SplashScreen;
                        AddMenuScreen(splash);
                    }
                    else if (line.Contains("[ControlsScreen]"))
                    {
                        MenuScreen control = new MenuScreen();
                        string textureName = reader.ReadLine().Trim();
                        control.LoadContent(content, textureName);
                        string delay = reader.ReadLine().Trim();
                        if (!string.IsNullOrEmpty(delay))
                            control.Delay = (int.Parse(delay) > 0) ? (int.Parse(delay)) : -1;
                        control.ScreenType = ScreenType.ControlsScreen;
                        AddMenuScreen(control);
                    }
                    else if (line.Contains("[WinScreen]"))
                    {
                        MenuScreen win = new MenuScreen();
                        string textureName = reader.ReadLine().Trim();
                        win.LoadContent(content, textureName);
                        string delay = reader.ReadLine().Trim();
                        if (!string.IsNullOrEmpty(delay))
                            win.Delay = (int.Parse(delay) > 0) ? (int.Parse(delay)) : -1;
                        win.ScreenType = ScreenType.WinScreen;
                        AddMenuScreen(win);
                    }
                    else if (line.Contains("[LooseScreen]"))
                    {
                        MenuScreen loose = new MenuScreen();
                        string textureName = reader.ReadLine().Trim();
                        loose.LoadContent(content, textureName);
                        string delay = reader.ReadLine().Trim();
                        if (!string.IsNullOrEmpty(delay))
                            loose.Delay = (int.Parse(delay) > 0) ? (int.Parse(delay)) : -1;
                        loose.ScreenType = ScreenType.LooseScreen;
                        AddMenuScreen(loose);
                    }
                    else if (line.Contains("[None]"))
                    {
                        MenuScreen none = new MenuScreen();
                        string textureName = reader.ReadLine().Trim();
                        none.LoadContent(content, textureName);
                        string delay = reader.ReadLine().Trim();
                        if (!string.IsNullOrEmpty(delay))
                            none.Delay = (int.Parse(delay) > 0) ? (int.Parse(delay)) : -1;
                        none.ScreenType = ScreenType.None;
                        AddMenuScreen(none);
                    }
                }
            }
        }

        public void AddMenuScreen(MenuScreen screen)
        {
            menus.Add(screen);
        }

        public void Update(GameTime gameTime)
        {
            foreach (MenuScreen m in menus)
                if (m.ScreenType == currentMenuScreen)
                {
                    m.Update(gameTime);
                    switch (currentMenuScreen)
                    {
                        case ScreenType.SplashScreen:
                            if (m.Progress == 1)
                                currentMenuScreen = ScreenType.ControlsScreen;
                            else if (m.Progress == -1)
                                currentMenuScreen = ScreenType.Exit;
                            break;
                        case ScreenType.ControlsScreen:
                            if (m.Progress == 1)
                                currentMenuScreen = ScreenType.Game;
                            else if (m.Progress == -1)
                                currentMenuScreen = ScreenType.SplashScreen;
                            break;
                        case ScreenType.LooseScreen:
                            if (m.Progress == 1)
                                currentMenuScreen = ScreenType.ControlsScreen;
                            else if (m.Progress == -1)
                                currentMenuScreen = ScreenType.Exit;
                            break;
                        case ScreenType.WinScreen:
                            if (m.Progress == 1)
                                currentMenuScreen = ScreenType.ControlsScreen;
                            else if (m.Progress == -1)
                                currentMenuScreen = ScreenType.Exit;
                            break;
                        case ScreenType.None:
                            if (m.Progress == 1)
                                currentMenuScreen = ScreenType.SplashScreen;
                            else if (m.Progress == -1)
                                currentMenuScreen = ScreenType.Exit;
                            break;
                        default:
                            currentMenuScreen = ScreenType.Exit;
                            break;
                    }
                }
                else
                    m.Reset();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (MenuScreen m in menus)
                if (m.ScreenType == currentMenuScreen)
                {
                    m.Draw(spriteBatch);
                    return;
                }
        }
    }
}
