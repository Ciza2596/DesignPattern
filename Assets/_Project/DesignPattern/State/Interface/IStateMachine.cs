namespace DesignPattern
{
	public interface IStateMachine
	{
		void AddState<T>(IState state);
		void RemoveState<T>();

		void OnTick(float deltaTime);
	}
}
