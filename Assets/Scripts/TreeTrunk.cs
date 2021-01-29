using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTrunk : MonoBehaviour
{
    public GameObject treeTrunkBase;
    public GameObjectPool leftTrunkPool;
    public GameObjectPool rightTrunkPool;

    public float prefabHeight = 4.9375f;
    public float prefabHalfWidth = 4.5859375f / 2f;

    public float distanceBeforeLoadingNextPart = 50f;

    public int initialPartsToSpawn = 6;
    public int maxPartsToSpawn = 10;
    public int spawnCount = 0;

    private List<Transform> spawnedParts;

    private float nextOffsetYUp = 0f;
    private float nextOffsetYDown = 0f; // TODO: use this so parts spawn and unspawn going back down as well
    // Alternative: just leave a row of deadliness at the lowest current point if you try to go back?

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        spawnedParts = new List<Transform>();
        leftTrunkPool = transform.Find("Left Trunk Parts").GetComponent<GameObjectPool>();
        rightTrunkPool = transform.Find("Right Trunk Parts").GetComponent<GameObjectPool>();

        // Place the trunk base
        Instantiate(treeTrunkBase, Vector3.zero, Quaternion.identity);

        // Place the initial trunk pieces
        for (int i = 0; i < initialPartsToSpawn; i++)
        {
            SpawnNextTrunkLayer();
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (nextOffsetYUp - cam.transform.position.y < distanceBeforeLoadingNextPart)
        {
            SpawnNextTrunkLayer();
        }
    }

    private void SpawnNextTrunkLayer()
    {
        Transform leftPart = leftTrunkPool.GetNext();
        Transform rightPart = rightTrunkPool.GetNext();

        leftPart.position = new Vector2(-prefabHalfWidth, nextOffsetYUp);
        rightPart.position = new Vector2(prefabHalfWidth, nextOffsetYUp);        

        nextOffsetYUp = nextOffsetYUp + prefabHeight;

        spawnedParts.Add(leftPart);
        spawnedParts.Add(rightPart);

        spawnCount += 1;

        if (spawnedParts.Count > maxPartsToSpawn)
        {
            spawnedParts[0].gameObject.SetActive(false);
            spawnedParts.RemoveAt(0);

            spawnedParts[0].gameObject.SetActive(false);
            spawnedParts.RemoveAt(0);
        }
    }
}

public enum TreePart
{
    None, // as this will be the default, this is just used so we can easily catch unassigned parts, which can be hard to catch in a randomly-generated game
    TrunkLeft,
    TrunkRight,
    BranchLeft,
    BranchRight,
    TrunkBase
}