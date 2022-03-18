using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector3 startPosition;
    public delegate void OnBlockHit(); //declare a new delegate type
    public OnBlockHit blockHitObservers; //instantiate an observer set
    // Start is called before the first frame update
    public void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        // blockHitObservers += SomeBlockHitHandler; //adding the observers set a handler
        // blockHitObservers += SomeBlockHitHandler2;
        // blockHitObservers(); //call the delegates
        Launch();
    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        // startPosition = new Vector2(Random.Range(1, 33), 10);
        transform.position = startPosition;
        Launch();
    }

    private void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = -1;
        rb.velocity = new Vector2(speed * x, speed * y);
    }

    void Update()
    {
        float x = rb.velocity.normalized.x > 0 ? 1 : -1;
        float y = rb.velocity.normalized.y > 0 ? 1 : -1;
        rb.velocity = new Vector2(speed * x, speed * y);
    }

    public Vector2 GetVelocityDirection()
    {
        float x = rb.velocity.normalized.x > 0 ? 1 : -1;
        float y = rb.velocity.normalized.y > 0 ? 1 : -1;
        return new Vector2(x, y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Block"))
        {
            blockHitObservers();
            Destroy(collision.gameObject);
        }
    }

    // void SomeBlockHitHandler()
    // {
    //     print("I handled it");
    // }

    // void SomeBlockHitHandler2()
    // {
    //     print("I handled it");
    // }
}
