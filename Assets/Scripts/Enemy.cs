using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;
    private float moveSpeed = 5f;
    private Rigidbody2D body, playerBody;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerBody = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        int xDirection = System.Math.Sign(playerBody.position.x - body.position.x);
        int yDirection = System.Math.Sign(playerBody.position.y - body.position.y);
        body.velocity = new Vector2(xDirection * moveSpeed, yDirection * moveSpeed);
    }
}
