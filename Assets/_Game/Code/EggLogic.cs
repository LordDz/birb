using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggLogic : MonoBehaviour
{
    public float timeToHatch = 10.0f;
    public Transform eggTransform;
    public ParentToHidables eggHidable;
    public ParentToHidables babyHidable;

    private float twiggleTimer = 0.0f;

    private Vector3 eggOriginPosition;

    void Start()
    {
        eggOriginPosition = eggTransform.position;
    }

    public void UpdateHatchTimer()
    {
        timeToHatch -= Time.deltaTime;
        if (HasHatched())
        {
            eggHidable.HideAll();
            babyHidable.UnhideAll();
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
            float shift = (Random.value - 0.5f) * 0.1f;
            eggTransform.position = eggOriginPosition + new Vector3(shift, 0.0f, 0.0f);
        }
        twiggleTimer -= Time.deltaTime;
    }
}
