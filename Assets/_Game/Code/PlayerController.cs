using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum State
    {
        NORMAL,
        SITTING_ON_EGG,
        TRANSITION_TO_NORMAL,
        TRANSITION_TO_SITTING_ON_EGG
    };
    public float movementSpeed = 4.0f;
    public ParentToHidables sittingOnEggHidables;
    public TransitionMovement sitOnEggTransition;

    private ParentToHidables hidables;
    private Rigidbody rb;
    private bool inHatchArea = false;
    private State state = State.NORMAL;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
       hidables = GetComponent<ParentToHidables>();
    }

    void OnTriggerStay(Collider target)
    {
        if (target.tag == "Hatch Area")
        {
            inHatchArea = true;
        }
    }

    void OnTriggerExit(Collider target)
    {
        if (target.tag == "Hatch Area")
        {
            inHatchArea = false;
        }
    }

    void SitOnEgg(bool active)
    {
        if (active)
        {
            state = State.TRANSITION_TO_SITTING_ON_EGG;
            hidables.HideAll();
            sitOnEggTransition.StartTransitionForward();
        }
        else
        {
            state = State.TRANSITION_TO_NORMAL;
            sittingOnEggHidables.HideAll();
            sitOnEggTransition.StartTransitionBackward();
        }
    }

    void UpdateNormal()
    {
        float xspeed = Input.GetAxis("Horizontal");
        float yspeed = Input.GetAxis("Vertical");
        
        Vector3 translation = new Vector3(xspeed, 0.0f, yspeed);

        if (translation.magnitude != 0.0f)
        {
            translation /= translation.magnitude;

            translation *= movementSpeed * Time.deltaTime;

            rb.MovePosition(transform.position + translation);
        }
        if (inHatchArea)
        {
            if (Input.GetButtonDown("Action"))
            {
                SitOnEgg(true);
            }
        }
    }

    void UpdateSitOnEgg()
    {
        if (Input.GetButtonDown("Action"))
        {
            state = State.NORMAL;
            SitOnEgg(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.NORMAL)
        {
            UpdateNormal();
        }
        else if (state == State.SITTING_ON_EGG)
        {
            UpdateSitOnEgg();
        }
        else if (state == State.TRANSITION_TO_SITTING_ON_EGG)
        {
            if (sitOnEggTransition.IsFinished())
            {
                state = State.SITTING_ON_EGG;
                sittingOnEggHidables.UnhideAll();
            }
        }
        else if (state == State.TRANSITION_TO_NORMAL)
        {
            if (sitOnEggTransition.IsFinished())
            {
                state = State.NORMAL;
                hidables.UnhideAll();
            }
        }
    }
}
