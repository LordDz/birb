using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xspeed = Input.GetAxis("Horizontal");
        float yspeed = Input.GetAxis("Vertical");
        
        Vector3 translation = new Vector3(xspeed, 0.0f, yspeed);

        if (translation.magnitude != 0.0f)
        {
            translation /= translation.magnitude;

            translation *= movementSpeed * Time.deltaTime;

            transform.Translate(translation);
        }
    }
}
