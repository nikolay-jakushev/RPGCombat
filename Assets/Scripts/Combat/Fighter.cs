using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 2.0f;
        [SerializeField] private float timeBetweenAttacks = 1f;
        [SerializeField] private float damageWeapon = 12.4f;
        
        private HealthComponent _target;
        private float _timeSinceLateAttack = Mathf.Infinity;
        
        private void Update()
        {
            _timeSinceLateAttack += Time.deltaTime;
            
            if (_target == null) return;
            
            if(_target.IsDead()) return;
            
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(_target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                InitAttackAnimation();
            }
        }
        
        public void Attack(GameObject combatTarget) 
        {
             GetComponent<Action>().StartAction(this);
             _target = combatTarget.GetComponent<HealthComponent>();
        }
        
        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }
            HealthComponent healthComponent = combatTarget.GetComponent<HealthComponent>();
            return healthComponent != null && !healthComponent.IsDead();
        }
        
        public void Cancel()
        {
            StopAttack();
            _target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        private void InitAttackAnimation()
        {
            transform.LookAt(_target.transform);
            if (!(_timeSinceLateAttack > timeBetweenAttacks)) return;
            TriggerAttack();
            _timeSinceLateAttack = 0;
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        private void Hit()
        {
            if(_target == null) { return; }
            _target.TakeDamage(damageWeapon);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) < weaponRange;
        }
    }
}
