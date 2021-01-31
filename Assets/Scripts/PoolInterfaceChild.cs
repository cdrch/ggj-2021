using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolInterfaceChild : MonoBehaviour
{
    public TreePart treePart;
    public List<GameObjectPool> pools;
    public int startingPoolSize;
    public GameObject[] prefabs;

    public void Init()
    {
        // make a pool for each
        foreach (GameObject prefab in prefabs)
        {
            GameObjectPool p = gameObject.AddComponent<GameObjectPool>();

            p.prefabs = new GameObject[] { prefab };
            p.startingPoolSize = startingPoolSize;
            p.Init();

            pools.Add(p);
        }
    }

    public Transform GetNext()
    {
        return pools[Random.Range(0, pools.Count)].GetNext();
    }
}
