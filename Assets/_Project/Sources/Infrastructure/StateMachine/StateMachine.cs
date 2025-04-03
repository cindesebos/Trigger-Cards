using System;
using System.Collections.Generic;

namespace Sources.Gameplay.Runtime.Infrastructure
{
    public class StateMachine
    {
        private Dictionary<Type, State> _states = new Dictionary<Type, State>();

        private State _activeState;

        public void AddState(State state)
        {
            if(!_states.ContainsKey(state.GetType())) _states.Add(state.GetType(), state);
        }

        public void SetState<TState>() where TState : State
        {
            var type = typeof(TState);

            if(_activeState != null && _activeState.GetType() == type) return;

            if(_states.TryGetValue(type, out var newState))
            {
                _activeState?.Exit();

                _activeState = newState;

                _activeState.Enter();
            }
        }

        public void Update() => _activeState?.Update();

        public void FixedUpdate() => _activeState?.FixedUpdate();
    }
}