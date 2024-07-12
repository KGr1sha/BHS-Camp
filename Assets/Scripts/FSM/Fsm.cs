using System;
using System.Collections.Generic;

public class Fsm
{
    private FsmState CurrentState { get; set; }
    private Dictionary<Type, FsmState> _states = new();

    public void AddState(FsmState state)
    {
        _states.Add(state.GetType(), state);
    }

    public void SetState<T>() where T : FsmState
    {
        var type = typeof(T);

        if (CurrentState.GetType() == type)
            return;

        if (_states.TryGetValue(type, out var newState))
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }

    public void Update()
    {
        CurrentState?.Update();
    }
}