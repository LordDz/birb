using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinShow : MonoBehaviour
{
    private AudioSource musicVictory;
    private WindowSelector windowSelector;
    private SittingBirbSpriteFlipper sittingBirbSpriteFlipper;
    private bool hasWon = false;
    // Start is called before the first frame update
    void Start()
    {
        musicVictory = GetComponent<AudioSource>();
        windowSelector = GameObject.FindObjectOfType<WindowSelector>();
        sittingBirbSpriteFlipper = GameObject.FindObjectOfType<SittingBirbSpriteFlipper>();
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowVictory()
    {
        if (hasWon)
        {
            return;
        }
        hasWon = true;
        this.gameObject.SetActive(true);
        sittingBirbSpriteFlipper.gameObject.SetActive(false);
        if (windowSelector != null)
        {
            windowSelector.HideAllCoverFromProps();
        }
        musicVictory.Play();
    }
}
