using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Singleton inspired by: https://www.sitepoint.com/saving-data-between-scenes-in-unity/
public class MenuSingelton : MonoBehaviour 
{
    public static MenuSingelton Instance;
	public GameObject menu;

    void Awake ()   
       {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
			GameObject.Find("startGame").GetComponent<startGamePrep>().prep();
            Destroy (gameObject);
        }
      }
	  
	  void Start()
	  {
		  menu.SetActive(true);
	  }
}