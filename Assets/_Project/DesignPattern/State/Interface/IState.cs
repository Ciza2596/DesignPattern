namespace DesignPattern
{
	public interface IState
	{
		void OnEnter();
		void OnTick(float deltaTime);
		void OnLeave();
	}
}
