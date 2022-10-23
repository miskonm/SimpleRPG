using SimpleRPG.Core;
using SimpleRPG.Movement;
using UnityEngine;

namespace SimpleRPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        private static readonly int AttackTrigger = Animator.StringToHash("Attack");
        private static readonly int StopAttackTrigger = Animator.StringToHash("StopAttack");

        [SerializeField] private float _weaponRange = 2f;
        [SerializeField] private float _timeBetweenAttacks = 1.5f;
        [SerializeField] private float _damage = 5f;

        private Health _target;
        private Mover _mover;
        private ActionScheduler _actionScheduler;
        private Animator _animator;

        private float _timeSinceLastAttack;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _actionScheduler = GetComponent<ActionScheduler>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;

            if (_target == null || _target.IsDead)
                return;

            float distance = Vector3.Distance(_target.transform.position, transform.position);
            if (distance <= _weaponRange)
            {
                _mover.Cancel();
                AttackBehaviour();
            }
            else
            {
                _mover.MoveTo(_target.transform.position);
            }
        }

        public bool CanAttack(CombatTarget target)
        {
            if (target == null)
                return false;
            
            Health targetHealth = target.GetComponent<Health>();
            return targetHealth != null && !targetHealth.IsDead;
        }

        public void Attack(CombatTarget target)
        {
            _actionScheduler.StartAction(this);
            _target = target.GetComponent<Health>();
        }

        public void Cancel()
        {
            _animator.ResetTrigger(AttackTrigger);
            _animator.SetTrigger(StopAttackTrigger);
            _target = null;
        }

        private void AttackBehaviour()
        {
            transform.LookAt(_target.transform);
            if (_timeSinceLastAttack < _timeBetweenAttacks)
                return;

            _animator.ResetTrigger(StopAttackTrigger );
            _animator.SetTrigger(AttackTrigger);
            _timeSinceLastAttack = 0;
        }

        // Animation event
        private void Hit()
        {
            if (_target == null)
                return;
            
            _target.TakeDamage(_damage);
        }
    }
}