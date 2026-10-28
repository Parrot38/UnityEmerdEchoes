using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateHandler
{
    private EnemyAbstactState currentState { get; set; }

    private Dictionary<Type, EnemyAbstactState> states = new Dictionary<Type, EnemyAbstactState>();

    public void AddState(EnemyAbstactState state)
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
    public void SetState<T>() where T : EnemyAbstactState
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
