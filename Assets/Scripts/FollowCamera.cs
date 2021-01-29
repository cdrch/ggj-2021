using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // TODO: consider making this a multi-target camera
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
