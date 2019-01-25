using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private PeterEatBehaviour PeterEatBehaviour;
    private bool isActive = false;
    void Start()
    {
        PeterEatBehaviour = FindObjectOfType<PeterEatBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLook()
    {
        isActive = true;
    }

    public void StopLook()
    {
        isActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //"Become angry"
            PeterEatBehaviour.StartBecomeAngry();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PeterEatBehaviour.StopBecomeAngry();
        }
    }
}
