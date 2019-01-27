using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    private float heat = 0.0f;
    private float baseY;
    private Vector3? toFollow;
    private Quaternion toFollowRotation;
    private bool hasOwner = false;

    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        baseY = transform.position.y;
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void Follow(Vector3 position, Quaternion rotation)
    {
        toFollow = position;
        toFollowRotation = rotation;
    }

    public bool HasOwner()
    {
        return hasOwner;
    }

    public void SetOwner()
    {
        hasOwner = true;
    }

    public void UnsetOwner()
    {
        hasOwner = false;
    }

    public void HeatUp(float amount)
    {
        heat = Mathf.Min(heat + amount, 1.0f);
    }

    public float GetHeat()
    {
        return heat;
    }

    // Update is called once per frame
    void Update()
    {
        heat -= 0.05f * heat * Time.deltaTime;
        if (heat < 0.0f)
        {
            heat = 0.0f;
        }
        sprite.color = new Color(1.0f, 1.0f - heat, 1.0f - heat, 1.0f);
        if (toFollow.HasValue)
        {
            float limit = 15.0f * Time.deltaTime;
            Vector3 translation = toFollow.Value - transform.position;
            if (translation.magnitude <= limit)
            {
                transform.position = toFollow.Value;
            }
            else
            {
                transform.position += limit * translation / (translation.magnitude);
            }
            transform.rotation = toFollowRotation;
            toFollow = null;
        }
        else
        {
            transform.position = new Vector3(
                    transform.position.x,
                    baseY,
                    transform.position.z);
        }
    }
}
