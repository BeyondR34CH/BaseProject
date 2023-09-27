using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateMachine<StateType>
{
    [ShowInInspector, ReadOnly]
    private State curState;
    [ShowInInspector, ReadOnly]
    private readonly Dictionary<StateType, State> states = new();

    public State CurState => curState;
    public Dictionary<StateType, State> States => states;

    public StateMachine<StateType> Binding(StateType type, State state)
    {
        if (!states.ContainsKey(type)) states.Add(type, state);
        return this;
    }

    public void Set(State state)
    {
        curState = state;
        curState.OnEnter();
    }

    public void Set(StateType type)
    {
        if (states.TryGetValue(type, out var state))
        {
            Set(state);
        }
        else Log.Error($"Unbound target state: {type}");
    }

    public void Update()
    {
        curState.OnUpdate();
    }

    public void Translate(State state)
    {
        curState.OnExit();
        Set(state);
    }

    public void Translate(StateType type)
    {
        curState.OnExit();
        Set(type);
    }
}
