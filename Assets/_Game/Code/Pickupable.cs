using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    private float baseY;
    private Vector3? toFollow;
    // Start is called before the first frame update
    void Start()
    {
        baseY = transform.position.y;
    }

    public void Follow(Vector3 position)
    {
        toFollow = position;
    }

    // Update is called once per frame
    void Update()
    {
        if (toFollow.HasValue)
        {
            transform.position = toFollow.Value; 
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
