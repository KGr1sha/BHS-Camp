using UnityEngine;

namespace BHSCamp
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private float _horizontal;
        private IMove _movable;
        private Jump _jump;
        private AttackBase _attack;
        private Health _health;
        private PlayerAnimation _animation;
        private Ground _ground;
        private bool _isDead;

        private void OnEnable()
        {
            _health.OnDeath += HandleDeath;
        }

        private void OnDisable()
        {
            _health.OnDeath -= HandleDeath;
        }

        private void Awake()
        {
            _movable = GetComponent<IMove>();
            _jump = GetComponent<Jump>();
            _animation = GetComponent<PlayerAnimation>();
            _attack = GetComponent<AttackBase>();
            _ground = GetComponent<Ground>();
            _health = GetComponent<Health>();
        }

        private void Update()
        {
            if (_isDead) return;

            _horizontal = Input.GetAxisRaw("Horizontal");

            _horizontal = _attack.IsAttacking && _ground.OnGround? 0 : _horizontal;
            _movable.SetVelocity(new Vector2(_horizontal, 0), _speed);
            _animation.SetInputX(_horizontal);

            if (Input.GetButtonDown("Attack"))
                _attack.BeginAttack();

            if (Input.GetButtonDown("Jump"))
                _jump.Action();
        }

        private void HandleDeath()
        {
            _isDead = true;
        }
    }
}