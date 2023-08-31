using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace DesignPattern
{
    public class StateMachine : IStateMachine
    {
        //private variable
        private IState _currentState;
        private IState _previousState;

        private Dictionary<Type, IState> _states = new Dictionary<Type, IState>();

        //public variable
        public int StateCount => _states.Count;

        //public method
        ~StateMachine()
        {
            _currentState = null;
            _previousState = null;

            _states.Clear();
            _states = null;
        }

        public virtual void AddState<T>(IState state)
        {
            var type = typeof(T);
            Assert.IsFalse(_states.ContainsKey(type),
                $"[StateMachine::AddState] Duplicate state type {type}.");
            _states.Add(type, state);
        }

        public virtual void RemoveState<T>(IState state)
        {
            var type = typeof(T);
            Assert.IsTrue(_states.ContainsKey(type), $"[StateMachine::RemoveState] State is not in state machine {type}.");
            _states.Remove(type);
        }

        public virtual void OnUpdate(float delta)
        {
            _currentState?.OnUpdate(delta);
        }

        public virtual void Clear() => _states.Clear();

        public virtual void ChangeState<T>() where T : IState
        {
            if (_currentState != null)
            {
                _currentState.OnLeave();
                _previousState = _currentState;
            }

            var type = typeof(T);
            Assert.IsTrue(HasState<T>(), $"[StateMachine::ChangeState] State is not in state machine {type}.");
            
            _currentState.OnEnter();
        }


        public T GetState<T>() where T : IState
        {
            var type = typeof(T);
            Assert.IsTrue(_states.ContainsKey(type),
                $"[StateMachine::GetState] State is not in state machine {type}.");

            return (T)_states[type];
        }


        public bool HasState<T>() where T : IState
        {
            return _states.ContainsKey(typeof(T));
        }


        public bool IsCurrentState<T>() where T : IState
        {
            return _currentState is T;
        }

        public T GetCurrentState<T>() where T : IState
        {
            Assert.IsNotNull(_currentState,
                $"[StateMachine::GetPreviousState] Hasnt previous state {typeof(T)}.");
            return (T)_currentState;
        }

        public T GetPreviousState<T>() where T : IState
        {
            Assert.IsNotNull(_previousState,
                $"[StateMachine::GetPreviousState] Hasnt previous state {typeof(T)}.");
            return (T)_previousState;
        }
    }
}