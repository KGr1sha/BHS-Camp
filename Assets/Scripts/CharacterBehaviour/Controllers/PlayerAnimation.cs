using BHSCamp;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _body;
    private Ground _ground;
    private Controller _controller;

    private float _inputX;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
        _controller = GetComponent<Controller>();
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
    }

    public void SetInputX(float inputX)
    {
        _inputX = inputX;
    }
}
