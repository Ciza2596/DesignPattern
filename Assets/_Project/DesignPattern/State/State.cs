namespace DesignPattern
{
	public abstract class State<T> : IState where T : IStateMachine
	{
		//protected variable
		protected T _stateMachine;

		protected State(T stateMachine) =>
			_stateMachine = stateMachine;

		public abstract void OnEnter();
		public abstract void OnTick(float deltaTime);
		public abstract void OnLeave();
	}
}
