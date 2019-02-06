using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quit : MonoBehaviour
{
    void Update()
    {
		if(Input.GetKeyUp("escape"))
		{
			Debug.Log("quitting");
			Application.Quit();
		}
    }
}
