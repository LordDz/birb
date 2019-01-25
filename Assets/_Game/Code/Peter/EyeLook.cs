using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLook : MonoBehaviour
{
    public List<Sprite> listSprites;
    private EyeCollision[] listCollisions;
    public int currentIndex = 0;
    private SpriteRenderer spriteRenderer;

    public float timeWait = 0.15f;
    private float cooldownCurrent = 0f;
    private EyeCollision currentCollision;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        listCollisions = GetComponentsInChildren<EyeCollision>();
        currentCollision = listCollisions[0];
    }

    // Update is called once per frame
    void Update()
    {
        cooldownCurrent -= Time.deltaTime;
        if (cooldownCurrent <= 0)
        {
            cooldownCurrent = timeWait;
            LookRandom();
        }
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

    private void LookShuffle()
    {
        
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
}
