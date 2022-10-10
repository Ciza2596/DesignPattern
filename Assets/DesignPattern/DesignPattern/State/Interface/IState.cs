namespace DesignPattern
{
    public interface IState
    {
        void OnEnter();
        void OnUpdate(float delta);
        void OnLeave();
    }
}