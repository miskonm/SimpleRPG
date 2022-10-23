using UnityEngine;

namespace SimpleRPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        private IAction _currentAction;

        public void StartAction(IAction value)
        {
            if (_currentAction == value)
                return;

            if (_currentAction != null)
                _currentAction.Cancel();

            _currentAction = value;
        }
    }
}