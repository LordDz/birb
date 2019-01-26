using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTowards : MonoBehaviour
{
    public SpawnPos FlyFrom;
    public SpawnPos FlyTo;
    private bool isFlying = false;
    private float speed = 10f;
    private AudioSource soundFly;
    private FlyManager flyManager;

    // Start is called before the first frame update
    void Start()
    {
        soundFly = GetComponent<AudioSource>();
        flyManager = GetComponentInParent<FlyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlying)
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), 
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

    public void SpawnAtStart(SpawnPos flyFrom, SpawnPos flyTo)
    {
        this.FlyFrom = flyFrom;
        this.FlyTo = flyTo;
        this.transform.position = FlyFrom.transform.position;
    }

    public void StartFlying()
    {
        isFlying = true;
        if (soundFly != null)
        {
            soundFly.Play();
        }
    }
}
