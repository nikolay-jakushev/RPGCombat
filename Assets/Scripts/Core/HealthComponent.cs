using UnityEngine;

namespace RPG.Core
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private float _health = 100f;

        private bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }
        
        public void TakeDamage(float damage)
        {
            _health = Mathf.Max(_health - damage, 0);
            if (_health == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<Action>().StopAction();
        }
    }
}