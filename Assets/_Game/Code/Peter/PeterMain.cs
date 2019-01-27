using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeterMain : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip clipInWindow;
    private EyeLook eyeLook;
    public PeterDirection SeeDirection;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        eyeLook = GetComponentInChildren<EyeLook>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetEnabled()
    {
        audioSource.clip = clipInWindow;
        audioSource.Play();
        eyeLook.SetEnabled();
    }
}
