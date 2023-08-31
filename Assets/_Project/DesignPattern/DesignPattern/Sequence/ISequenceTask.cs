using System.Threading.Tasks;

namespace DesignPattern
{
    public interface ISequenceTask
    {
        void Start();
        Task<SequenceExecutor.TaskStates> Update();
        void End();
    }
}