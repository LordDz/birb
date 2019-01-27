using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionMovement : MonoBehaviour
{
    enum State
    {
        FINISHED,
        FORWARD,
        BACKWARD
    };

    public float transitionSpeed = 5.0f;
    public Transform positionStart;
    public Transform positionEnd;

    private State state = State.FINISHED;
    private float transitionAlpha = 0.0f;
    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
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

    public bool IsFinished()
    {
        return state == State.FINISHED;
    }

    Vector3 QuadraticBezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        return (1 - t) * ((1 - t) * p0 + t * p1) + t * ((1 - t) * p1 + t * p2);
    }

    void Transition()
    {
        Vector3 topPosition = (positionEnd.position + positionStart.position) / 2 + new Vector3(0.0f, 2.5f, 0.0f);
        transform.position = QuadraticBezier(
                positionStart.position,
                topPosition,
                positionEnd.position,
                transitionAlpha);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.FINISHED)
        {
            transform.position = positionStart.position + new Vector3(0.0f, -20.0f, 0.0f);
        }
        else
        {
            if (state == State.FORWARD)
            {
                sprite.flipX = positionEnd.position.x > transform.position.x;
                transitionAlpha += transitionSpeed * Time.deltaTime;
                if (transitionAlpha > 1.0f)
                {
                    state = State.FINISHED;
                }
            }
            else if (state == State.BACKWARD)
            {
                sprite.flipX = positionStart.position.x > transform.position.x;
                transitionAlpha -= transitionSpeed * Time.deltaTime;
                if (transitionAlpha < 0.0f)
                {
                    state = State.FINISHED;
                }
            }
            Transition();
        }
    }
}
