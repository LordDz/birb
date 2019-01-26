using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    // Start is called before the first frame update
    public CoverPos HiddenFromLeft;
    public CoverPos HiddenFromMiddle;
    public CoverPos HiddenFromRight;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPeterNowLooksFromHere(PeterDirection peterDirection)
    {
        switch (peterDirection)
        {
            case PeterDirection.Left:
                HiddenFromLeft.SetEnabled(true);
                break;
            case PeterDirection.Middle:
                HiddenFromMiddle.SetEnabled(true);
                break;
            case PeterDirection.Right:
                HiddenFromRight.SetEnabled(true);
                break;
        }
    }

    public void HideAllCovers()
    {
        HiddenFromLeft.SetEnabled(false);
        HiddenFromMiddle.SetEnabled(false);
        HiddenFromRight.SetEnabled(false);
    }
}
