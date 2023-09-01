namespace DesignPattern
{
    public interface IStateMachine
    {
        void AddState<T>(IState state);
        void RemoveState<T>(IState state);
        
        void OnTick(float deltaTime);
    }
}