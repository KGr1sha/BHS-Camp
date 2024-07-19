using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace BHSCamp
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject _playerObject;
        private Rigidbody2D _body;
        private Ground _ground;
        private Animator _animator;
        private Health _healthComponent;

        private void OnEnable()
        {
            _healthComponent.OnDamageTaken += EnableHurtParameter;
            _healthComponent.OnDeath += EnableIsDeadParameter;
        }

        private void OnDisable()
        {
            _healthComponent.OnDamageTaken -= EnableHurtParameter;
            _healthComponent.OnDeath -= EnableIsDeadParameter;
        }

        private void Awake()
        {
            _healthComponent = _playerObject.GetComponent<Health>();
            _ground = _playerObject.GetComponent<Ground>();
            _body = _playerObject.GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _animator.SetFloat("VelocityX", Mathf.Abs(_body.velocity.x));
            _animator.SetFloat("VelocityY", _body.velocity.y);
            _animator.SetBool("IsJumping", !_ground.OnGround);
        }

        public void SetInputX(float inputX)
        {
            if (inputX == 0) return;
            transform.localScale = new Vector2(
                Mathf.Sign(inputX) * Mathf.Abs(transform.localScale.x),
                transform.localScale.y
            );
        }

        private void EnableIsDeadParameter()
        {
            _animator.SetBool("IsDead", true);
        }

        private void EnableHurtParameter(int damage)
        {
            _animator.SetBool("Hurt", true);
        }

        public void DisableHurtParameter(float time)
        {
            Invoke(nameof(DisableHurtParameter), time);
        }

        private void DisableHurtParameter()
        {
            _animator.SetBool("Hurt", false);
        }
    }
}