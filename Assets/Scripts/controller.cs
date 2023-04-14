using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class controller : MonoBehaviour
{
    private int dashFrames = 60;
    private bool pressingLeft = false, pressingRight = false, pressingUp = false, pressingDown = false, movementDisabled = false;
    private float moveSpeed, walkSpeed = .1f, dashSpeed = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!movementDisabled)
        {
            pressingLeft = Input.GetKey(KeyCode.A);
            pressingRight = Input.GetKey(KeyCode.D);
            pressingUp = Input.GetKey(KeyCode.W);
            pressingDown = Input.GetKey(KeyCode.S);
        }
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(DisableMovementInput(dashFrames));
    }

    private void FixedUpdate()
    {
        int xDirection = Convert.ToInt32(pressingLeft) * -1 + Convert.ToInt32(pressingRight);
        int yDirection = Convert.ToInt32(pressingUp) + Convert.ToInt32(pressingDown) * -1;
        transform.Translate(new Vector3(xDirection * moveSpeed, yDirection * moveSpeed, 0));
    }

    private IEnumerator DisableMovementInput(int duration)
    {
        movementDisabled = true;
        moveSpeed = dashSpeed;
        for (int i = 0; i < duration; i++)
            yield return null;
        movementDisabled = false;
        moveSpeed = walkSpeed;
    }
}