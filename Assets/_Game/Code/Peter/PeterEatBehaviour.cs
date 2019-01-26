using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeterEatBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float angerMeter = 0;
    public float angerKillLevel = 2f;
    public bool IsAngry { get { return isAngry; } }
    private bool isAngry = false;
    private bool killMode = false;
    public AudioSource soundAngry;
    public AudioSource soundChill;
    public AudioSource soundKillMode;

    private EyeLook eyeLook;

    void Start()
    {
        eyeLook = GetComponentInChildren<EyeLook>();
    }

    // Update is called once per frame
    void Update()
    {
        if (killMode == false)
        {
            if (angerMeter > 0 && !isAngry)
            {
                angerMeter -= Time.deltaTime;
            }
            else if (isAngry)
            {
                angerMeter += Time.deltaTime;
            }
            CheckAnger();
        }
    }

    public void StartBecomeAngry()
    {
        soundAngry.Play();
        isAngry = true;
        eyeLook.StopLooking();
    }

    public void StopBecomeAngry()
    {
        soundChill.Play();
        isAngry = false;
        eyeLook.StartLooking();
    }

    private void CheckAnger()
    {
        if (angerMeter >= angerKillLevel)
        {
            //KILL BIRB NAOW!
            isAngry = true;
            StartKillBird();
        }
    }

    private void StartKillBird()
    {
        //It's game over man, game over!
        killMode = true;
        soundKillMode.Play();
    }
}
