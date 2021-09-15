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
    //Collider of the player character
    private Collider2D playerCollider;
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

    public Animator animator;

    private float stepTime;
    public AudioSource playerSource;
    public List<AudioClip> playerClips;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        //If player is on crop space, set current crop to interact with
        if (collision.gameObject.tag == "Crop") {
            onCrop = true;
            currentCrop = collision.gameObject;
        }
        //If player is on a watering hole, set bool to true
        if (collision.gameObject.tag == "WaterHole") {
            onWater = true;
        }

        if (collision.gameObject.tag == "Rocket")
        {
            //Initiate Stage End
            mani.stageEnd(false);
            Debug.Log("Rocketeer");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        //If player is on crop space, set current crop to interact with
        if (collision.gameObject.tag == "Crop") {
            onCrop = false;
            currentCrop = null;
        }
        //If player is on a watering hole, set bool to true
        if (collision.gameObject.tag == "WaterHole") {
            onWater = false;
        }
    }

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        GameObject temp = GameObject.FindGameObjectWithTag("Manager");
        mani = temp.GetComponent<StageManager>();
        //playerDirection = 1.0f;
        grounded = true;
        waterTank = 1.0f;
        waterTankMax = 1.0f;
        stepTime = 0.0f;
    }

    void FixedUpdate()
    {
        //JUMP
        if (Input.GetAxisRaw("Vertical") > 0 && grounded) {
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
            animator.SetBool("isJumping", true);
            playerSource.PlayOneShot(playerClips[2]);
        }

        //Get movement input in the horizontal axis, multiply by speed, and move that amount
        float mover = Input.GetAxisRaw("Horizontal");
        mover = mover * playerSpeed;
        playerRB.velocity = new Vector2(mover, playerRB.velocity.y);
        animator.SetFloat("PlayerSpeed", Mathf.Abs(playerRB.velocity.x));

        if (mover != 0)
        {
            animator.SetBool("Watering", false);
            animator.SetBool("Refilling", false);
            if (mover > 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (mover < 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            if (Time.time - stepTime > 0.4f || stepTime == 0.0f)
            {
                playerSource.PlayOneShot(playerClips[Random.Range(3,6)], 0.5f);
                stepTime = Time.time;
            }
        }
    }

    private void Update()
    {
        //INTERACT
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            //INTERACT with crop while having water
            if (onCrop && playerRB.velocity.y == 0 && currentCrop.GetComponent<Crop>().hasCrop && waterTank > 0)
            {
                animator.SetBool("Watering", true);
                //If interact with a plot of land that has a crop, perform operation
                currentCrop.GetComponent<Crop>().scoreCrop();
                waterTank -= 0.2f;
                playerSource.PlayOneShot(playerClips[1]);
            }
            //INTERACT with watering hole
            else if (onWater && playerRB.velocity.y == 0)
            {
                animator.SetBool("Refilling", true);
                waterTank = waterTankMax;
                playerSource.PlayOneShot(playerClips[0], 0.025f);
            }
        }

        //Check if the player is grounded
        RaycastHit2D groundCheck = Physics2D.Raycast(playerCollider.bounds.center + Vector3.down * playerCollider.bounds.size.y / 2, Vector3.down, Mathf.Infinity, 1 << 0);
        grounded = groundCheck.collider != null && groundCheck.distance < 0.015f;
        if (grounded) animator.SetBool("isJumping", false);

        waterBar.fillAmount = waterTank;
    }
}
