using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SittingBirbSpriteFlipper : MonoBehaviour
{
    public Transform playerBirdTransform;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sprite.flipX = playerBirdTransform.position.x < transform.position.x;
    }
}
