using JetBrains.Annotations;
using NUnit.Framework.Internal;
using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
public abstract class AbstractState
{
    protected readonly StateHandler stateHandler;
    public AbstractState(StateHandler handler)
    {
        stateHandler = handler;
    }
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
    public abstract void FixedUpdate();
}
