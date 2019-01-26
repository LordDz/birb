using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heater : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
        foreach (TowelContainer towelContainer in GetComponentsInChildren<TowelContainer>())
        {
            if (towelContainer.HasItem())
            {
                towelContainer.HeatUp(0.2f * Time.deltaTime);
            }
        }
    }
}
