using UnityEngine;
using UnityEngine.AI;

namespace SimpleRPG.Trash
{
    public class Mover : MonoBehaviour
    {
        private static readonly int ForwardSpeed = Animator.StringToHash("ForwardSpeed");

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;

        private Camera _cachedCamera;

        private void Start()
        {
            _cachedCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
                MoveToCursor();

            UpdateAnimation();
        }

        private void MoveToCursor()
        {
            Ray ray = _cachedCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
                _agent.destination = hit.point;
        }

        private void UpdateAnimation()
        {
            Vector3 velocity = _agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            _animator.SetFloat(ForwardSpeed, localVelocity.z);
        }
    }
}