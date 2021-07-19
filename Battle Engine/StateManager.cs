using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Battle_Engine
{
    public interface IStateManager
    {
        GameState CurrentState { get; }

        event EventHandler StateChanged;

        void PushState(GameState state);
        void ChangeState(GameState state);
        void PopState();
        bool ContainsState(GameState state);
    }

    public class StateManager : GameComponent, IStateManager
    {

        private readonly Stack<GameState> _gameStates = new Stack<GameState>(); //benefits of stack

        public event EventHandler StateChanged;

        public GameState CurrentState
        {
            get { return _gameStates.Peek(); }
        }

        public StateManager(Game game) : base(game)
        {
            Game.Services.AddService(typeof(IStateManager), this); //IMPORTANT
        //Lastly, back in Game1, you create an instance of your class, and then add it to the services
        //collection with the type of the interface.
        }

        public void PushState(GameState state)
        {
            AddState(state);
            OnStateChanged();
        }

        private void AddState(GameState state)
        {

            _gameStates.Push(state);
            Game.Components.Add(state);
            StateChanged += state.StateChanged;
        }

        public void PopState()
        {
            if (_gameStates.Count != 0)
            {
                RemoveState();
                OnStateChanged();
            }
        }

        private void RemoveState()
        {
            GameState state = _gameStates.Peek();
            StateChanged -= state.StateChanged;
            Game.Components.Remove(state);
            _gameStates.Pop();
        }

        public void ChangeState(GameState state)
        {
            while (_gameStates.Count > 0)
                RemoveState();

            AddState(state);
            OnStateChanged();
        }

        public bool ContainsState(GameState state)
        {
            return _gameStates.Contains(state);
        }

        protected internal virtual void OnStateChanged()
        {
            if (StateChanged != null)
                StateChanged(this, null);
        }
    }
}
