using UnityEngine;

namespace RPG.Core
{
    class Action : MonoBehaviour
    {
        IAction _currentAction;
        public void StartAction(IAction action)
        {
            if (_currentAction == action) return;
            _currentAction?.Cancel();
            _currentAction = action;
        }
    }
}
