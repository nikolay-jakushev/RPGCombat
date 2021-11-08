using System;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace  RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float _distance = 5f;
        
        private Fighter _fighter;
        private HealthComponent _healthComponent;
        private GameObject _player;
        private Mover _mover;

        private Vector3 _position;

        private void Start()
        {
            _fighter = GetComponent<Fighter>();
            _healthComponent = GetComponent<HealthComponent>();
            _mover = GetComponent<Mover>();
            _player = GameObject.FindWithTag("Player");

            _position = transform.position;
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
                _mover.StartMoveAction(_position);
            }
        }
        
        private bool AttackRangeOfPlayer()
        {
            float distancePlayer = Vector3.Distance(_player.transform.position, transform.position);
            return distancePlayer < _distance;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _distance);
        }
    }
}

