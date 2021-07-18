using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Engine
{
    public class ActionManager
    {
        public List<Action> namedActions = new List<Action>();

        public void SetAction(Action a)
        {
            namedActions.Add(a);
        }

        public void InvokeAction(int i)
        {
            namedActions[i]();
        }
    }
}
