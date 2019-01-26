using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    private Renderer renderer;
    private float time;
    private float timeOffset;
    private float smokeWiggleSpeed;
    
    void Start()
    {
        renderer = GetComponent<Renderer>();
        time = Random.Range(1.5f, 2.5f);
        timeOffset = Random.value;
        smokeWiggleSpeed = Random.Range(8.0f, 12.0f);
    }

    void Update()
    {
        time -= Time.deltaTime;
        transform.Translate(new Vector3(
                    Mathf.Cos((time + timeOffset) * smokeWiggleSpeed) * 0.2f,
                    1.0f, 0.0f) * Time.deltaTime);
        renderer.material.color = new Color(
                renderer.material.color.r,
                renderer.material.color.g,
                renderer.material.color.b,
                Mathf.Clamp(time, 0, 1));
        if (time < 0)
        {
            Destroy(gameObject);
        }
    }
}
