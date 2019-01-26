using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum State
    {
        NORMAL,
        SITTING_ON_EGG,
        FINISHED,
        TRANSITION_TO_NORMAL,
        TRANSITION_TO_SITTING_ON_EGG,
        TRANSITION_TO_FINISHED
    };
    public float movementSpeed = 4.0f;
    public ParentToHidables sittingOnEggHidables;
    public TransitionMovement sitOnEggTransition;
    public Transform spriteTransform;

    private float walkCycle = 0.0f;

    private ParentToHidables hidables;
    private Rigidbody rb;
    private bool inHatchArea = false;
    private State state = State.NORMAL;
    private EggLogic egg;
    private SpriteRenderer sprite;
    private Pickupable itemInHand;
    private Pickupable itemAbleToPickup;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hidables = GetComponent<ParentToHidables>();
        egg = GameObject.FindWithTag("Egg").GetComponent<EggLogic>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void OnTriggerStay(Collider target)
    {
        if (target.tag == "Pickupable")
        {
            itemAbleToPickup = target.gameObject.GetComponent<Pickupable>();
        }
        if (target.tag == "Hatch Area")
        {
            inHatchArea = true;
        }
    }

    void OnTriggerExit(Collider target)
    {
        if (target.tag == "Pickupable")
        {
            if (target.gameObject == itemAbleToPickup)
            {
                itemAbleToPickup = null;
            }
        }
        if (target.tag == "Hatch Area")
        {
            inHatchArea = false;
        }
    }

    void SitOnEgg()
    {
        walkCycle = 0.0f;
        spriteTransform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        state = State.TRANSITION_TO_SITTING_ON_EGG;
        itemInHand = null;
        hidables.HideAll();
        sitOnEggTransition.StartTransitionForward();
    }

    void JumpOffEgg()
    {
        state = State.TRANSITION_TO_NORMAL;
        sittingOnEggHidables.HideAll();
        sitOnEggTransition.StartTransitionBackward();
    }

    void HatchEgg()
    {
        state = State.TRANSITION_TO_FINISHED;
        sittingOnEggHidables.HideAll();
        transform.position = new Vector3(
                egg.transform.position.x - 2.0f,
                0.0f,
                egg.transform.position.z + 1.0f);
        sitOnEggTransition.StartTransitionBackward();
    }

    void UpdateNormal()
    {
        float xspeed = Input.GetAxis("Horizontal");
        float yspeed = Input.GetAxis("Vertical");
        
        Vector3 translation = new Vector3(xspeed, 0.0f, yspeed);

        if (xspeed > 0.0f)
        {
            sprite.flipX = true;
        }
        else if (xspeed < 0.0f)
        {
            sprite.flipX = false;
        }

        if (translation.magnitude != 0.0f)
        {
            translation /= translation.magnitude;

            translation *= movementSpeed * Time.deltaTime;
            
            rb.MovePosition(transform.position + translation);

            if (walkCycle >= 1.0f)
            {
                walkCycle -= 1.0f;
            }
        }
        if (walkCycle < 1.0f)
        {
            walkCycle += Time.deltaTime * 4.0f;
            spriteTransform.localPosition = new Vector3(0.0f, 0.4f * Mathf.Abs(Mathf.Sin(walkCycle * Mathf.PI)), 0.0f);
        }
        else
        {
            walkCycle = 1.0f;
        }
        if (inHatchArea)
        {
            if (Input.GetButtonDown("Action"))
            {
                SitOnEgg();
            }
        }
        UpdateItemInHandPosition();
    }

    void UpdateSitOnEgg()
    {
        if (Input.GetButtonDown("Action"))
        {
            JumpOffEgg();
        }
        egg.UpdateHatchTimer();
        if (egg.HasHatched())
        {
            HatchEgg();
        }
    }

    void UpdateItemInHandPosition()
    {
        if (itemInHand)
        {
            itemInHand.Follow(
                    spriteTransform.position
                    + new Vector3(0.0f, 0.5f, -0.25f));
            if (Input.GetButtonDown("Pick Up"))
            {
                itemInHand = null;
            }
        }
        else if (itemAbleToPickup)
        {
            if (Input.GetButtonDown("Pick Up"))
            {
                itemInHand = itemAbleToPickup;
            }
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
        else if (state == State.FINISHED)
        {
            sprite.flipX = true;
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
            sprite.flipX = sitOnEggTransition.positionEnd.position.x < transform.position.x;
            if (sitOnEggTransition.IsFinished())
            {
                state = State.NORMAL;
                hidables.UnhideAll();
            }
        }
        else if (state == State.TRANSITION_TO_FINISHED)
        {
            if (sitOnEggTransition.IsFinished())
            {
                state = State.FINISHED;
                hidables.UnhideAll();
            }
        }
    }
}
