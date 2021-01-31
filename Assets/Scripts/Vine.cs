using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour
{
    private const float ODDS_TO_BE_BLANK = 0.75f;

    private bool init = false;
    private SpriteRenderer sr;

    void Init()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        init = true;
    }

    void Awake()
    {
        if (!init)
            Init();

        if (Random.Range(0f, 1f) < ODDS_TO_BE_BLANK)
        {
            sr.enabled = false;
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().enabled = true;
        }

        //sr.color = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
        float h, s, v;
        Color.RGBToHSV(Color.white, out h, out s, out v);
        sr.color = Color.HSVToRGB(h, Random.Range(0.95f, 1f), Random.Range(0.95f, 1f));
    }
}
