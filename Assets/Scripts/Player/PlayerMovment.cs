using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    #region Inspector Variables
    public CharacterController controller;
    //public float playerSpeed = 40f;
    public float playerClimbingSpeed = 1f;
    public float MaxPlayerSpeed = 40f;
    private float playerSpeed;

    #endregion
    #region Variables
    int state = 0;

    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool jump = false;
    int isJumping = 0;
    bool crouch = false;

    bool isInLadder;
    float gravityScale;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gravityScale = GetComponent<Rigidbody2D>().gravityScale;
        playerSpeed = MaxPlayerSpeed;
    }

    #region Get Set
    public void SetPlayerSpeed(float input)
    {
        playerSpeed = input;
    }

    public float GetPlayerSpeed()
    {
        return playerSpeed;
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        #region Movment Speeds
        horizontalMove = Input.GetAxisRaw("Horizontal") * playerSpeed;
        verticalMove = isInLadder ? Input.GetAxisRaw("Vertical") * playerClimbingSpeed : 0f;
        #endregion

        #region Keys Press
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = isJumping == 0 ? 1 : isJumping;
            jump = true;
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
        #endregion 

        #region Animation
        state = horizontalMove != 0 ? (int)PlayerAnimationState.walking : (int)PlayerAnimationState.idle;
        state = isJumping == 2 ? (int)PlayerAnimationState.jump : state;
        state = isInLadder ? (int)PlayerAnimationState.climbing : state;
        if (isInLadder)
        {
            this.GetComponent<Animator>().speed = verticalMove == 0 ? 0 : 1;
        }
        else if (crouch == true)
        {
            state = horizontalMove == 0 ? (int)PlayerAnimationState.crouch : (int)PlayerAnimationState.crouchWalking;
        }
        else
            this.GetComponent<Animator>().speed = 1;
        this.GetComponent<PlayerAnimationManager>().SetAnimationState(state);
        #endregion
    }

    //Applay the move speeds and move the player using the player controller
    private void FixedUpdate()
    {
        if (isInLadder)
        {
            transform.Translate(new Vector3(0, verticalMove * Time.deltaTime, 0));
        }
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    //not jumping anymore -> stop jumping animation
    public void OnLanding()
    {
        isJumping = isJumping > 0 ? isJumping + 1 : isJumping;

        if (isJumping == 3)
        {
            isJumping = 0;
        }
    }

    //On entering the ladder
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ladder")
        {
            isInLadder = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    //On exiting the ladder
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ladder")
        {
            isInLadder = false;
            GetComponent<Rigidbody2D>().gravityScale = gravityScale;
        }
    }
}
