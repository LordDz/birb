using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeterEatBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float angerMeter = 0;
    public float angerKillLevel = 2f;
    private bool isAngry = false;
    private bool killMode = false;
    public AudioSource soundAngry;
    public AudioSource soundChill;
    public AudioSource soundKillMode;

    void Start()
    {
        
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
        }
    }

    public void StartBecomeAngry()
    {
        soundAngry.Play();
        isAngry = true;
    }

    public void StopBecomeAngry()
    {
        soundChill.Play();
        isAngry = false;
    }

    private void CheckAnger()
    {
        if (angerMeter >= angerKillLevel)
        {
            //KILL BIRB NAOW!
            StartKillBird();
        }
    }

    private void StartKillBird()
    {
        killMode = true;
        soundKillMode.Play();
    }
}
