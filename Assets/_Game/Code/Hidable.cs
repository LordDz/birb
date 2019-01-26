using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidable : MonoBehaviour
{
    private bool isHiding;
    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
    }

    public void Hide()
    {
        transform.localPosition = new Vector3(0.0f, -10.0f, 0.0f);
        isHiding = true;
    }

    public void Unhide()
    {
        transform.localPosition = startPosition;
        isHiding = false;
    }

    public bool IsHiding()
    {
        return isHiding;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
