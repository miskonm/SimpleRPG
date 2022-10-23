using UnityEngine;

namespace SimpleRPG.Combat
{
    public class Health : MonoBehaviour
    {
        private static readonly int DieTrigger = Animator.StringToHash("Die");

        [SerializeField] private float _health = 100f;

        private Animator _animator;
        
        public bool IsDead { get; private set; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void TakeDamage(float value)
        {
            _health = Mathf.Max(0, _health - value);

            if (_health == 0)
                Die();
        }

        private void Die()
        {
            if (IsDead)
                return;

            IsDead = true;
            _animator.SetTrigger(DieTrigger);
        }
    }
}