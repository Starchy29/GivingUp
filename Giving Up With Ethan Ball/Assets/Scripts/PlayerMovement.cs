using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float WalkForce;
    [SerializeField] private float MaxWalkSpeed;
    [SerializeField] private float FrictionForce;
    [SerializeField] private float JumpVelocity;

    private Rigidbody2D rigBod;
    private bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        rigBod = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // move left / right
        if(Input.GetKey(KeyCode.LeftArrow)) {
            rigBod.AddForce(new Vector2(-WalkForce * Time.deltaTime, 0f));
        }
        if(Input.GetKey(KeyCode.RightArrow)) {
            rigBod.AddForce(new Vector2(WalkForce * Time.deltaTime, 0f));
        }

        // apply friction
        if(rigBod.velocity.x != 0) {
            int mult = (rigBod.velocity.x > 0 ? -1 : 1);
            rigBod.AddForce(new Vector2(mult * FrictionForce * Time.deltaTime, 0f));
        }

        // cap speed
        if(rigBod.velocity.x > MaxWalkSpeed) {
            rigBod.velocity = new Vector2(MaxWalkSpeed, rigBod.velocity.y);
        }
        else if(rigBod.velocity.x < -MaxWalkSpeed) {
            rigBod.velocity = new Vector2(-MaxWalkSpeed, rigBod.velocity.y);
        }

        if(grounded) {
            // jumping
            if(Input.GetKeyDown(KeyCode.UpArrow)) {
                rigBod.velocity = new Vector2(rigBod.velocity.x, JumpVelocity);
                grounded = false;
            }
        } else {
            // extendable jump height
            rigBod.gravityScale = 4;
            if(rigBod.velocity.y > 0 && Input.GetKey(KeyCode.UpArrow)) {
                rigBod.gravityScale = 2;
            }

            // land
            if(rigBod.velocity.y == 0) {
                grounded = true;
            }
        }
    }

    public void WindPush(float force) {
        rigBod.AddForce(new Vector2(force * Time.deltaTime, 0f));
    }
}
