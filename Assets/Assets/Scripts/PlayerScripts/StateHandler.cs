using System;
using System.Collections.Generic;
using UnityEngine;

public class StateHandler 
{
    private AbstractState currentState { get; set; }

    private Dictionary<Type, AbstractState> states = new Dictionary<Type, AbstractState>();

    public void AddState(AbstractState state)
    {
        var type = state.GetType();
        if (!states.ContainsKey(type))
        {
            states.Add(type, state);
        }
        else
        {
            Debug.LogWarning($"State {type} already exists, skipping");
        }
    }
    public void SetState<T>() where T : AbstractState
    {
        var type = typeof(T);
        if (currentState?.GetType() == type) return;

        if (states.TryGetValue(type, out var newState))
        {
            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }
        else
        {
            Debug.LogError($"State {type} not found! Did you forget to call AddState?");
        }
    }
    public void StateUpdate()
    {
        currentState?.Update();
    }
    public void FixedStateUpdate()
    {
        currentState?.FixedUpdate();
    }

}
