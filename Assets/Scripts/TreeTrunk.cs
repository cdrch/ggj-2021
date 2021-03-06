﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTrunk : MonoBehaviour
{
    public GameObject treeTrunkBase;
    public GameObject treeTrunkTop;
    //public GameObjectPool leftTrunkPool;
    //public GameObjectPool rightTrunkPool;
    //public GameObjectPool leftBranchPool;
    //public GameObjectPool rightBranchPool;
    private PoolInterface poolInterface;

    public float prefabHeight = 4.9375f;
    public float prefabWidth = 4.5859375f;
    public float prefabHalfWidth = 4.5859375f / 2f;
    
    public float spawnDistance;
    public float despawnDistance;

    public int initialPartsToSpawn = 6;
    public int maxPartsToSpawn;
    public int spawnCount = 0;

    private List<Transform> spawnedParts;
    private List<Transform> spawnedBranches;
    private List<Transform> spawnedVines;

    private float nextOffsetYUp = 0f;
    private float nextOffsetYDown;

    private Camera cam;

    private bool topperPlaced = false;

    // Start is called before the first frame update
    void Start()
    {
        nextOffsetYDown = 0f;

        cam = Camera.main;

        poolInterface = GetComponentInChildren<PoolInterface>();
        

        spawnedParts = new List<Transform>();
        spawnedBranches = new List<Transform>();
        spawnedVines = new List<Transform>();

        // Place the trunk base
        Instantiate(treeTrunkBase, Vector3.zero, Quaternion.identity);

        // Place the initial trunk pieces
        DecreaseOffsetDownByOneLayer();
        for (int i = 0; i < initialPartsToSpawn; i++)
        {
            AddNewLayerGoingUp();
            CheckIfOffsetDownIsCloseEnoughToNeedANewLayer();
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        CheckIfOffsetUpIsCloseEnoughToNeedANewLayer();
        CheckIfOffsetDownIsCloseEnoughToNeedANewLayer();

        CheckIfOffsetUpIsFarEnoughToDespawn();
        CheckIfOffsetDownIsFarEnoughToDespawn();
    }

    private void RemoveTopLayer()
    {
        spawnedParts[spawnedParts.Count - 1].gameObject.SetActive(false);
        spawnedParts.RemoveAt(spawnedParts.Count - 1);

        spawnedParts[spawnedParts.Count - 1].gameObject.SetActive(false);
        spawnedParts.RemoveAt(spawnedParts.Count - 1);

        spawnedBranches[spawnedBranches.Count - 1].gameObject.SetActive(false);
        spawnedBranches.RemoveAt(spawnedBranches.Count - 1);

        spawnedBranches[spawnedBranches.Count - 1].gameObject.SetActive(false);
        spawnedBranches.RemoveAt(spawnedBranches.Count - 1);

        spawnedVines[spawnedVines.Count - 1].gameObject.SetActive(false);
        spawnedVines.RemoveAt(spawnedVines.Count - 1);

        spawnedVines[spawnedVines.Count - 1].gameObject.SetActive(false);
        spawnedVines.RemoveAt(spawnedVines.Count - 1);


        DecreaseOffsetUpByOneLayer();
    }

    private void RemoveBottomLayer()
    {
        spawnedParts[0].gameObject.SetActive(false);
        spawnedParts.RemoveAt(0);

        spawnedParts[0].gameObject.SetActive(false);
        spawnedParts.RemoveAt(0);

        spawnedBranches[0].gameObject.SetActive(false);
        spawnedBranches.RemoveAt(0);

        spawnedBranches[0].gameObject.SetActive(false);
        spawnedBranches.RemoveAt(0);

        spawnedVines[0].gameObject.SetActive(false);
        spawnedVines.RemoveAt(0);

        spawnedVines[0].gameObject.SetActive(false);
        spawnedVines.RemoveAt(0);

        IncreaseOffsetDownByOneLayer();
    }

    private void CheckIfOffsetUpIsCloseEnoughToNeedANewLayer()
    {
        if (cam.transform.position.y + spawnDistance >= nextOffsetYUp)
        {
            //Debug.Log(GameManager.instance.treeTotalHeight - GameManager.instance.treeTopHeight);
            //Debug.Log(nextOffsetYUp);
            if (nextOffsetYUp * GameManager.SQUIRREL_LENGTH_IN_METERS >= GameManager.instance.treeTotalHeight - GameManager.instance.treeTopHeight)
            {
                AddNewLayerGoingUp(true);
            }
            else
            {
                AddNewLayerGoingUp();
            }
            
        }
    }

    private void CheckIfOffsetDownIsCloseEnoughToNeedANewLayer()
    {
        if (cam.transform.position.y - spawnDistance <= nextOffsetYDown + prefabHeight && nextOffsetYDown >= 0f)
        {
            //Debug.Log("no " + cam.transform.position.y + " - " + distanceBeforeLoadingNextPart + " = " + (cam.transform.position.y - distanceBeforeLoadingNextPart) + " | " + nextOffsetYDown + " + " + prefabHeight + " = " + (nextOffsetYDown + prefabHeight));
            AddNewLayerGoingDown();
        }
    }

    private void CheckIfOffsetUpIsFarEnoughToDespawn()
    {
        if (cam.transform.position.y + despawnDistance < nextOffsetYUp)
        {
            RemoveTopLayer();
        }
    }

    private void CheckIfOffsetDownIsFarEnoughToDespawn()
    {
        if (cam.transform.position.y - despawnDistance > nextOffsetYDown + prefabHeight)
        {
            RemoveBottomLayer();
        }
    }

    private void AddNewLayerGoingUp(bool topper = false)
    {
        if (topper && !topperPlaced)
        {
            Instantiate(treeTrunkTop, new Vector2(0f, nextOffsetYUp), Quaternion.identity);

            Transform leftPart = poolInterface.GetNext(TreePart.TrunkTopBlank);
            Transform rightPart = poolInterface.GetNext(TreePart.TrunkTopBlank);

            leftPart.position = new Vector2(-prefabHalfWidth, nextOffsetYUp);
            rightPart.position = new Vector2(prefabHalfWidth, nextOffsetYUp);

            spawnedParts.Add(leftPart);
            spawnedParts.Add(rightPart);

            topperPlaced = true;
        }
        else if (topperPlaced)
        {
            Transform leftPart = poolInterface.GetNext(TreePart.TrunkTopBlank);
            Transform rightPart = poolInterface.GetNext(TreePart.TrunkTopBlank);

            leftPart.position = new Vector2(-prefabHalfWidth, nextOffsetYUp);
            rightPart.position = new Vector2(prefabHalfWidth, nextOffsetYUp);

            spawnedParts.Add(leftPart);
            spawnedParts.Add(rightPart);
        }
        else
        {
            Transform leftPart = poolInterface.GetNext(TreePart.TrunkLeft);
            Transform rightPart = poolInterface.GetNext(TreePart.TrunkRight);

            leftPart.position = new Vector2(-prefabHalfWidth, nextOffsetYUp);
            rightPart.position = new Vector2(prefabHalfWidth, nextOffsetYUp);

            spawnedParts.Add(leftPart);
            spawnedParts.Add(rightPart);
        }
        

        
        if (GameManager.instance.stage == GameStage.One || topperPlaced)
        {
            Transform leftBranch = poolInterface.GetNext(TreePart.BlankBranchLeft);
            Transform rightBranch = poolInterface.GetNext(TreePart.BlankBranchRight);

            leftBranch.position = new Vector2(-prefabHalfWidth, nextOffsetYUp);
            rightBranch.position = new Vector2(prefabHalfWidth, nextOffsetYUp);

            spawnedBranches.Add(leftBranch);
            spawnedBranches.Add(rightBranch);
        }
        else if (GameManager.instance.stage == GameStage.Two || GameManager.instance.stage == GameStage.Three)
        {
            Transform leftBranch = poolInterface.GetNext(TreePart.BranchLeft);
            Transform rightBranch = poolInterface.GetNext(TreePart.BranchRight);

            leftBranch.position = new Vector2(-prefabHalfWidth, nextOffsetYUp);
            rightBranch.position = new Vector2(prefabHalfWidth, nextOffsetYUp);

            spawnedBranches.Add(leftBranch);
            spawnedBranches.Add(rightBranch);
        }

        
        if (GameManager.instance.stage == GameStage.Three || topperPlaced)  
        {
            Transform leftVine = poolInterface.GetNext(TreePart.BlankVine);
            Transform rightVine = poolInterface.GetNext(TreePart.BlankVine);

            leftVine.position = new Vector2(-prefabHalfWidth, nextOffsetYUp);
            rightVine.position = new Vector2(prefabHalfWidth, nextOffsetYUp);

            spawnedVines.Add(leftVine);
            spawnedVines.Add(rightVine);
        }
        else if (GameManager.instance.stage == GameStage.One || GameManager.instance.stage == GameStage.Two)
        {
            Transform leftVine = poolInterface.GetNext(TreePart.VineLeft);
            Transform rightVine = poolInterface.GetNext(TreePart.VineRight);

            leftVine.position = new Vector2(-prefabHalfWidth, nextOffsetYUp);
            rightVine.position = new Vector2(prefabHalfWidth, nextOffsetYUp);

            spawnedVines.Add(leftVine);
            spawnedVines.Add(rightVine);
        }

        IncreaseOffsetUpByOneLayer();

        spawnCount += 1;
        //Debug.Log("Up: " + nextOffsetYUp);
    }

    private void AddNewLayerGoingDown()
    {
        Transform leftPart = poolInterface.GetNext(TreePart.TrunkLeft);
        Transform rightPart = poolInterface.GetNext(TreePart.TrunkRight);

        leftPart.position = new Vector2(-prefabHalfWidth, nextOffsetYDown);
        rightPart.position = new Vector2(prefabHalfWidth, nextOffsetYDown);

        spawnedParts.Insert(0, leftPart);
        spawnedParts.Insert(0, rightPart);

        if (GameManager.instance.stage == GameStage.Two || GameManager.instance.stage == GameStage.Three)
        {
            Transform leftBranch = poolInterface.GetNext(TreePart.BranchLeft);
            Transform rightBranch = poolInterface.GetNext(TreePart.BranchRight);

            leftBranch.position = new Vector2(-prefabHalfWidth, nextOffsetYDown);
            rightBranch.position = new Vector2(prefabHalfWidth, nextOffsetYDown);


            spawnedBranches.Insert(0, leftBranch);
            spawnedBranches.Insert(0, rightBranch);
        }
        else if (GameManager.instance.stage == GameStage.One)
        {
            Transform leftBranch = poolInterface.GetNext(TreePart.BlankBranchLeft);
            Transform rightBranch = poolInterface.GetNext(TreePart.BlankBranchRight);

            leftBranch.position = new Vector2(-prefabHalfWidth, nextOffsetYDown);
            rightBranch.position = new Vector2(prefabHalfWidth, nextOffsetYDown);


            spawnedBranches.Insert(0, leftBranch);
            spawnedBranches.Insert(0, rightBranch);
        }

        Transform leftVine = poolInterface.GetNext(TreePart.VineLeft);
        Transform rightVine = poolInterface.GetNext(TreePart.VineRight);

        leftVine.position = new Vector2(-prefabHalfWidth, nextOffsetYDown);
        rightVine.position = new Vector2(prefabHalfWidth, nextOffsetYDown);

        spawnedVines.Insert(0, leftVine);
        spawnedVines.Insert(0, rightVine);

        DecreaseOffsetDownByOneLayer();

        spawnCount += 1;
        //Debug.Log("Down: " + nextOffsetYDown);
    }

    private void IncreaseOffsetDownByOneLayer()
    {
        nextOffsetYDown += prefabHeight;
    }

    private void DecreaseOffsetDownByOneLayer()
    {
        nextOffsetYDown -= prefabHeight;
    }

    private void IncreaseOffsetUpByOneLayer()
    {
        nextOffsetYUp += prefabHeight;
    }

    private void DecreaseOffsetUpByOneLayer()
    {
        nextOffsetYUp -= prefabHeight;
    }

    /*private void SpawnNextTrunkLayer(bool goingUp)
    {
        // iterate over imaginary grid of trunk parts
        for (float x = -cam.orthographicSize * 2 * prefabWidth; x <= cam.orthographicSize * 2 * prefabWidth; x += prefabWidth) // consider Floor/Ceil in the x <= check?
        {
            for (float y = -cam.orthographicSize * 2 * prefabHeight; y <= cam.orthographicSize * 2 * prefabHeight; y += prefabHeight)
            {
                if (
            }
        }
    }*/
}

public enum TreePart
{
    None, // as this will be the default, this is just used so we can easily catch unassigned parts, which can be hard to catch in a randomly-generated game
    TrunkLeft,
    TrunkRight,
    BranchLeft,
    BranchRight,
    TrunkBase,
    TrunkTop,
    BlankBranchLeft,
    BlankBranchRight,
    VineLeft,
    VineRight,
    BlankVine,
    TrunkTopBlank
}