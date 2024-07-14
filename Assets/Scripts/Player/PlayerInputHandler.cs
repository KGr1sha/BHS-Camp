using UnityEngine;

namespace BHSCamp
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        private float _horizontal;
        private bool _leftClicked;
        private IMove _movable;
        private Jump _jump;
        private AttackBase _attack;
        private PlayerAnimation _animation;
        private Ground _ground;

        private void Awake()
        {
            _movable = GetComponent<IMove>();
            _jump = GetComponent<Jump>();
            _animation = GetComponent<PlayerAnimation>();
            _attack = GetComponent<AttackBase>();
            _ground = GetComponent<Ground>();
        }

        private void Update()
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            _leftClicked = Input.GetButtonDown("Attack");

            _horizontal = _attack.IsAttacking && _ground.OnGround? 0 : _horizontal;
            _movable.SetDirection(new Vector2(_horizontal, 0), _speed);
            if (_leftClicked)
            {
                _attack.BeginAttack();
            } 

            _animation.SetInputX(_horizontal);

            if (Input.GetButtonDown("Jump"))
                _jump.Action();
        }
    }
}