using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Current stage manager
    private StageManager mani;
    //Rigid body of the player character
    private Rigidbody2D playerRB;
    //Speed that the player moves at horizontally
    public float playerSpeed;
    //Force the player jumps up with
    public float jumpForce;
    //Max amount the water tank can hold
    public float waterTankMax = 1.0f;
    //The current amount the tank holds
    public float waterTank = 1.0f;
    //Whether the player is on the ground or not
    private bool grounded;
    //Whether the player is colliding with a crop
    private bool onCrop;
    //Whether the player is on top of the watering hole
    private bool onWater;
    //The crop the player is colliding with
    private GameObject currentCrop;
    //UI Bar to display water tank amount
    public Image waterBar;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If player is on crop space, set current crop to interact with
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
        //If player is on a watering hole, set bool to true
        if (collision.gameObject.tag == "WaterHole")    {    onWater = true;    }
        else    {   onWater = false;    }
    }

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        GameObject temp = GameObject.FindGameObjectWithTag("Manager");
        mani = temp.GetComponent<StageManager>();
        //playerDirection = 1.0f;
        grounded = true;
        waterTank = 1.0f;
        waterTankMax = 1.0f;
    }

    void FixedUpdate()
    {
        //Get movement input in the horizontal axis, multiply by speed, and move that amount
        float mover = Input.GetAxisRaw("Horizontal");
        mover = mover * playerSpeed;
        playerRB.velocity = new Vector2(mover, playerRB.velocity.y);
        waterBar.fillAmount = waterTank;

    }

    private void Update()
    {
        //JUMP
        if (Input.GetAxisRaw("Vertical") > 0 && grounded)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
            grounded = false;
        }

        //INTERACT
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            //INTERACT with crop while having water
            if (onCrop && playerRB.velocity.y == 0 && currentCrop.GetComponent<Crop>().hasCrop && waterTank > 0)
            {
                //If interact with a plot of land that has a crop, perform operation and gain points
                currentCrop.GetComponent<Crop>().removeCrop();
                mani.score += 100;
                mani.updateScore();
                waterTank -= 0.2f;
            }
            //INTERACT with watering hole
            else if (onWater && playerRB.velocity.y == 0)
            {
                waterTank = waterTankMax;
                Debug.Log("Refilled");
            }
        }
        //If player isn't moving up or down, they are on the ground
        if (playerRB.velocity.y == 0)
        {
            grounded = true;
        }
    }
}
