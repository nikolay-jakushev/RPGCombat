using System;
using RPG.Combat;
using RPG.Core;
using UnityEngine;

namespace  RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float _distance = 5f;
        private Fighter _fighter;
        private HealthComponent _healthComponent;
        private GameObject _player;

        private void Start()
        {
            _fighter = GetComponent<Fighter>();
            _healthComponent = GetComponent<HealthComponent>();
            _player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if(_healthComponent.IsDead()) return;
            if (AttackRangeOfPlayer() && _fighter.CanAttack(_player) )
            {
                _fighter.Attack(_player);
            }
            else
            {
                _fighter.Cancel();
            }
        }
        
        private bool AttackRangeOfPlayer()
        {
            float distancePlayer = Vector3.Distance(_player.transform.position, transform.position);
            return distancePlayer < _distance;
        }
    }
}

