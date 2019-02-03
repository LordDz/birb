using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hightlightActionButton : MonoBehaviour
{
	private Vector3 initialPos;
	private Vector3 target = new Vector3(0.05f,-0.1f,0);
	private float percent = 0;
	private float speed = 10;
	private spriteFadeInOut spriteFade;
	private SpriteRenderer sprite;
	private bool buttonPressedLastFrame = false;
	public bool maxAlphaOnAnyKey = false;
	
    void Start()
    {
        initialPos=transform.localPosition;
		spriteFade=gameObject.GetComponent<spriteFadeInOut>();
		sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
		percent=Mathf.Clamp01(percent+Time.deltaTime*speed*(Input.GetButton("Action")?1:-1));
		transform.localPosition=Vector3.Lerp(initialPos,initialPos+Vector3.Scale(target,transform.localScale),percent);

		if(maxAlphaOnAnyKey)
		{
				bool buttonPressed = Input.anyKey;		
				
				if(buttonPressed)
				{
					spriteFade.alphaOverride=1;
				}
				else if(buttonPressedLastFrame)
				{
					spriteFade.alphaOverride=-1;
				}
				buttonPressedLastFrame=buttonPressed;
		}
    }
}
