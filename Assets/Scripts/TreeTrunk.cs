using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTrunk : MonoBehaviour
{
    public GameObject treeTrunkPartPrefab;
    public GameObjectPool pool;

    public float prefabHeight;

    public float distanceBeforeLoadingNextPart = 50f;

    public int initialPartsToSpawn = 3;
    public int maxPartsToSpawn = 10;
    private List<Transform> spawnedParts;

    private Vector2 nextOffset = Vector2.zero;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        spawnedParts = new List<Transform>();
        pool = GetComponent<GameObjectPool>();
        for (int i = 0; i < initialPartsToSpawn; i++)
        {
            SpawnPart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (nextOffset.y - cam.transform.position.y < distanceBeforeLoadingNextPart)
        {
            SpawnPart();
        }
    }

    private void SpawnPart()
    {
        Transform part = pool.GetNext();

        part.position = nextOffset;

        nextOffset = new Vector2(part.position.x, part.position.y + prefabHeight);

        spawnedParts.Add(part);

        if (spawnedParts.Count > maxPartsToSpawn)
        {
            spawnedParts[0].gameObject.SetActive(false);
            spawnedParts.RemoveAt(0);
        }
    }
}
