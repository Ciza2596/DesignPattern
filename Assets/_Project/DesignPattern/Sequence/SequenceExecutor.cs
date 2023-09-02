using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesignPattern
{
    public class SequenceExecutor
    {
        //private variable
        private int _currentIndex;
        private bool _isFail;

        private ISequenceTask _currentTask;
        private List<ISequenceTask> _tasks;

        private Action<TaskStates> _onCompleteAction;


        //public variable
        public bool IsRunning => _currentIndex < _tasks.Count;
        public ISequenceTask CurrentTask => _currentTask;


        //public method
        public SequenceExecutor()
        {
            _tasks = new List<ISequenceTask>();
            _currentIndex = 0;
        }

        ~SequenceExecutor()
        {
            _tasks.Clear();
            _currentTask = null;
            _onCompleteAction = null;
        }

        public void AddTask(ISequenceTask task) => _tasks.Add(task);

        public async void Execute()
        {
            while (_currentIndex < _tasks.Count && !_isFail)
            {
                if (_currentTask == null)
                {
                    _currentTask = _tasks[_currentIndex];
                    _currentTask.Start();
                }

                var taskState = await _currentTask?.Update();

                if (taskState == TaskStates.Success)
                {
                    _currentIndex++;
                    _currentTask?.End();
                    _currentTask = null;
                }
                else if (taskState == TaskStates.Fail)
                {
                    _isFail = true;
                    break;
                }


                await Task.Yield();
            }

            _onCompleteAction?.Invoke(_isFail ? TaskStates.Fail : TaskStates.Success);
        }

        public void Abort()
        {
            _isFail = true;
            _currentTask?.End();
        }

        public void AddOnCompleteAction(Action<TaskStates> onCompleteAction) => _onCompleteAction += onCompleteAction;


        //data
        public enum TaskStates
        {
            Success,
            Fail
        }
    }
}