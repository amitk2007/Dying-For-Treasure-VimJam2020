using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public CharacterController controller;
    public float playerSpeed = 40f;
    public float playerClimbingSpeed = 1f;
    public Animator animator;


    int state = 0;

    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool jump = false;
    int isJumping = -1;
    bool crouch = false;

    bool isInLadder;
    float gravityScale;

    // Start is called before the first frame update
    void Start()
    {
        gravityScale = GetComponent<Rigidbody2D>().gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * playerSpeed;
        verticalMove = isInLadder ? Input.GetAxisRaw("Vertical") * playerClimbingSpeed : 0f;

        if (Input.GetButtonDown("Jump") && isInLadder == false)
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

        #region Animation
        state = horizontalMove != 0 ? (int)PlayerAnimationState.walking : (int)PlayerAnimationState.idle;
        state = isJumping == 2 ? (int)PlayerAnimationState.jump : state;
        state = playerClimbingSpeed != 0 ? (int)PlayerAnimationState.climbing : state;
        animator.SetInteger("State", state);
        #endregion
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        if (isInLadder)
        {
            transform.Translate(new Vector3(0, verticalMove * Time.deltaTime, 0));
        }
        jump = false;
    }

    public void OnLanding()
    {
        isJumping++;
        if (isJumping == 3)
        {
            isJumping = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInLadder = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isInLadder = false;
        GetComponent<Rigidbody2D>().gravityScale = gravityScale;

    }





}
