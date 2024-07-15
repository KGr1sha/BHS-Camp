using UnityEngine;

namespace BHSCamp
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Move : MonoBehaviour, IMove
    {
        [SerializeField, Range(0f, 5f)] private float _maxAcceleration = 0.3f;
        [SerializeField, Range(0f, 10f)] private float _maxAirAcceleration = 2f;

        private Rigidbody2D _body;
        private Ground _ground;
        private Vector2 _desiredVelocity, _velocity;

        private float _maxSpeed;
        private float _acceleration;
        private bool _onGround;

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _ground = GetComponent<Ground>();
        }

        private void FixedUpdate()
        {
            _onGround = _ground.OnGround;
            _velocity = _body.velocity;

            _acceleration = _onGround ? _maxAcceleration : _maxAirAcceleration;
            _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _acceleration);

            _body.velocity = _velocity;
        }

        public void SetVelocity(Vector2 direction, float speed)
        {
            _maxSpeed = speed;
            _desiredVelocity = direction * Mathf.Max(_maxSpeed - _ground.Friction, 0f);
        }
    }
}
