using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform objectToFollow;

    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    void FollowObject()
    {
        float followX = objectToFollow.transform.position.x;
        if (followX > transform.position.x + 2.5f)
        {
            transform.position = new Vector3(
                    followX - 2.5f,
                    originalPosition.y,
                    originalPosition.z);
        }
        else if (followX < transform.position.x - 2.5f)
        {
            transform.position = new Vector3(
                    followX + 2.5f,
                    originalPosition.y,
                    originalPosition.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (objectToFollow.gameObject.activeSelf)
        {
            FollowObject();
        }
        else
        {
            Vector3 direction = originalPosition - transform.position;
            if (direction.magnitude > 1.0f)
            {
                transform.position += direction / direction.magnitude;
            }
            else
            {
                transform.position = originalPosition;
            }
        }
    }
}
