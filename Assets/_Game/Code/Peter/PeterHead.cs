using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeterHead : MonoBehaviour
{
    private bool revealed = false;
    private float timer = 0.0f;
    private Vector3 originPosition;
    private float alpha = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.localPosition;
    }

    public void Reveal()
    {
        revealed = true;
    }

    public void Hide()
    {
        revealed = false;
        Debug.Log(alpha);
    }

    // Update is called once per frame
    void Update()
    {
        if (revealed)
        {
            alpha = Mathf.Min(alpha + Time.deltaTime * 5.0f, 1.0f);
        }
        else
        {
            alpha = Mathf.Max(alpha - Time.deltaTime * 5.0f, 0.0f);
        }
        timer += Time.deltaTime * 2.0f;
        transform.localPosition = originPosition
            + 0.1f * Vector3.up * Mathf.Sin(timer)
            - 5.0f * Vector3.up * Mathf.SmoothStep(0.0f, 1.0f, 1.0f - alpha);
        if (timer > 2 * Mathf.PI)
        {
            timer = 0.0f;
        }
    }
}
