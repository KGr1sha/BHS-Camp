using UnityEngine;

public class EnemyFsm : MonoBehaviour
{
    [SerializeField] private float _patrolSpeed = 5f;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _chaseSpeed = 7f;
    private Fsm _fsm;
    private Rigidbody2D _body;

    private void Start()
    {
        _fsm = new Fsm();
        _body = GetComponent<Rigidbody2D>();
        _fsm.AddState(new PatrolState(_fsm, _patrolSpeed, _waypoints, _body));
        _fsm.SetState<PatrolState>();
    }
}
