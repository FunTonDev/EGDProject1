using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*
     * ALL THIS IS STARTER/SCRATCH CODE
     * USE/CHANGE/DELETE AS YOU WISH
     */
    private Rigidbody2D playerRB;
    private float playerSpeed, playerDirection;
    private bool grounded;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerSpeed = 5.0f;
        playerDirection = 1.0f;
    }

    void FixedUpdate()
    {
        playerRB.velocity = new Vector2(playerSpeed * playerDirection, 0);        
    }

    private void Update()
    {
        playerDirection = Input.GetKeyDown(KeyCode.LeftArrow) ? -1.0f : playerDirection;
        playerDirection = Input.GetKeyDown(KeyCode.RightArrow) ? 1.0f : playerDirection;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //JUMP
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //INTERACT
        }
    }
}
