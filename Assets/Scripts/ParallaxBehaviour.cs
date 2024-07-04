using UnityEngine;

public class ParallaxBehaviour : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _horizontalMovementMultiplier;
    [SerializeField, Range(0, 1)] private float _verticalMovementMultiplier;

    private Vector3 _cameraPosition => Camera.main.transform.position;
    private Vector3 _lastCameraPosition;
    private float _textureWidth;

    private void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        _textureWidth = sprite.texture.width / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 delta = _cameraPosition - _lastCameraPosition;
        delta *= new Vector2(_horizontalMovementMultiplier, _verticalMovementMultiplier);
        transform.position += delta;
        _lastCameraPosition = _cameraPosition;

        if (Mathf.Abs(_cameraPosition.x - transform.position.x) >= _textureWidth)
        {
            float offsetX = (_cameraPosition.x - transform.position.x) % _textureWidth;
            transform.position = new Vector3(_cameraPosition.x + offsetX, transform.position.y);
        }
    }
}
