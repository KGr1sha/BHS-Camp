using Unity.VisualScripting;
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
        private PlayerAnimation _animation;

        private void Awake()
        {
            _movable = GetComponent<IMove>();
            _jump = GetComponent<Jump>();
            _animation = GetComponent<PlayerAnimation>();
        }

        private void Update()
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            _leftClicked = Input.GetButtonDown("Attack");

            _movable.SetDirectionX(_horizontal, _speed);
            if (_leftClicked)
            {
                _animation.SetAttackBool(1);
            } 

            _animation.SetInputX(_horizontal);

            if (Input.GetButtonDown("Jump"))
                _jump.Action();
        }
    }
}