using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    public int startingPoolSize;
    public GameObject[] prefabs;

    private List<GameObject> pool;
    private bool notInitialized = true;

    // Start is called before the first frame update
    void Start()
    {
        if (notInitialized)
            Init();
    }

    private void Init()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < startingPoolSize; i++)
        {
            pool.Add(Instantiate(prefabs[Random.Range(0, prefabs.Length)]));
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

        pool.Add(Instantiate(prefabs[Random.Range(0, prefabs.Length)]));
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
}
