using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeterMain : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip clipInWindow;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEnabled()
    {
        audioSource.clip = clipInWindow;
        audioSource.Play();
    }
}
