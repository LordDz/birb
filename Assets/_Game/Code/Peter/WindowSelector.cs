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

    public bool isSelectingPeter = false;
    private CameraShake cameraShake;
    private Prop[] listProps;
    private PlayerController player;

    void Start()
    {
        cameraShake = GameObject.FindObjectOfType<CameraShake>();
        listPeters = GameObject.FindObjectsOfType<PeterMain>();
        listProps = GameObject.FindObjectsOfType<Prop>();
        player = GameObject.FindObjectOfType<PlayerController>();
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
            //peter.GetComponent<Transform>().gameObject.SetActive(false);
        }
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
		// Random.Range returns MAX-1, ie does not include the range's MAX number. 
		// So use +1 if it should be included. 
		// Since this is an array, we obviously settle for the array length
		// https://docs.unity3d.com/ScriptReference/Random.Range.html
        peterCurrent = listPeters[Random.Range(0, listPeters.Length)];
		
        //peterCurrent.GetComponent<Transform>().gameObject.SetActive(true);
        peterCurrent.SetEnabled();
        for (var i = 0; i < listProps.Length; i++)
        {
            listProps[i].SetPeterNowLooksFromHere(peterCurrent.SeeDirection);
        }
        cameraShake.StopShake();
    }

    public void HideAllCoverFromProps()
    {
        player.SetCovered(false);
        for (var i = 0; i < listProps.Length; i++)
        {
            listProps[i].HideAllCovers();
        }
    }

    public PeterDirection GetPeterSees()
    {
        return peterCurrent.SeeDirection;
    }
}
