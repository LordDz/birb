using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseShow : MonoBehaviour
{
    private AudioSource musicDefeat;
    private WindowSelector windowSelector;
    // Start is called before the first frame update
    void Start()
    {
        musicDefeat = GetComponent<AudioSource>();
        windowSelector = GameObject.FindObjectOfType<WindowSelector>();
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLose()
    {
        this.gameObject.SetActive(true);
        if (windowSelector != null)
        {
            windowSelector.HideAllCoverFromProps();
        }
        musicDefeat.Play();
    }
}
