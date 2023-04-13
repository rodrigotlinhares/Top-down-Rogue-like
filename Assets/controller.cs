using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class controller : MonoBehaviour
{
    public float speed;
    private bool pressingLeft = false, pressingRight = false, pressingUp = false, pressingDown = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pressingLeft = Input.GetKey(KeyCode.A);
        pressingRight = Input.GetKey(KeyCode.D);
        pressingUp = Input.GetKey(KeyCode.W);
        pressingDown = Input.GetKey(KeyCode.S);
    }

    private void FixedUpdate()
    {
        int xDirection = Convert.ToInt32(pressingLeft) * -1 + Convert.ToInt32(pressingRight);
        int yDirection = Convert.ToInt32(pressingUp) + Convert.ToInt32(pressingDown) * -1;
        transform.Translate(new Vector3(xDirection * speed, yDirection * speed, 0));
    }
}