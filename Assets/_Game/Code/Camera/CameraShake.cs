using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;
    private AudioSource soundShake;

    void Awake()
    {
        soundShake = GetComponent<AudioSource>();
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            StopShake();
        }
    }

    public void StartShake(float duration = 10000f)
    {
        shakeDuration = duration;
        soundShake.Play();
    }

    public void StopShake()
    {
        shakeDuration = 0f;
        camTransform.localPosition = originalPos;
        soundShake.Stop();
    }
}