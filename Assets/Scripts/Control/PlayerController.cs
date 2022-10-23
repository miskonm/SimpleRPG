using SimpleRPG.Combat;
using SimpleRPG.Movement;
using UnityEngine;

namespace SimpleRPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Mover _mover;
        private Camera _cachedCamera;
        private Fighter _fighter;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _fighter = GetComponent<Fighter>();
        }

        private void Start()
        {
            _cachedCamera = Camera.main;
        }

        private void Update()
        {
            if (InteractWithCombat())
                return;

            if (InteractWithMovement())
                return;
        }

        private bool InteractWithCombat()
        {
            // TODO: Refactor
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (!_fighter.CanAttack(target))
                    continue;

                if (Input.GetMouseButtonDown(0))
                {
                    _fighter.Attack(target);
                }
                
                return true;
            }

            return false;
        }

        private bool InteractWithMovement()
        {
            // TODO: Refactor
            if (Input.GetMouseButton(0) && Physics.Raycast(GetMouseRay(), out RaycastHit hit))
            {
                _mover.StartMovingAction(hit.point);
                return true;
            }

            return false;
        }

        private Ray GetMouseRay() =>
            _cachedCamera.ScreenPointToRay(Input.mousePosition);
    }
}