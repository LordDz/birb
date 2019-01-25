using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private PeterEatBehaviour PeterEatBehaviour;
    private MeshRenderer rend;
    private bool isActive = false;
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        rend.enabled = false;
        PeterEatBehaviour = FindObjectOfType<PeterEatBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLook()
    {
        rend.enabled = true;
        isActive = true;
        this.GetComponent<Collider>().enabled = true;
    }

    public void StopLook()
    {
        rend.enabled = false;
        isActive = false;
        this.GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive && other.tag == "Player")
        {
            //"Become angry"
            rend.material.color = new Color(1, 0, 0);
            PeterEatBehaviour.StartBecomeAngry();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isActive && other.tag == "Player")
        {
            PeterEatBehaviour.StopBecomeAngry();
            rend.material.color = new Color(0, 1, 0);
        }
    }
}
