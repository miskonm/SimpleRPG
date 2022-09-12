using UnityEngine;
using UnityEngine.AI;

namespace SimpleRPG.Trash
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private NavMeshAgent _agent;

        private Camera _cachedCamera;

        private void Start()
        {
            _cachedCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                MoveToCursor();
        }

        private void MoveToCursor()
        {
            Ray ray = _cachedCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
                _agent.destination = hit.point;
        }
    }
}