using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public Action OnEnter;
    public Action OnUpdate;
    public Action OnExit;

    protected void DefaultAction() { }

    public State(Action Enter, Action Update, Action Exit)
    {
        OnEnter = Enter ?? DefaultAction;
        OnUpdate = Update ?? DefaultAction;
        OnExit = Exit ?? DefaultAction;
    }

    public State(Action Update)
    {
        OnEnter = DefaultAction;
        OnUpdate = Update ?? DefaultAction;
        OnExit = DefaultAction;
    }
}
