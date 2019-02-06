using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startGamePrep : MonoBehaviour
{
    public void prep()
    {
		GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
		GameObject.Find("FlyManager").GetComponent<FlyManager>().enabled = true;
		GameObject.Find("Foreground").GetComponent<SpriteRenderer>().enabled = true;
		spriteFadeInOut spr = GameObject.Find("Foreground").GetComponent<spriteFadeInOut>();
		spr.fade=1;
		spr.fadeIn=false;
		spr.run=true;
    }
}
