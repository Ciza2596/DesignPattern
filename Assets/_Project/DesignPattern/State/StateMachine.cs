using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace DesignPattern
{
	public sealed class StateMachine : IStateMachine
	{
		// private variable
		private IState _currentState;
		private IState _previousState;

		private readonly Dictionary<Type, IState> _stateMap = new Dictionary<Type, IState>();

		// public variable
		public int StateCount => _stateMap.Count;

		// public method
		public bool CheckHasState<T>() =>
			_stateMap.ContainsKey(typeof(T));

		public bool CheckIsCurrentState<T>() =>
			_currentState is T;

		public T GetState<T>() where T : class
		{
			var type = typeof(T);
			Assert.IsTrue(_stateMap.ContainsKey(type), $"[StateMachine::GetState] State is not in state machine {type}.");

			return (T)_stateMap[type];
		}

		public bool TryGetCurrentState<T>(out T currentState) where T : class
		{
			if (_currentState == null)
			{
				currentState = default;
				return false;
			}

			currentState = (T)_currentState;
			return true;
		}

		public bool GetPreviousState<T>(out T previousState) where T : class
		{
			if (_previousState == null)
			{
				previousState = default;
				return false;
			}

			previousState = (T)_previousState;
			return true;
		}

		public void AddState<T>(IState state)
		{
			var registeredType = typeof(T);
			Assert.IsFalse(_stateMap.ContainsKey(registeredType), $"[StateMachine::AddState] Duplicate state type {registeredType}.");

			var stateType = state.GetType();
			if (registeredType.Name != stateType.Name && !stateType.GetInterfaces().Contains(registeredType))
			{
				Debug.LogWarning($"[StateMachine::AddState] {stateType.Name} doesn't inherit {registeredType.Name}.");
				return;
			}

			_stateMap.Add(registeredType, state);
		}

		public void RemoveState<T>()
		{
			var type = typeof(T);
			Assert.IsTrue(_stateMap.ContainsKey(type), $"[StateMachine::RemoveState] State is not in state machine {type}.");
			_stateMap.Remove(type);
		}

		public void OnTick(float deltaTime) =>
			_currentState?.OnTick(deltaTime);

		public void Release()
		{
			_currentState  = null;
			_previousState = null;

			_stateMap.Clear();
		}

		public void ChangeState<T>() where T : class
		{
			if (_currentState != null)
			{
				_currentState.OnLeave();
				_previousState = _currentState;
			}

			var type = typeof(T);
			Assert.IsTrue(CheckHasState<T>(), $"[StateMachine::ChangeState] State is not in state machine {type}.");

			_currentState = GetState<T>() as IState;
			_currentState.OnEnter();
		}
	}
}
