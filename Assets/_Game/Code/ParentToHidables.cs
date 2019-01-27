using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentToHidables : MonoBehaviour
{
    public bool hidingAtStart;

    private Hidable[] hidables;

    // Start is called before the first frame update
    void Start()
    {
        hidables = GetComponentsInChildren<Hidable>();
        if (hidingAtStart)
        {
            HideAll();
        }
        else
        {
            UnhideAll();
        }
    }

    public void HideAll()
    {
        foreach (Hidable hidable in hidables)
        {
            hidable.Hide();
        }
    }

    public void UnhideAll()
    {
        foreach (Hidable hidable in hidables)
        {
            hidable.Unhide();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
