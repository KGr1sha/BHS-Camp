using UnityEngine;
using BHSCamp.FSM;

namespace BHSCamp
{
    // враг, который умеет только патрулировать
    public class PatrolEnemy : MonoBehaviour
    {
        [Header("Patroling")]
        [SerializeField] protected float _patrolSpeed;
        [SerializeField] protected Transform[] _waypoints;
        [SerializeField] protected float _idleTime;
        [Header("Other")]
        [SerializeField] protected float _staggerTime;
        [SerializeField] protected bool _respawn;
        [SerializeField] protected float _respawnTime;

        protected Rigidbody2D _body;
        protected Health _health;

        protected Fsm _fsm;
        protected Vector2 _forwardVector;

        protected virtual void OnEnable()
        {
            _health.OnDamageTaken += HandleHit;
            _health.OnDeath += HandleDeath;
        }

        protected virtual void OnDisable()
        {
            _health.OnDamageTaken -= HandleHit;
            _health.OnDeath -= HandleDeath;
        }

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _health = GetComponent<Health>();
        }

        private void Start()
        {
            InitializeStates();
            _fsm.SetState<PatrolState>();
            SetForwardVector(new Vector2(transform.localScale.x, 0));
        }

        protected virtual void InitializeStates()
        {
            _fsm = new Fsm();
            _fsm.AddState(new PatrolState(_fsm, this, _patrolSpeed, _waypoints));
            _fsm.AddState(new HurtState(_fsm, this, _staggerTime));
            _fsm.AddState(new IdleState(_fsm, this, _idleTime));
            _fsm.AddState(new DeadState(_fsm, this, _respawn, _respawnTime));
        }

        private void Update()
        {
            _fsm.Update(Time.deltaTime);
        }

        public virtual void SetForwardVector(Vector2 forward)
        {
            _forwardVector = forward;
            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x) * _forwardVector.x,
                transform.localScale.y,
                transform.localScale.z
            );
        }

        protected virtual void HandleHit(int damage)
        {
            _fsm.SetState<HurtState>();
        }

        protected virtual void HandleDeath()
        {
            _fsm.SetState<DeadState>();
        }
    }
}