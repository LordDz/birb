using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggLogic : MonoBehaviour
{
    public float timeToHatch = 10.0f;
    public Transform eggTransform;
    public ParentToHidables eggHidable;
    public ParentToHidables babyHidable;
    public Sprite[] sprites;

    private float startTime;
    private float twiggleTimer = 0.0f;
    private SpriteRenderer sprite;
    private int spriteIndex;
    private WinShow winShow;

    private Vector3 eggOriginPosition;

    void Start()
    {
        eggOriginPosition = eggTransform.position;
        GameObject go = GameObject.FindWithTag("Egg Shell");
        winShow = GameObject.FindObjectOfType<WinShow>();
        sprite = go.GetComponentInChildren<SpriteRenderer>();
        startTime = timeToHatch;
    }

    float HatchTimerMultiplier()
    {
        float multiplier = 1.0f;
        foreach (TowelContainer towelContainer in GetComponentsInChildren<TowelContainer>())
        {
            if (towelContainer.HasItem())
            {
                multiplier *= 1.0f + towelContainer.GetHeat() * 0.9f + 0.1f;
            }
        }
        return multiplier;
    }

    public void UpdateHatchTimer()
    {
        float multiplier = HatchTimerMultiplier();
        timeToHatch -= Time.deltaTime * multiplier;
        if (HasHatched())
        {
            eggHidable.HideAll();
            babyHidable.UnhideAll();
            winShow.ShowVictory();
        }
    }

    public bool HasHatched()
    {
        return timeToHatch < 0.0f;
    }

    void Update()
    {
        if (Random.value < 1 / (timeToHatch + 1e-6) * Time.deltaTime * 5.0f)
        {
            twiggleTimer = 0.1f;
        }
        if (twiggleTimer > 0.0f)
        {
            float shift = (Random.value - 0.5f) * 0.08f;
            eggTransform.position = eggOriginPosition + new Vector3(shift, 0.0f, 0.0f);
        }
        twiggleTimer -= Time.deltaTime;

        int previousSpriteIndex = spriteIndex;
        spriteIndex = (int) Mathf.Floor((startTime - timeToHatch) / startTime * sprites.Length);
        spriteIndex = Mathf.Min(spriteIndex, sprites.Length - 1);
        if (previousSpriteIndex != spriteIndex)
        {
            twiggleTimer = 0.1f;
            sprite.sprite = sprites[spriteIndex];
        }
    }
}
