using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SittingOnEggLogic : MonoBehaviour
{
    private bool isActive;
    private Vector3 activePosition;
    private Vector3 inactivePosition;

    public void SetActive(bool active)
    {
        if (active)
        {
            transform.position = activePosition;
        }
        else
        {
            transform.position = inactivePosition;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        activePosition = transform.position;
        inactivePosition = transform.position - new Vector3(0.0f, -10.0f, 0.0f);
        transform.position = inactivePosition;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
