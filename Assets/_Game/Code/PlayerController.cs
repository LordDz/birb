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

    public AudioSource walkSound;
    public AudioSource pickUpSound;
    public AudioSource putDownSound;
    public AudioSource jumpSound;

    private float walkCycle = 0.0f;

    private ParentToHidables hidables;
    private Rigidbody rb;
    private bool inHatchArea = false;
    private State state = State.NORMAL;
    private EggLogic egg;
    private SpriteRenderer sprite;
    private Pickupable itemInHand;
    private Pickupable itemAbleToPickup;
    private GameObject stoveAbleToUse;


    private bool isCovered = false;

    public bool IsCovered { get => isCovered; private set => isCovered = value; }

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
            if (itemInHand == null)
            {
                Pickupable item = target.gameObject.GetComponent<Pickupable>();
                if (!item.HasOwner())
                {
                    float distanceToClosest = float.PositiveInfinity;
                    if (itemAbleToPickup != null)
                    {
                        distanceToClosest = Vector3.Distance(transform.position, itemAbleToPickup.transform.position);
                    }
                    float distanceToThis = Vector3.Distance(transform.position, item.transform.position);
                    if (distanceToThis < distanceToClosest)
                    {
                        itemAbleToPickup = item;
                    }
                }
            }
        }
        else if (target.tag == "Stove")
        {
            stoveAbleToUse = target.gameObject;
        }
        else if (target.tag == "Hatch Area")
        {
            inHatchArea = true;
        }
    }

    void OnTriggerExit(Collider target)
    {
        if (target.tag == "Pickupable")
        {
            if (target.gameObject.GetComponent<Pickupable>() == itemAbleToPickup)
            {
                itemAbleToPickup = null;
            }
        }
        else if (target.tag == "Stove")
        {
            stoveAbleToUse = null;
        }
        else if (target.tag == "Hatch Area")
        {
            inHatchArea = false;
        }
    }

    void SitOnEgg()
    {
        walkCycle = 0.0f;
        spriteTransform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        state = State.TRANSITION_TO_SITTING_ON_EGG;
        ReleaseItemInHand();
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
        transform.localPosition = new Vector3(
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

            if (walkCycle == 0.0f)
            {
                walkCycle = 1e-6f;
            }
        }
        if (walkCycle > 0.0f)
        {
            walkCycle += Time.deltaTime * 4.0f;
            if (walkCycle > 1.0f)
            {
                walkSound.Play();
                walkCycle = 0.0f;
            }
            spriteTransform.localPosition = new Vector3(0.0f, 0.4f * Mathf.Abs(Mathf.Sin(walkCycle * Mathf.PI)), 0.0f);
        }
        else
        {
            walkCycle = 0.0f;
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

    void ReleaseItemInHand()
    {
        if (itemInHand != null)
        {
            itemInHand.UnsetOwner();
            itemInHand = null;
        }
    }

    void PutItemInContainer(GameObject gameObject)
    {
        TowelContainer[] towelContainers = gameObject.GetComponentsInChildren<TowelContainer>();
        float closestContainerDistance = float.PositiveInfinity;
        TowelContainer closestContainer = null;
        foreach (TowelContainer towelContainer in towelContainers)
        {
            if (!towelContainer.HasItem())
            {
                float distance = Vector3.Distance(transform.position, towelContainer.transform.position);
                if (distance < closestContainerDistance)
                {
                    closestContainerDistance = distance;
                    closestContainer = towelContainer;
                }
            }
        }
        if (closestContainer != null)
        {
            closestContainer.Give(itemInHand);
            itemInHand = null;
        }
        else
        {
            ReleaseItemInHand();
        }
    }

    void TakeItemFromContainer(GameObject gameObject)
    {
        TowelContainer[] towelContainers = gameObject.GetComponentsInChildren<TowelContainer>();
        float closestContainerDistance = float.PositiveInfinity;
        TowelContainer closestContainer = null;
        foreach (TowelContainer towelContainer in towelContainers)
        {
            if (towelContainer.HasItem())
            {
                float distance = Vector3.Distance(transform.position, towelContainer.transform.position);
                if (distance < closestContainerDistance)
                {
                    closestContainerDistance = distance;
                    closestContainer = towelContainer;
                }
            }
        }
        if (closestContainer != null)
        {
            itemInHand = closestContainer.Take();
        }
    }

    void UpdateItemInHandPosition()
    {
        if (itemInHand != null)
        {
            itemInHand.Follow(
                    spriteTransform.position
                    + new Vector3(0.0f, 0.5f, -0.25f),
                    transform.rotation);
        }
        if (Input.GetButtonDown("Pick Up"))
        {
            if (itemInHand != null)
            {
                if (inHatchArea)
                {
                    PutItemInContainer(egg.gameObject);
                }
                else if (stoveAbleToUse != null)
                {
                    PutItemInContainer(stoveAbleToUse);
                }
                else
                {
                    ReleaseItemInHand();
                }
            }
            else if (itemAbleToPickup != null)
            {
                itemAbleToPickup.SetOwner();
                itemInHand = itemAbleToPickup;
                itemAbleToPickup = null;
            }
            else if (inHatchArea)
            {
                TakeItemFromContainer(egg.gameObject);
            }
            else if (stoveAbleToUse != null)
            {
                TakeItemFromContainer(stoveAbleToUse);
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

    public void SetCovered(bool covered)
    {
        this.IsCovered = covered;
        if (covered)
        {
            Debug.Log("COVERED");
            sprite.material.color = new Color(0.1f, 0.1f, 0.1f);
        }
        else
        {
            Debug.Log("NOT COVERED");
            sprite.material.color = new Color(1f, 1f, 1f);
        }
    }

    public void SetDead(bool dead)
    {
        this.gameObject.SetActive(false);
    }
}
