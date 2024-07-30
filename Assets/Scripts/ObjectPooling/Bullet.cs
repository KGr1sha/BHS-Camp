using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public event Action<Bullet> OnBulletHit;
    [SerializeField] private float _speed;
    private Vector3 _direction;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 newDirection)
    {
        _direction = newDirection;
        _rb.velocity = _direction * _speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        OnBulletHit?.Invoke(this);
    }
}