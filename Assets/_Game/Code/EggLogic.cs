using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggLogic : MonoBehaviour
{
    public ParentToHidables eggHidable;
    public ParentToHidables babyHidable;

    public float timeToHatch = 5.0f;

    void Start()
    {
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

}
