using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorRopeSpawner : MonoBehaviour
{

    public static GameObjectPool frontPool;
    public static GameObjectPool backPool;
    public GameObject frontRope;
    public GameObject backRope;
    public List<GameObject> frontRopes;
    public List<GameObject> backRopes;
    public List<Transform> frontRopesXY;
    public List<Transform> backRopesXY;
    public Rigidbody2D Elevator;
    public Vector2 lastDespawnXY = new Vector2(0f,0f);
    public Vector2 lastSpawnXY = new Vector2(0f,0f);
    private float height = 51f / 128f;
    private Vector2 ropeHeight;
    public Vector2 backOffset = new Vector2(1.825f, 3.682f);
    public Vector2 frontOffset = new Vector2(2.24684f, 3.747f);
    public Vector2 initialBackXY = new Vector2(1.825f, 3.682f);
    public Vector2 initialFrontXY = new Vector2(2.24684f, 3.747f);

    public int ropeCount = 50;

    // Start is called before the first frame update
    void Start()
    {
        Elevator = GameObject.FindWithTag("Elevator").GetComponent<Rigidbody2D>();
        initialBackXY += Elevator.position;
        initialFrontXY += Elevator.position;
        frontRopes = new List<GameObject>();
        backRopes = new List<GameObject>();
        GameObject frontRopeObj;
        GameObject backRopeObj;
        ropeHeight = new Vector2(0f, height);
        for (int i=0; i < ropeCount; i++)
        {
            initialBackXY += ropeHeight;
            initialFrontXY += ropeHeight;
            frontRopeObj = Instantiate(frontRope);
            frontRopeObj.SetActive(true);
            backRopeObj = Instantiate(backRope);
            backRopeObj.SetActive(true);
            frontRopes.Add(frontRopeObj);
            backRopes.Add(backRopeObj);
            frontRopesXY.Add(frontRopeObj.transform);
            frontRopesXY[i].position = initialFrontXY;
            backRopesXY.Add(backRopeObj.transform);
            backRopesXY[i].position = initialBackXY;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForNewRopes();
    }

    private void CheckForNewRopes()
    {
        for(int j=0; j < frontRopes.Count; j++)
        {
            Debug.Log(Elevator.position.y + " > " + lastDespawnXY.y+20*height);
            if(Elevator.position.y > frontRopesXY[j].position.y - frontRopes.Count * height && !frontRopes[j].activeSelf)
            {
                frontRopes[j].SetActive(true);
                backRopes[j].SetActive(true);
            }
            if(Elevator.position.y > frontRopesXY[j].position.y - frontOffset.y && frontRopes[j].activeSelf)
            {
                frontRopes[j].SetActive(false);
                backRopes[j].SetActive(false);
                frontRopesXY[j].position = Elevator.position + frontOffset + frontRopes.Count * ropeHeight;
                backRopesXY[j].position = Elevator.position + backOffset + backRopes.Count * ropeHeight;
            }
        }
    }
}
