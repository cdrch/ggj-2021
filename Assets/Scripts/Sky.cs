using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    private Transform cam;

    public GameObject[] skies;
    public float[] transitionHeights;
    public bool[] manualHeight;
    private int currentSky = 0;

    private Vector3 nextSkyPoint = Vector3.zero;

    private float skyHeight = 15.7109375f;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameManager.instance.cam.transform;
        float height = GameManager.instance.treeTotalHeight / GameManager.SQUIRREL_LENGTH_IN_METERS;
        for (int i = 0; i < transitionHeights.Length; i++)
        {
            if (manualHeight[i])
                continue;

            transitionHeights[i] = height / transitionHeights.Length - 2.5f; // TODO: magic number is bad
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cam.position;

        if (nextSkyPoint.y < cam.position.y)
        {

        }
    }

    private void SwitchSky(int sky)
    {
        skies[currentSky].GetComponent<SpriteRenderer>().enabled = false;

        skies[sky].GetComponent<SpriteRenderer>().enabled = true;

        currentSky = sky;
    }

}
