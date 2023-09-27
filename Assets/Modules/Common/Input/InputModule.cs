using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputModule : MonoSingleton<InputModule>
{
    [SerializeField]
    private PlayerInput input;

    public static PlayerInput Input => Ins.input;
}
