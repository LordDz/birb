using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSelector : MonoBehaviour
{
    // Start is called before the first frame update

    private PeterMain[] listPeters;
    PeterMain peterCurrent;
    public float timeToSelectPeter = 1f;
    private float timeSelectCooldown = 0f;

    private bool isSelectingPeter = false;
    private CameraShake cameraShake;

    void Start()
    {
        cameraShake = GameObject.FindObjectOfType<CameraShake>();
        listPeters = GameObject.FindObjectsOfType<PeterMain>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInited == false)
        {
            Init();
        }
        
        if (isSelectingPeter)
        {
            timeSelectCooldown -= Time.deltaTime;
            if (timeSelectCooldown <= 0)
            {
                SetRandomPeter();
            }
        }
    }

    private bool isInited = false;
    private void Init()
    {
        isInited = true;
        foreach (PeterMain peter in listPeters)
        {
            peter.GetComponent<Transform>().gameObject.SetActive(false);
        }
        StartPeterSelect();
    }

    public void StartPeterSelect()
    {
        timeSelectCooldown = timeToSelectPeter;
        isSelectingPeter = true;
        cameraShake.StartShake();
    }

    private void SetRandomPeter()
    {
        isSelectingPeter = false;
        peterCurrent = listPeters[Random.Range(0, listPeters.Length - 1)];
        peterCurrent.GetComponent<Transform>().gameObject.SetActive(true);
        peterCurrent.SetEnabled();
        cameraShake.StopShake();
    }
}
