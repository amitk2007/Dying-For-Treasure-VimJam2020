using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public CharacterController controller;
    public float playerSpeed = 40f;
    public Animator animator;

    float horizontalMove = 0f;
    bool jump = false;
    int isJumping = -1;
    bool crouch = false;

    int state = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * playerSpeed;

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

        #region Animation
        state = horizontalMove != 0 ? (int)PlayerAnimationState.walking : (int)PlayerAnimationState.idle;
        state = isJumping == 2 ? (int)PlayerAnimationState.jump : state;
        animator.SetInteger("State", state);
        #endregion
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
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

}
