using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        NavMeshAgent _navMeshAgent;
        private HealthComponent _healthComponent;
        
        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _healthComponent = GetComponent<HealthComponent>();
        }

        void Update()
        {
            _navMeshAgent.enabled = !_healthComponent.IsDead();
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<Action>().StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
           _navMeshAgent.destination = destination;
           _navMeshAgent.isStopped = false;
        }

        public void Cancel() 
        {
            _navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = _navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}