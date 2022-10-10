namespace DesignPattern
{
    public abstract class BaseState<T> : IState where T : IStateMachine
    {
        //protected variable
        protected T _stateMachine;

        //istate flow
        public abstract void OnEnter();
        public abstract void OnUpdate(float delta);
        public abstract void OnLeave();
        
        
        //public method
        public BaseState(T stateMachine)
        {
            _stateMachine = stateMachine;
        }
    }
}