using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoSingleton<PoolController>
{
    [SerializeReference, ReadOnly, DictionaryDrawerSettings]
    private Dictionary<GameObject, ObjectPool> pools = new();

    public GameObject GetObject(GameObject prefab)
    {
        if (!pools.ContainsKey(prefab)) CreatePool(prefab);
        return pools[prefab].GetObject();
    }

    public T GetObject<T>(GameObject prefab) where T : Component
    {
        return GetObject(prefab).GetComponent<T>();
    }

    public void ClearPools()
    {
        foreach (Transform obj in transform) Destroy(obj.gameObject);
        pools.Clear();
    }

    private void CreatePool(GameObject prefab)
    {
        var go = new GameObject(prefab.name + "Pool", typeof(ObjectPool));
        go.transform.SetParent(transform, false);
        ObjectPool pool = go.GetComponent<ObjectPool>();
        pool.SetPrefab(prefab);
        pools[prefab] = pool;
    }
}
