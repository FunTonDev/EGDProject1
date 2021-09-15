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
    //Force applied to player if they hold jump
    public float sustainedJumpForce;
    //Timespan in which force can be applied
    public float maxJumpTime;
    //Gravity force
    public float gravityForce;
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

    private Vector2 playerVelocity;
    private float jumpTime;
    private float stepTime;
    public AudioSource playerSource;
    public List<AudioClip> playerClips;

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
        //If player is on a watering hole, set bool to false
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
        grounded = false;
        waterTank = 1.0f;
        waterTankMax = 1.0f;
        stepTime = 0.0f;
        playerVelocity = new Vector2(0.0f, 0.0f);
    }

    void FixedUpdate()
    {
        if (!grounded) {
            playerVelocity = new Vector2(playerVelocity.x, playerVelocity.y - gravityForce * Time.deltaTime);
        }

        // try horizontal movement first (fails on overlap with ground)
        Vector3 newPos = playerRB.position + Vector2.right * playerVelocity.x * Time.deltaTime;
        Collider[] overlaps = Physics.OverlapBox(newPos, playerCollider.bounds.extents / 2, Quaternion.identity, 1 << 3);
        if (overlaps.Length == 0) {
            playerRB.position = newPos;
        }

        // next try vertical movement (fails on overlap with ground)
        newPos = playerRB.position + Vector2.up * playerVelocity.y * Time.deltaTime;
        overlaps = Physics.OverlapBox(newPos, playerCollider.bounds.extents / 2, Quaternion.identity, 1 << 3);
        if (overlaps.Length == 0) {
            playerRB.position = newPos;
        }

        //Check if the player is grounded
        bool wasGrounded = grounded;
        grounded = false;
        for (int i = 0; i < 3; ++i) {
            RaycastHit2D groundCheck = Physics2D.Raycast(playerCollider.bounds.center + Vector3.right * ((i - 1) * (playerCollider.bounds.extents.x - 0.2f)) + Vector3.down * playerCollider.bounds.size.y / 2, Vector3.down, Mathf.Infinity, 1 << 3);

            if (groundCheck.collider != null && groundCheck.distance < 0.015f) {
                grounded = true;

                break;
            }
        }
        if (grounded && !wasGrounded) {
            playerVelocity = new Vector2(playerVelocity.x, 0);
        }
        if (grounded) animator.SetBool("isJumping", false);
    }

    private void Update() {
        if (jumpTime > 0) {
            jumpTime -= Time.deltaTime;
        }
        //JUMP
        if (Input.GetAxisRaw("Vertical") > 0 && (grounded || jumpTime > 0)) {
            playerVelocity = new Vector2(playerVelocity.x, playerVelocity.y + (jumpTime > 0 ? sustainedJumpForce * Time.deltaTime : jumpForce));
            if (jumpTime <= 0) jumpTime = maxJumpTime;
            animator.SetBool("isJumping", true);
            playerSource.PlayOneShot(playerClips[2]);
        }

        //Get movement input in the horizontal axis, multiply by speed, and set player velocity to that amount
        float mover = Input.GetAxisRaw("Horizontal");
        mover = mover * playerSpeed;
        playerVelocity = new Vector2(mover, playerVelocity.y);
        animator.SetFloat("PlayerSpeed", Mathf.Abs(playerVelocity.x));

        if (mover != 0) {
            animator.SetBool("Watering", false);
            animator.SetBool("Refilling", false);
            if (mover > 0) {
                GetComponent<SpriteRenderer>().flipX = true;
            } else if (mover < 0) {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            if (Time.time - stepTime > 0.4f || stepTime == 0.0f) {
                playerSource.PlayOneShot(playerClips[Random.Range(3, 6)], 0.5f);
                stepTime = Time.time;
            }
        }


        //INTERACT
        if (grounded && jumpTime <= 0 && Input.GetAxisRaw("Vertical") < 0)
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

        waterBar.fillAmount = waterTank;
    }
}
