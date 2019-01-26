using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowelContainer : MonoBehaviour
{
    Pickupable itemInContainer;

    public void Give(Pickupable item)
    {
        itemInContainer = item;
    }

    public Pickupable Take()
    {
        Pickupable toReturn = itemInContainer;
        itemInContainer = null;
        return toReturn;
    }

    public bool HasItem()
    {
        return itemInContainer != null;
    }

    public void HeatUp(float amount)
    {
        itemInContainer.HeatUp(amount);
    }

    public float GetHeat()
    {
        return itemInContainer.GetHeat();
    }

    // Update is called once per frame
    void Update()
    {
        if (itemInContainer)
        {
            itemInContainer.Follow(
                    transform.position
                    + new Vector3(0.0f, 0.0f, -0.25f));
        }
    }
}
