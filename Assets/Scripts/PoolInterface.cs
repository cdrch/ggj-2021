using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is used as a parent object of several child objects that are groups of children which have GameObjectPools, and acts as a convenient centralized source for multiple groupings of pooled objects
public class PoolInterface : MonoBehaviour
{
    Dictionary<string, PoolInterfaceChild> pools;
    private bool init = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!init)
            Init();
    }

    public void Init()
    {
        pools = new Dictionary<string, PoolInterfaceChild>();

        int childCount = transform.childCount;
        List<Transform> children = new List<Transform>();
        for (int i = 0; i < childCount; i++)
        {
            PoolInterfaceChild child = transform.GetChild(i).GetComponent<PoolInterfaceChild>();
            child.Init();
            pools.Add(child.treePart.ToString("G"), child);// G makes it turn into the actual names rather than ints
        }
        init = true;
    }

    public Transform GetNext(TreePart treePart)
    {
        return GetNext(treePart.ToString("G")); // "G" returns the actual string name, rather than the int representation
    }

    public Transform GetNext(string name)
    {
        if (!init)
            Init();

        PoolInterfaceChild value;
        pools.TryGetValue(name, out value);

        // TODO: put safeguards here

        return value.GetNext();
    }
}
