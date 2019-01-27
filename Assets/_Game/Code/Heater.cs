using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heater : MonoBehaviour
{
    public Smoke smoke;

    // Update is called once per frame
    void Update()
    {
        foreach (TowelContainer towelContainer in GetComponentsInChildren<TowelContainer>())
        {
            if (towelContainer.HasItem())
            {
                towelContainer.HeatUp(0.2f * Time.deltaTime);

                if (towelContainer.GetHeat() > 0.9f)
                {
                    if (Random.value < Time.deltaTime * 10.0f)
                    {
                        Vector3 randomVector = new Vector3(
                                Random.Range(-0.5f, 0.5f),
                                0.0f,
                                Random.Range(-0.5f, 0.0f));
                        Vector3 position = towelContainer.transform.position + randomVector;
                        Object.Instantiate(smoke, position, transform.rotation);
                    }
                }
            }
        }
    }
}
