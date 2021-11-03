using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 2.0f;
        [SerializeField] private float timeBetweenAttacks = 1f;
        private Transform _target;
        private float _timeSinceLateAttack = 0f;
        
        private void Update()
        {
            _timeSinceLateAttack += Time.deltaTime;
            
            if (_target == null) return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(_target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                InitAttackAnimation();
            }
        }
        
        public void Attack(CombatTarget combatTarget) 
        {
             GetComponent<Action>().StartAction(this);
             _target = combatTarget.transform;
        }
        
        public void Cancel()
        {
            _target = null;
        }

        private void InitAttackAnimation()
        {
            if (!(_timeSinceLateAttack > timeBetweenAttacks)) return;
            GetComponent<Animator>().SetTrigger("attack");
            _timeSinceLateAttack = 0;
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.position) < weaponRange;
        }

        private void Hit()
        {
            
        }
    }
}
