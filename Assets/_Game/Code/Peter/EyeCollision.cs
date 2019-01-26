using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private PeterEatBehaviour PeterEatBehaviour;
    private MeshRenderer rend;
    private bool isActive = false;
    private EyeLook eyeLook;
    private PlayerController player;

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        rend.enabled = false;
        PeterEatBehaviour = GetComponentInParent<PeterEatBehaviour>();
        eyeLook = GetComponentInParent<EyeLook>();
        player = GameObject.FindObjectOfType<PlayerController>();
        StopLook();
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
        rend.material = eyeLook.materialOk;
        this.GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (isActive && other.tag == "Player")
        {
            Debug.Log("aaaarrr ! " + other.tag);

            //"Become angry"
            
            if (player.IsCovered == false)
            {
                rend.material = eyeLook.materialDetected;
                Debug.Log("Detected Player!");
                PeterEatBehaviour.StartBecomeAngry();
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isActive && other.tag == "Player")
        {
            PeterEatBehaviour.StopBecomeAngry();
            rend.material = eyeLook.materialOk;
        }
    }
}
