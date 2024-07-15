using UnityEngine;

public class ParallaxBehaviour : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _horizontalMovementMultiplier;
    [SerializeField, Range(0, 1)] private float _verticalMovementMultiplier;
    [SerializeField] private Transform _target;

    private Vector3 _targetPosition => _target.position;
    private Vector3 _lastTargetPosition;
    private float _textureWidth;

    private void Start()
    {
        _lastTargetPosition = _targetPosition;
    }

    private void Update()
    {
        Vector3 delta = _targetPosition - _lastTargetPosition;
        delta *= new Vector2(_horizontalMovementMultiplier, _verticalMovementMultiplier);
        transform.position += delta;
        _lastTargetPosition = _targetPosition;
    }
}
