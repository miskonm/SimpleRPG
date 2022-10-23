using System;
using SimpleRPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace SimpleRPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        private static readonly int ForwardSpeed = Animator.StringToHash("ForwardSpeed");

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;

        private ActionScheduler _actionScheduler;

        private void Awake()
        {
            _actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            UpdateAnimation();
        }

        public void StartMovingAction(Vector3 position)
        {
            _actionScheduler.StartAction(this);
            MoveTo(position);
        }

        public void MoveTo(Vector3 position)
        {
            _agent.destination = position;
            _agent.isStopped = false;
        }

        public void Cancel() =>
            _agent.isStopped = true;

        private void UpdateAnimation()
        {
            Vector3 velocity = _agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            _animator.SetFloat(ForwardSpeed, localVelocity.z);
        }
    }
}