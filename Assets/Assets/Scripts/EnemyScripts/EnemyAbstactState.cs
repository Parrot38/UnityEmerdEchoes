using JetBrains.Annotations;
using NUnit.Framework.Internal;
using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
public abstract class EnemyAbstactState
{
    protected readonly EnemyStateHandler stateHandler;
    public EnemyAbstactState(EnemyStateHandler handler)
    {
        stateHandler = handler;
    }
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
    public abstract void FixedUpdate();
}
