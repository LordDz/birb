using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLook : MonoBehaviour
{
    private FlyManager flyManager;
    private PeterEatBehaviour PeterEatBehaviour;
    public List<Sprite> listSprites;
    private EyeCollision[] listCollisions;
    public int currentIndex = 0;
    private SpriteRenderer spriteRenderer;
    private Sprite spriteIdle;

    public float timeWait = 0.15f;
    private float cooldownCurrent = 0f;
    private EyeCollision currentCollision;
    private bool shouldLook = true;

    private float cooldownIdle = 0f;
    public float timeWaitIdle = 1f;

    public float totalMin = 4f;
    public float totalMax = 10f;
    private float timeTotalRange = 10f;
    private float cooldownTotal = 0f;
    public Material materialOk;
    public Material materialDetected;

    // Start is called before the first frame update
    void Start()
    {
        flyManager = GameObject.FindObjectOfType<FlyManager>();
        PeterEatBehaviour = GetComponentInParent<PeterEatBehaviour>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        listCollisions = GetComponentsInChildren<EyeCollision>();
        currentCollision = listCollisions[0];
        spriteIdle = spriteRenderer.sprite;
        timeTotalRange = Random.Range(totalMin, totalMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownIdle > 0)
        {
            cooldownIdle -= Time.deltaTime;
            if (cooldownIdle <= 0)
            {
                StartLooking();
            }
        }

        if (shouldLook)
        {
            cooldownCurrent -= Time.deltaTime;
            if (cooldownCurrent <= 0)
            {
                cooldownCurrent = timeWait;
                LookRandom();
            }

            if (cooldownTotal < timeTotalRange && PeterEatBehaviour.IsAngry == false)
            {
                cooldownTotal += Time.deltaTime;
                if (cooldownTotal >= timeTotalRange)
                {
                    IsDoneLooking();
                }
            }
        }
    }

    public void SetEnabled()
    {
        cooldownTotal = 0;
        timeTotalRange = Random.Range(totalMin, totalMax);
        shouldLook = false;
        cooldownIdle = timeWaitIdle;
        spriteRenderer.sprite = spriteIdle;
        spriteRenderer.enabled = true;
    }

    public void StartLooking()
    {
        shouldLook = true;
        cooldownCurrent = timeWait;
        LookRandom();
    }

    public void StopLooking()
    {
        shouldLook = false;
    }

    private void Look(LookDirection direction)
    {
        currentCollision.StopLook();
        switch (direction)
        {
            case LookDirection.Left:
                spriteRenderer.sprite = listSprites[0];
                currentCollision = listCollisions[0];
                break;
            case LookDirection.Right:
                spriteRenderer.sprite = listSprites[1];
                currentCollision = listCollisions[1];
                break;
            case LookDirection.Top:
                spriteRenderer.sprite = listSprites[2];
                currentCollision = listCollisions[2];
                break;
            case LookDirection.Bot:
                spriteRenderer.sprite = listSprites[3];
                currentCollision = listCollisions[3];
                break;
        }
        currentCollision.StartLook();
    }

    private void LookRandom()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                Look(LookDirection.Left);
                break;
            case 1:
                Look(LookDirection.Right);
                break;
            case 2:
                Look(LookDirection.Top);
                break;
            case 3:
                Look(LookDirection.Bot);
                break;
        }
    }

    private void IsDoneLooking()
    {
        StopLooking();
        foreach (var eyeCollision in listCollisions)
        {
            eyeCollision.StopLook();
        }
        spriteRenderer.enabled = false;
        flyManager.DoNewFlyBy();
    }
}
