using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistence : MonoSingleton<Persistence>
{
    public Dictionary<string, bool> bools = new();
    public Dictionary<string, int> ints = new();
    public Dictionary<string, float> floats = new();
    public Dictionary<string, string> strings = new();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        if (Ins) return;

        var name = "Persistence";
        var prefab = Resources.Load<GameObject>(name);
        Instantiate(prefab).name = name;
    }

    public static Coroutine RunCoroutine(IEnumerator routine) => Ins.StartCoroutine(routine);

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
}
