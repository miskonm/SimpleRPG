using UnityEngine;
using UnityEngine.AI;

namespace SimpleRPG.Trash
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private NavMeshAgent _agent;

        private void Update()
        {
            _agent.destination = _target.position;
        }
    }
}
