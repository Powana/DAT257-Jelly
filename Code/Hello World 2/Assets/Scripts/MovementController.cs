using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Vector2 centerPoint;
    private Vector3 pos;

    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        centerPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxisRaw("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);
    }
    private void LateUpdate()
    {
        pos = Camera.main.WorldToViewportPoint(transform.position);
        // Use if statement to know when to set velocity to zero, otherwise it wouldnt be needed
        if (pos.x > 1 || pos.x < 0 || pos.y > 1 || pos.y < 0)
        {
            pos.x = Mathf.Clamp01(pos.x);
            pos.y = Mathf.Clamp01(pos.y);
            pos = Camera.main.ViewportToWorldPoint(pos);
            transform.position = pos;
            rb2d.velocity = Vector2.zero;
        }

    }
}
