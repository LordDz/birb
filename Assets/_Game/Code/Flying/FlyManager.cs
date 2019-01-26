using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyManager : MonoBehaviour
{
    private SpawnPosGroup[] listSpawns;
    private SpawnFlyGroup[] listFlyGroups;

    private FlyTowards flyBird;
    private int flyIndex = 0;
    private WindowSelector WindowSelector;

    private float nextCooldown = 0f;
    public float timeWaitPerFly;

    private float roundCooldown = 0f;
    public float timePerRound = 2f;

    // Start is called before the first frame update
    void Start()
    {
        listFlyGroups = GetComponentsInChildren<SpawnFlyGroup>();
        listSpawns = listFlyGroups[0].GetComponentsInChildren<SpawnPosGroup>();
        WindowSelector = GameObject.FindObjectOfType<WindowSelector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flyBird == null)
        {
            flyBird = GetComponentInChildren<FlyTowards>();
            DoNewFlyBy();
        }

        if (nextCooldown > 0f)
        {
            nextCooldown -= Time.deltaTime;
            if (nextCooldown <= 0)
            {
                NextBird();
            }
        }

        if (roundCooldown > 0f)
        {
            roundCooldown -= Time.deltaTime;
            if (roundCooldown <= 0)
            {
                flyBird.StartFlying();
            }
        }
    }

    public void DoNewFlyBy()
    {
        listSpawns = listFlyGroups[Random.Range(0, listFlyGroups.Length - 1)].GetComponentsInChildren<SpawnPosGroup>();
        flyBird.SpawnAtStart(listSpawns[0].FlyFrom, listSpawns[0].FlyTo);
        roundCooldown = timePerRound;
    }

    public void BirdHasReachedEnd()
    {
        flyIndex++;
        if (flyIndex < listSpawns.Length)
        {
            nextCooldown = timeWaitPerFly;
        }
        else
        {
            flyIndex = 0;
            WindowSelector.StartPeterSelect();
        }
    }

    private void NextBird()
    {
        flyBird.SpawnAtStart(listSpawns[flyIndex].FlyFrom, listSpawns[flyIndex].FlyTo);
        flyBird.StartFlying();
    }
}
