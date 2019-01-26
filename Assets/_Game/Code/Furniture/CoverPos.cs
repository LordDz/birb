using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverPos : MonoBehaviour
{
    private WindowSelector windowSelector;
    public PeterDirection coveredFromDirection;

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        windowSelector = GameObject.FindObjectOfType<WindowSelector>();
        player = GameObject.FindObjectOfType<PlayerController>();
        SetEnabled(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEnabled(bool enabled)
    {
        this.gameObject.SetActive(enabled);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var dir = windowSelector.GetPeterSees();
            player.SetCovered(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            var dir = windowSelector.GetPeterSees();
            player.SetCovered(false);
        }
    }
}
