using DG.Tweening.Core.Easing;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    private string prefabName;
    private readonly Stack<EnterPoolObject> pool = new();

    [ShowInInspector, ReadOnly]
    private int count;

    private void Awake()
    {
        if (prefab) SetPrefab(prefab);
    }

    public void SetPrefab(GameObject go)
    {
        if (go == prefab)
        {
            Log.Warning("The new Prefab is the same as the old one");
            return;
        }
        ClearAll();
        prefab = go;
        prefabName = go.name;
    }

    public GameObject GetObject()
    {
        GameObject go = null;
        while (!go && pool.Count > 0) go = PopObject();
        if (go)
        {
            go.SetActive(true);
            return go;
        }
        else return CreateObject();
    }

    public T GetObject<T>() where T : Component
    {
        GameObject go = GetObject();
        return go ? go.GetComponent<T>() : null;
    }

    public void PushObject(EnterPoolObject po)
    {
        if (po) pool.Push(po);
    }

    private GameObject PopObject()
    {
        return pool.Pop().gameObject;
    }

    public void Clear()
    {
        while (pool.Count > 0) Destroy(pool.Pop().gameObject);
    }

    public void ClearAll()
    {
        foreach (Transform obj in transform) Destroy(obj.gameObject);
        pool.Clear();
        count = 0;
    }

    private GameObject CreateObject()
    {
        GameObject go = Instantiate(prefab, transform);
        go.SetName(prefabName + count++);
        EnterPoolObject po = go.GetComponent<EnterPoolObject>();
        if (!po) po = go.AddComponent<EnterPoolObject>();
        po.SetUp(PushObject);
        return go;
    }
}
