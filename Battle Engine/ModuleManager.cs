using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Battle_Engine
{
    public static class ModuleManager
    {
        public static Dictionary<ModuleKey, Module> ModulesList = new Dictionary<ModuleKey, Module>();

        public static Module currentState = new Module();

        public static void ResetModule()
        {
            currentState?.Reset();
        }

        public static void AddModule(ModuleKey key, Module module)
        {
            ModulesList.Add(key, module);
        }

        public static void ActivateModule(ModuleKey moduleName)
        {
            currentState.active = false;

            if (ModulesList.ContainsKey(moduleName))
            {
                currentState = ModulesList[moduleName];
                currentState.Reset();
                currentState.active = true;
            }
        }

        public static void StopModule()
        {
            if (currentState != null)
            {
                currentState.active = false;
            }
        }

        public static void Update(GameTime gameTime)
        {
            currentState?.Update(gameTime);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            currentState?.Draw(spriteBatch);
        }

    }
}
