using UnityEngine;

public class PatrolState : FsmState
{
    private float _speed;
    private Transform[] _waypoints;
    private Rigidbody2D _body;
    private Vector3 _currentPosition => _body.transform.position;
    private int _currentIndex;


    public PatrolState(Fsm fsm, float speed, Transform[] waypoints, Rigidbody2D body) : base(fsm)
    {
        _speed = speed;
        _waypoints = waypoints;
        _body = body;
    }

    public override void Update()
    {
        
    }
}