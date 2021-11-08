using System;
using UnityEngine;

namespace  RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float _distance = 5f;

        private void Update()
        {
            if (MoveToPlayer() < _distance)
            {
                print("I see you!");                
            } 
        }
        
        private float MoveToPlayer()
        {
            GameObject player = GameObject.FindWithTag("Player");
            return Vector3.Distance(player.transform.position, transform.position);
        }
    }
}

