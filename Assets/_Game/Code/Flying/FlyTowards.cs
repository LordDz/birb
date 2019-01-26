using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTowards : MonoBehaviour
{
    public SpawnPos FlyFrom;
    public SpawnPos FlyTo;
    private bool isFlying = false;
    public float speed = 60f;
    private AudioSource soundFly;

    public List<AudioClip> listFlySounds;
    private FlyManager flyManager;
    SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        soundFly = GetComponent<AudioSource>();
        flyManager = GetComponentInParent<FlyManager>();
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlying)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, 
                FlyTo.transform.position, speed * Time.deltaTime);

            var distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
                new Vector2(FlyTo.transform.position.x, FlyTo.transform.position.y));
            if (distance <= 0.5f)
            {
                isFlying = false;
                flyManager.BirdHasReachedEnd();
            }
        }
    }

    public void SpawnAtStart(SpawnPos flyFrom, SpawnPos flyTo, bool toLeft)
    {
        this.FlyFrom = flyFrom;
        this.FlyTo = flyTo;
        if (toLeft)
        {
            this.rend.flipX = true;
        }
        else
        {
            this.rend.flipX = false;
        }
        this.transform.position = FlyFrom.transform.position;
    }

    public void StartFlying()
    {
        isFlying = true;
        var sound = listFlySounds[Random.Range(0, listFlySounds.Count - 1)];
        if (sound != null)
        {
            soundFly.clip = sound;
            soundFly.Play();
        }
    }
}
