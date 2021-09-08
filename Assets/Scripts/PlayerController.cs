using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*
     * ALL THIS IS STARTER/SCRATCH CODE
     * USE/CHANGE/DELETE AS YOU WISH
     */
    //Current stage manager
    private StageManager mani;
    //Rigid body of the player character
    private Rigidbody2D playerRB;
    //Speed that the player moves at horizontally
    public float playerSpeed;
    //Force the player jumps up with
    public float jumpForce;
    private float playerDirection;
    //Whether the player is on the ground or not
    private bool grounded;
    //Whether the player is colliding with a crop
    private bool onCrop;
    //The crop the player is colliding with
    private GameObject currentCrop;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Crop")
        {
            onCrop = true;
            currentCrop = collision.gameObject;
        }
        else
        {
            onCrop = false;
            currentCrop = null;
        }
    }

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        GameObject temp = GameObject.FindGameObjectWithTag("Manager");
        mani = temp.GetComponent<StageManager>();
        //playerSpeed = 5.0f;
        playerDirection = 1.0f;
        grounded = true;
    }

    void FixedUpdate()
    {
        //Get movement input in the horizontal axis, multiply by speed, and move that amount
        float mover = Input.GetAxisRaw("Horizontal");
        mover = mover * playerSpeed;
        playerRB.velocity = new Vector2(mover, playerRB.velocity.y);
        //playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * playerSpeed;
    }

    private void Update()
    {
        //playerDirection = (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) ? -1.0f : playerDirection;
        //playerDirection = (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) ? 1.0f : playerDirection;

        //JUMP
        if (Input.GetAxisRaw("Vertical") > 0 && grounded)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
            grounded = false;
        }

        //INTERACT
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            //INTERACT
            if (onCrop && playerRB.velocity.y == 0 && currentCrop.GetComponent<Crop>().hasCrop)
            {
                currentCrop.GetComponent<Crop>().removeCrop();
                mani.score += 100;
                mani.updateScore();
            }
            
            Debug.Log("Should interact");
        }
        if (playerRB.velocity.y == 0)
        {
            grounded = true;
        }
    }
}
