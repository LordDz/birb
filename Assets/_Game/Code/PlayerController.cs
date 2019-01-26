using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum State
    {
        NORMAL,
        HATCHING
    };
    public float movementSpeed = 4.0f;
    public SittingOnEggLogic sittingOnEggLogic;
    public TransitionMovement sitOnEggTransition;

    private Rigidbody rb;
    private bool inHatchArea = false;
    private State state = State.NORMAL;
    private Vector3 voidPosition;
    private Vector3 memoryPosition;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
       voidPosition = transform.position + new Vector3(0.0f, -10.0f, 0.0f);
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
            sittingOnEggLogic.SetActive(true);
            sitOnEggTransition.StartTransitionForward();
            memoryPosition = transform.position;
            transform.position = voidPosition;
        }
        else
        {
            sittingOnEggLogic.SetActive(false);
            sitOnEggTransition.StartTransitionBackward();
            transform.position = memoryPosition;
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
                state = State.HATCHING;
                SitOnEgg(true);
            }
        }
    }

    void UpdateHatching()
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
        else if (state == State.HATCHING)
        {
            UpdateHatching();
        }
    }
}
