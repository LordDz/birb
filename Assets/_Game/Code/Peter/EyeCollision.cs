using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private PeterEatBehaviour PeterEatBehaviour;
    private MeshRenderer rend;
    private bool isActive = false;
    private EyeLook eyeLook;
    private PlayerController player;
	private Vector4 angryColor = new Vector4(2,0,0,1);
	private Vector4 okColor = new Vector4(1,1,0,1);
	private float angerFuryStartTime = -1;
	
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        rend.enabled = false;
        PeterEatBehaviour = GetComponentInParent<PeterEatBehaviour>();
        eyeLook = GetComponentInParent<EyeLook>();
        player = GameObject.FindObjectOfType<PlayerController>();
        StopLook();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLook()
    {
        rend.enabled = true;
        isActive = true;
		rend.material.color = okColor;
        this.GetComponent<Collider>().enabled = true;
    }

    public void StopLook()
    {
        rend.enabled = false;
        isActive = false;
        this.GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive && other.tag == "Player")
        {
            if (player.IsCovered == false)
            {
                //"Become angry"
                Debug.Log("Detected Player!");
                PeterEatBehaviour.StartBecomeAngry();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isActive && other.tag == "Player")
        {
            PeterEatBehaviour.StopBecomeAngry();
        }
    }
	
    private void OnTriggerStay(Collider other)
    {
        if (isActive && other.tag == "Player")
        {
            if (player.IsCovered == false)
            {
				float blinkIntensity=0.75f;
				
				float lerpDone=0.25f;
				float startBlink=0.5f;
				float angerPercent=Mathf.Clamp01(PeterEatBehaviour.angerMeter/PeterEatBehaviour.angerKillLevel);
				float lerpPercent=Mathf.Clamp01(angerPercent+(1-lerpDone)*angerPercent);
				
				Vector4 rendColor=Vector4.Lerp(okColor,angryColor,lerpPercent);
				float intensity = 1.0f;
				if(angerPercent>startBlink)
				{
					//Offset time so blink starts in correct phase
					if(angerFuryStartTime==-1)
					{
						angerFuryStartTime=Time.time;
					}
					float blinkTime=Time.time-angerFuryStartTime;
					float speed=25;
					float blink=(Mathf.Sin(blinkTime*speed)+1)/2;
					
					intensity = Mathf.Lerp(1,blinkIntensity,blink);
				}
				
				rend.material.color=rendColor;
				rend.material.SetFloat("_Intensity", intensity);			}
		}
	}		
}
