using UnityEngine;

namespace RPG.Core
{
     public class Action : MonoBehaviour
    {
        IAction _currentAction;
        public void StartAction(IAction action)
        {
            if (_currentAction == action) return;
            _currentAction?.Cancel();
            _currentAction = action;
        }

        public void StopAction()
        {
            StartAction(null);
        }
    }
}
