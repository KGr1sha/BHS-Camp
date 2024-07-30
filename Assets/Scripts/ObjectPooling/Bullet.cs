using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Vector2 _direction;
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.position += new Vector3(_direction.x, _direction.y, 0) * (_speed * Time.deltaTime);
    }
}