using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Battle_Engine
{
    public static class ScreenManager
    {
        public static Dictionary<ScreenKey, Screen> screenList = new Dictionary<ScreenKey, Screen>();

        public static void ResetScreen(ScreenKey screenKey)
        {
            screenList[screenKey].Reset();
        }

        public static void AddScreen(ScreenKey key, Screen screen)
        {
            screenList.Add(key, screen);
        }

        public static void ShowScreen(ScreenKey screenName) //transition
        {
            if (screenList.ContainsKey(screenName))
            {
                screenList[screenName].Reset();
                screenList[screenName].active = true;
            }
        }

        public static void HideScreen(ScreenKey screenName)
        {
            if (screenList.ContainsKey(screenName))
            {
                screenList[screenName].active = false;
            }
        }

        public static void Update(GameTime gameTime)
        {
            foreach(Screen screen in screenList.Values)
            {
                if (screen.active)
                {
                    screen.Update(gameTime);
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (Screen screen in screenList.Values)
            {
                if (screen.active)
                {
                    screen.Draw(spriteBatch);
                }
            }
        }

    }
}

