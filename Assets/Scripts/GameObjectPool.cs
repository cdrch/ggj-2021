using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    public int startingPoolSize;
    public GameObject[] prefabs;

    private List<GameObject> pool;
    private bool notInitialized = true;

    private int nextPrefabToAdd;

    // Start is called before the first frame update
    void Start()
    {
        if (notInitialized)
            Init();
    }

    // Init is used instead of just Start in case script execution order is bad
    public void Init()
    {
        nextPrefabToAdd = Random.Range(0, prefabs.Length);
        pool = new List<GameObject>();
        for (int i = 0; i < startingPoolSize; i++)
        {
            pool.Add(Instantiate(GetNextPrefab()));
        }

        foreach (GameObject go in pool)
        {
            go.SetActive(false);
        }

        notInitialized = false;
    }

    public Transform GetNext()
    {
        if (notInitialized)
            Init();

        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return pool[i].transform;
            }
        }

        pool.Add(Instantiate(GetNextPrefab()));
        pool[pool.Count - 1].SetActive(true);
        return pool[pool.Count - 1].transform;
    }

    public void TrimToSize(int newSize)
    {
        foreach (GameObject go in pool)
        {
            if (pool.Count <= newSize)
            {
                break;
            }

            if (!go.activeInHierarchy)
            {
                pool.Remove(go);
            }
        }
    }

    // Ensures approximately equal distribution of prefabs
    private GameObject GetNextPrefab()
    {
        GameObject go = prefabs[nextPrefabToAdd];

        nextPrefabToAdd++;
        if (nextPrefabToAdd >= prefabs.Length)
            nextPrefabToAdd = 0;

        return go;
    }
}
