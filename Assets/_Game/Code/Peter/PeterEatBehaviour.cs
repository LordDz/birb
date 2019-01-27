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
    private AudioSource audioSource;
    public AudioClip clipAngry;
    public AudioClip clipKillMode;
    private PlayerController player;
    public LoseShow loseShow;

    private EyeLook eyeLook;

    void Start()
    {
        eyeLook = GetComponentInChildren<EyeLook>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindObjectOfType<PlayerController>();
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
            else if (isAngry && player.IsCovered == false)
            {
                angerMeter += Time.deltaTime;
            }
            CheckAnger();
        }
    }

    public void StartBecomeAngry()
    {
        audioSource.clip = clipAngry;
        audioSource.Play();
        isAngry = true;
        eyeLook.StopLooking();
    }

    public void StopBecomeAngry()
    {
        isAngry = false;
        eyeLook.StartLooking();
    }

    private void CheckAnger()
    {
        if (player.IsCovered && isAngry)
        {
            StopBecomeAngry();
        }

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
        audioSource.clip = clipKillMode;
        audioSource.Play();
        player.SetDead(true);
        loseShow.ShowLose();
        
        this.gameObject.SetActive(false);

    }
}
