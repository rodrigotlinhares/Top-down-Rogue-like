using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D projectile;

    private int dashFrames = 60, iFrames = 750;
    private bool pressingLeft = false, pressingRight = false, pressingUp = false, pressingDown = false, movementDisabled = false;
    private float moveSpeed, walkSpeed = 5f, dashSpeed = 25f, projectileSpeed = 10f;
    private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = walkSpeed;
        body = GetComponent<Rigidbody2D>();
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
            StartCoroutine(Dash(dashFrames));
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            
    }

    private void FixedUpdate()
    {
        int xDirection = Convert.ToInt32(pressingLeft) * -1 + Convert.ToInt32(pressingRight);
        int yDirection = Convert.ToInt32(pressingUp) + Convert.ToInt32(pressingDown) * -1;
        body.velocity = new Vector2(xDirection * moveSpeed, yDirection * moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            StartCoroutine(GoInvulnerable(iFrames, collision.gameObject.layer));
    }

    private IEnumerator Dash(int duration)
    {
        movementDisabled = true;
        moveSpeed = dashSpeed;
        for (int i = 0; i < duration; i++)
            yield return null;
        movementDisabled = false;
        moveSpeed = walkSpeed;
    }

    private IEnumerator GoInvulnerable(int duration, int enemyLayer)
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer, true);
        GetComponent<SpriteRenderer>().color = Color.cyan;
        for (int i = 0; i < duration; i++)
            yield return null;
        Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer, false);
        GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    private void Shoot(Vector2 direction)
    {
        direction.Normalize();
        Rigidbody2D clone = Instantiate(projectile, body.transform.position, Quaternion.identity);
        clone.velocity = direction * projectileSpeed;
    }
}