using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionMovement : MonoBehaviour
{
    enum State
    {
        NONE,
        FORWARD,
        BACKWARD
    };

    public Transform positionStart;
    public Transform positionEnd;

    private State state = State.NONE;
    private float transitionAlpha = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartTransitionForward()
    {
        state = State.FORWARD;
        transitionAlpha = 0.0f;
    }

    public void StartTransitionBackward()
    {
        state = State.BACKWARD;
        transitionAlpha = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.NONE)
        {
            transform.position = positionStart.position - new Vector3(0.0f, -10.0f, 0.0f);
        }
        else
        {
            if (state == State.FORWARD)
            {
                transitionAlpha += 0.1f * Time.deltaTime;
                if (transitionAlpha > 1.0f)
                {
                    state = State.NONE;
                }
            }
            else if (state == State.BACKWARD)
            {
                transitionAlpha -= 0.1f * Time.deltaTime;
                if (transitionAlpha < 0.0f)
                {
                    state = State.NONE;
                }
            }
            Debug.Log(transitionAlpha);
            transform.position = Vector3.Lerp(positionStart.position, positionEnd.position, transitionAlpha);
        }
    }
}
