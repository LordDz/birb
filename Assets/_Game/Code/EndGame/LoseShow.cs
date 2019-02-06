using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseShow : MonoBehaviour
{
    private AudioSource musicDefeat;
    private WindowSelector windowSelector;
    private SittingBirbSpriteFlipper sittingBirbSpriteFlipper;
	private GameObject reload;
	
    // Start is called before the first frame update
    void Start()
    {
        musicDefeat = GetComponent<AudioSource>();
        windowSelector = GameObject.FindObjectOfType<WindowSelector>();
        sittingBirbSpriteFlipper = GameObject.FindObjectOfType<SittingBirbSpriteFlipper>();
		reload=GameObject.Find("Reload");
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLose()
    {
        this.gameObject.SetActive(true);
        sittingBirbSpriteFlipper.gameObject.SetActive(false);
        if (windowSelector != null)
        {
            windowSelector.HideAllCoverFromProps();
        }
        musicDefeat.Play();
		GameObject.Find("Egg").SetActive(false);
		reload.GetComponent<reloadScene>().reload();
    }
}
