using System;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    public FSMState StateCurrent { get; private set; }

    Dictionary<string, FSMState> _states = new Dictionary<string, FSMState>();

    public FSM()
    {
            
    }

    public void SetState<T>(string newState) where T : FSMState
    {
        if (_states.TryGetValue(newState, out var stateFromStorage))
        {
            StateCurrent?.Exit();
            StateCurrent = stateFromStorage;
            StateCurrent.Enter();
        }
    }

    public void Update()
    {
        StateCurrent?.Update();
    }

    public void AddState(string name, FSMState state)
    {
        _states[name] = state;
    }
}
