using BHSCamp;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private const string HurtTrigger = "Hurt";
    private const string IsDeadAnimatorParameter = "IsDead";

    private Animator _animator;
    private Rigidbody2D _body;
    private Ground _ground;

    private HealthComponent _healthComponent;
    private bool isDead = false;

    private float _inputX;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
        _healthComponent = GetComponent<HealthComponent>();
    }

    private void Update()
    {
        if (_inputX != 0)
            transform.localScale = new Vector2(
                Mathf.Sign(_inputX) * Mathf.Abs(transform.localScale.x),
                transform.localScale.y
            );

        _animator.SetFloat("VelocityX", Mathf.Abs(_body.velocity.x));
        _animator.SetFloat("VelocityY", _body.velocity.y);
        _animator.SetBool("IsJumping", !_ground.OnGround);
        _animator.SetBool(IsDeadAnimatorParameter, isDead);
    }

    private void OnEnable()
    {
        if (_healthComponent != null)
        {
            _healthComponent.OnDamageTaken.AddListener(TriggerHurtAnimation);
            _healthComponent.OnDeath.AddListener(EnableIsDeadParameter);
        }
    }

    private void OnDisable()
    {
        if (_healthComponent != null)
        {
            _healthComponent.OnDamageTaken.RemoveAllListeners();
            _healthComponent.OnDeath.RemoveAllListeners();
        }
    }

    public void SetInputX(float inputX)
    {
        _inputX = inputX;
    }

    public void EnableIsDeadParameter()
    {
        isDead = true;
    }

    public void DisableIsDeadParameter()
    {
        isDead = false;
    }

    private void TriggerHurtAnimation()
    {
        _animator.SetTrigger(HurtTrigger);
    }
}