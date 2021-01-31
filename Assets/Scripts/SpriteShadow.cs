using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShadow : MonoBehaviour
{
    [SerializeField] private Vector2 offset = new Vector2(0, -0.046875f);
    [SerializeField] private Material shadowMaterial;
    [SerializeField] private Color shadowColor = Color.black;

    private SpriteRenderer rendererCaster;
    private SpriteRenderer rendererShadow;

    private Transform transformCaster;
    private Transform transformShadow;

    private void Start()
    {
        transformCaster = transform;
        transformShadow = new GameObject().transform;
        transformShadow.parent = transformCaster;
        transformShadow.gameObject.name = gameObject.name + " Shadow";
        transformShadow.localRotation = Quaternion.identity;

        rendererCaster = GetComponent<SpriteRenderer>();
        rendererShadow = transformShadow.gameObject.AddComponent<SpriteRenderer>();
        rendererShadow.sortingLayerName = rendererCaster.sortingLayerName;
        rendererShadow.sortingOrder = rendererCaster.sortingOrder - 1;
        rendererShadow.material = shadowMaterial;
        rendererShadow.color = shadowColor;
    }

    private void LateUpdate()
    {
        transformShadow.position = (Vector2)transformCaster.position + offset;
        rendererShadow.sprite = rendererCaster.sprite;
    }
}
